using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;


namespace CapWeb.Captacao
{
    public partial class Vincular_Imob : MaterialForm
    {
        private string DBA;
        private bool carregando = false;
        private bool precisaAtualizar = false;

        public Vincular_Imob(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
            this.FormClosing += Vincular_Imob_FormClosing; // Associa evento de fechamento
        }

        private async void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.Text = "Carregando...";
                await Task.Run(() => Carregar_VinculosAsync());
                this.Invoke((MethodInvoker)(() => this.Text = "Vinculação de imobiliárias"));
                e.Handled = true;
            }
        }

        private async Task Carregar_VinculosAsync()
        {
            if (carregando) return;
            carregando = true;
            try
            {
                SetStatus("Atualizando...", Color.Gold);
                SetDgvEnabled(false);

                using (SqlConnection conn = new SqlConnection(DBA))
                {
                    await conn.OpenAsync();

                    // 1. Obter lista de Imobiliárias
                    string sqlImobiliarias = "SELECT ID_Imobiliaria, Nome_Imobiliaria FROM Imobiliaria";
                    SqlDataAdapter daImob = new SqlDataAdapter(sqlImobiliarias, conn);
                    DataTable dtImob = new DataTable();
                    daImob.Fill(dtImob);

                    // 2. Obter lista de Proprietários
                    string sqlProprietarios = "SELECT ID, Nome FROM Proprietarios ORDER BY ID DESC";
                    SqlDataAdapter daProp = new SqlDataAdapter(sqlProprietarios, conn);
                    DataTable dtProp = new DataTable();
                    daProp.Fill(dtProp);

                    // 3. Obter vinculações
                    string sqlVinculos = "SELECT ID_Proprietario, ID_Imobiliaria FROM Proprietario_Imobiliaria";
                    SqlDataAdapter daVinc = new SqlDataAdapter(sqlVinculos, conn);
                    DataTable dtVinc = new DataTable();
                    daVinc.Fill(dtVinc);

                    // 4. Criar DataTable de exibição
                    DataTable dtGrid = new DataTable();
                    dtGrid.Columns.Add("ID_Proprietario", typeof(int));
                    dtGrid.Columns.Add("Proprietario", typeof(string));

                    // Adicionar colunas dinamicamente para cada imobiliária
                    foreach (DataRow row in dtImob.Rows)
                    {
                        string nomeImobiliaria = row["Nome_Imobiliaria"].ToString();
                        dtGrid.Columns.Add(nomeImobiliaria, typeof(bool));
                    }

                    // 5. Preencher linhas
                    foreach (DataRow propRow in dtProp.Rows)
                    {
                        DataRow newRow = dtGrid.NewRow();
                        newRow["ID_Proprietario"] = propRow["ID"];
                        newRow["Proprietario"] = propRow["Nome"];

                        foreach (DataRow imobRow in dtImob.Rows)
                        {
                            int idProp = (int)propRow["ID"];
                            int idImob = (int)imobRow["ID_Imobiliaria"];

                            bool vinculado = dtVinc.Select($"ID_Proprietario = {idProp} AND ID_Imobiliaria = {idImob}").Length > 0;
                            newRow[imobRow["Nome_Imobiliaria"].ToString()] = vinculado;
                        }

                        dtGrid.Rows.Add(newRow);
                    }

                    // 6. Atualizar a interface (UI)
                    this.Invoke((MethodInvoker)delegate
                    {
                        dgvVinculos.DataSource = dtGrid;

                        // 7. Configurar CheckBox nas colunas de Imobiliária
                        foreach (DataRow row in dtImob.Rows)
                        {
                            string colName = row["Nome_Imobiliaria"].ToString();
                            if (!(dgvVinculos.Columns[colName] is DataGridViewCheckBoxColumn))
                            {
                                int colIndex = dgvVinculos.Columns[colName].Index;

                                dgvVinculos.Columns.RemoveAt(colIndex);

                                DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
                                {
                                    Name = colName,
                                    HeaderText = colName,
                                    DataPropertyName = colName
                                };

                                dgvVinculos.Columns.Insert(colIndex, chk);
                            }
                        }
                    });
                }
                SetStatus("Atualizado!", Color.LimeGreen);
                precisaAtualizar = false; // Resetar flag após atualizar
            }
            catch (Exception ex)
            {
                SetStatus($"Erro ao atualizar: {ex.Message}", Color.Red);
            }
            finally
            {
                SetDgvEnabled(true);
                carregando = false;
            }
        }

        private async void dgvVinculos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvVinculos.Columns[e.ColumnIndex].Name;
                if (colName == "ID_Proprietario" || colName == "Proprietario") return;

                int idProprietario = Convert.ToInt32(dgvVinculos.Rows[e.RowIndex].Cells["ID_Proprietario"].Value);

                try
                {
                    SetStatus("Salvando...", Color.Gold);
                    dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = true;

                    int idImobiliaria = await BuscarIdImobiliariaAsync(colName);
                    bool vinculado = Convert.ToBoolean(dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                    await Task.Run(async () =>
                    {
                        if (vinculado)
                            await VincularAsync(idProprietario, idImobiliaria);
                        else
                            await DesvincularAsync(idProprietario, idImobiliaria);
                    });

                    SetStatus(vinculado ? "Vinculado com sucesso!" : "Desvinculado com sucesso!", vinculado ? Color.LimeGreen : Color.OrangeRed);
                    precisaAtualizar = true; // Marcar que precisa atualizar
                }
                catch (Exception ex)
                {
                    SetStatus($"Erro: {ex.Message}", Color.Red);
                }
                finally
                {
                    if (e.RowIndex >= 0 && e.RowIndex < dgvVinculos.Rows.Count &&
                        e.ColumnIndex >= 0 && e.ColumnIndex < dgvVinculos.Columns.Count)
                    {
                        dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].ReadOnly = false;
                    }
                    // Após a alteração, recarrega a tabela para garantir que o estado está correto
                    await Carregar_VinculosAsync();
                }
            }
        }

        private async Task<int> BuscarIdImobiliariaAsync(string nomeImobiliaria)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                await conn.OpenAsync();
                string sql = "SELECT ID_Imobiliaria FROM Imobiliaria WHERE Nome_Imobiliaria = @nome";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nomeImobiliaria);
                    object result = await cmd.ExecuteScalarAsync();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Imobiliária não encontrada: " + nomeImobiliaria);
                    }
                }
            }
        }

        private async Task VincularAsync(int idProprietario, int idImobiliaria)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                await conn.OpenAsync();

                // Buscar o valor cobrado da imobiliária
                string sqlValor = "SELECT Valor_Cobrado FROM Imobiliaria WHERE ID_Imobiliaria = @id";
                SqlCommand cmdValor = new SqlCommand(sqlValor, conn);
                cmdValor.Parameters.AddWithValue("@id", idImobiliaria);
                object valorObj = await cmdValor.ExecuteScalarAsync();

                string valorCobradoStr = valorObj != null ? valorObj.ToString() : "R$ 0,00";

                // Converter para decimal, removendo "R$"
                decimal valorCobradoDecimal = 0;
                decimal.TryParse(
                    valorCobradoStr.Replace("R$", "").Trim(),
                    NumberStyles.Any,
                    new CultureInfo("pt-BR"),
                    out valorCobradoDecimal
                );

                // Inserir na tabela se não existir
                string sql = @"
            IF NOT EXISTS (
                SELECT 1 FROM Proprietario_Imobiliaria 
                WHERE ID_Proprietario = @idP AND ID_Imobiliaria = @idI
            )
            BEGIN
                INSERT INTO Proprietario_Imobiliaria 
                (ID_Proprietario, ID_Imobiliaria, Valor)
                VALUES (@idP, @idI, @valor)
            END";

                using (SqlCommand cmdInsert = new SqlCommand(sql, conn))
                {
                    cmdInsert.Parameters.AddWithValue("@idP", idProprietario);
                    cmdInsert.Parameters.AddWithValue("@idI", idImobiliaria);
                    cmdInsert.Parameters.AddWithValue("@valor", valorCobradoDecimal);
                    await cmdInsert.ExecuteNonQueryAsync();
                }
            }
        }

        private async Task DesvincularAsync(int idProprietario, int idImobiliaria)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                await conn.OpenAsync();
                string sql = "DELETE FROM Proprietario_Imobiliaria WHERE ID_Proprietario = @idP AND ID_Imobiliaria = @idI";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idP", idProprietario);
                    cmd.Parameters.AddWithValue("@idI", idImobiliaria);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        private async void timer1_Tick(object sender, EventArgs e)
        {
            await Carregar_VinculosAsync();
        }

        private async void Vincular_Imob_Load(object sender, EventArgs e)
        {
            await Carregar_VinculosAsync();
        }

        // Método auxiliar para atualizar o status de forma thread-safe
        private void SetStatus(string texto, Color cor)
        {
            if (LB_Status.InvokeRequired)
            {
                LB_Status.Invoke((MethodInvoker)(() => {
                    LB_Status.Text = texto;
                    LB_Status.ForeColor = cor;
                }));
            }
            else
            {
                LB_Status.Text = texto;
                LB_Status.ForeColor = cor;
            }
        }

        // Método auxiliar para alterar dgvVinculos.Enabled de forma thread-safe
        private void SetDgvEnabled(bool enabled)
        {
            if (dgvVinculos.InvokeRequired)
            {
                dgvVinculos.Invoke((MethodInvoker)(() => dgvVinculos.Enabled = enabled));
            }
            else
            {
                dgvVinculos.Enabled = enabled;
            }
        }

        private void Vincular_Imob_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (precisaAtualizar)
            {
                MessageBox.Show("Você realizou alterações. Para garantir que a tabela esteja atualizada, pressione F5 antes de sair da tela.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }
    }
}
