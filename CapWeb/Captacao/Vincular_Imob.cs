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

        // Estrutura para armazenar alterações pendentes
        private class AlteracaoVinculo
        {
            public int IdProprietario { get; set; }
            public string NomeImobiliaria { get; set; }
            public bool Vincular { get; set; }
        }
        private List<AlteracaoVinculo> alteracoesPendentes = new List<AlteracaoVinculo>();

        public Vincular_Imob(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
            this.FormClosing += Vincular_Imob_FormClosing; // Associa evento de fechamento
            this.dgvVinculos.MultiSelect = true; // Habilita seleção múltipla
            this.dgvVinculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect; // Seleção por linha
            this.dgvVinculos.CellContentClick += dgvVinculos_CellContentClick; // Novo evento para seleção em lote
        }

        private async void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                this.Text = "Carregando...";
                await Task.Run(() => AplicarAlteracoesPendentesAsync());
                await Task.Run(() => Carregar_VinculosAsync());
                this.Invoke((MethodInvoker)(() => this.Text = "Vinculação de imobiliárias"));
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.F6)
            {
                this.Text = "Filtrando...";
                await Task.Run(() => AplicarFiltroAsync());
                this.Invoke((MethodInvoker)(() => this.Text = "Vinculação de imobiliárias"));
                e.Handled = true;
            }
        }

        private async Task Carregar_VinculosAsync(string nomeProprietario = null, string nomeImobiliaria = null)
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
                    if (!string.IsNullOrEmpty(nomeProprietario))
                        sqlProprietarios = $"SELECT ID, Nome FROM Proprietarios WHERE Nome = @Nome ORDER BY ID DESC";
                    SqlDataAdapter daProp = new SqlDataAdapter();
                    if (!string.IsNullOrEmpty(nomeProprietario))
                    {
                        daProp.SelectCommand = new SqlCommand(sqlProprietarios, conn);
                        daProp.SelectCommand.Parameters.AddWithValue("@Nome", nomeProprietario);
                    }
                    else
                    {
                        daProp.SelectCommand = new SqlCommand(sqlProprietarios, conn);
                    }
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
                        string nomeImob = row["Nome_Imobiliaria"].ToString();
                        if (string.IsNullOrEmpty(nomeImobiliaria) || nomeImobiliaria == nomeImob)
                        {
                            dtGrid.Columns.Add(nomeImob, typeof(bool));
                        }
                    }

                    // 5. Preencher linhas
                    foreach (DataRow propRow in dtProp.Rows)
                    {
                        DataRow newRow = dtGrid.NewRow();
                        newRow["ID_Proprietario"] = propRow["ID"];
                        newRow["Proprietario"] = propRow["Nome"];

                        foreach (DataRow imobRow in dtImob.Rows)
                        {
                            string nomeImob = imobRow["Nome_Imobiliaria"].ToString();
                            if (!string.IsNullOrEmpty(nomeImobiliaria) && nomeImobiliaria != nomeImob)
                                continue;
                            int idProp = (int)propRow["ID"];
                            int idImob = (int)imobRow["ID_Imobiliaria"];
                            bool vinculado = dtVinc.Select($"ID_Proprietario = {idProp} AND ID_Imobiliaria = {idImob}").Length > 0;
                            newRow[nomeImob] = vinculado;
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
                            if (!string.IsNullOrEmpty(nomeImobiliaria) && nomeImobiliaria != colName)
                                continue;
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
                        // Reforçar seleção múltipla após atualizar DataSource
                        dgvVinculos.MultiSelect = true;
                        dgvVinculos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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

        private async Task AplicarFiltroAsync()
        {
            string nomeProp = null;
            string nomeImob = null;
            if (Combo_Nome_Prop_Filtro_Vinculo.InvokeRequired)
            {
                Combo_Nome_Prop_Filtro_Vinculo.Invoke((MethodInvoker)(() => nomeProp = Combo_Nome_Prop_Filtro_Vinculo.SelectedItem as string));
            }
            else
            {
                nomeProp = Combo_Nome_Prop_Filtro_Vinculo.SelectedItem as string;
            }
            if (Combo_Imobiliaria_Filtro_Vinculo.InvokeRequired)
            {
                Combo_Imobiliaria_Filtro_Vinculo.Invoke((MethodInvoker)(() => nomeImob = Combo_Imobiliaria_Filtro_Vinculo.SelectedItem as string));
            }
            else
            {
                nomeImob = Combo_Imobiliaria_Filtro_Vinculo.SelectedItem as string;
            }
            await Carregar_VinculosAsync(nomeProp, nomeImob);
        }

        private void dgvVinculos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            string colName = dgvVinculos.Columns[e.ColumnIndex].Name;
            if (colName == "ID_Proprietario" || colName == "Proprietario") return;

            // --- LOTE POR COLUNA (já existente) ---
            if (dgvVinculos.SelectedRows.Count > 1 && dgvVinculos.Rows[e.RowIndex].Selected)
            {
                bool novoValor = !(Convert.ToBoolean(dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true);
                foreach (DataGridViewRow row in dgvVinculos.SelectedRows)
                {
                    if (row.IsNewRow) continue;
                    row.Cells[e.ColumnIndex].Value = novoValor;
                }
                dgvVinculos.EndEdit();
                return;
            }

            // --- LOTE POR LINHA (NOVO, só com Shift) ---
            if (Control.ModifierKeys.HasFlag(Keys.Shift))
            {
                if (dgvVinculos.SelectedCells.Count > 1)
                {
                    var selectedCells = dgvVinculos.SelectedCells.Cast<DataGridViewCell>()
                        .Where(cell => cell.RowIndex == e.RowIndex && cell.ColumnIndex != dgvVinculos.Columns["ID_Proprietario"].Index && cell.ColumnIndex != dgvVinculos.Columns["Proprietario"].Index)
                        .ToList();
                    if (selectedCells.Count > 1)
                    {
                        bool novoValor = !(Convert.ToBoolean(dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value) == true);
                        foreach (var cell in selectedCells)
                        {
                            dgvVinculos.Rows[e.RowIndex].Cells[cell.ColumnIndex].Value = novoValor;
                        }
                        dgvVinculos.EndEdit();
                        return;
                    }
                }
            }
            // Caso contrário, comportamento padrão: só altera o checkbox clicado
        }

        private void dgvVinculos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvVinculos.Columns[e.ColumnIndex].Name;
                if (colName == "ID_Proprietario" || colName == "Proprietario") return;

                int idProprietario = Convert.ToInt32(dgvVinculos.Rows[e.RowIndex].Cells["ID_Proprietario"].Value);
                bool vincular = Convert.ToBoolean(dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                // Remove duplicidade: se já existe alteração para esse proprietário e imobiliária, remove
                alteracoesPendentes.RemoveAll(a => a.IdProprietario == idProprietario && a.NomeImobiliaria == colName);
                // Adiciona a alteração atual
                alteracoesPendentes.Add(new AlteracaoVinculo
                {
                    IdProprietario = idProprietario,
                    NomeImobiliaria = colName,
                    Vincular = vincular
                });
                precisaAtualizar = true; // Marcar que precisa atualizar
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
            
        }

        private async void Vincular_Imob_Load(object sender, EventArgs e)
        {
            Preencher_ComboBox_Proprietarios();
            Preencher_ComboBox_Cidades();
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

        /// <summary>
        /// Criação de fitro
        /// Começando com as buscar de informações
        /// </summary>

        private List<string> Obter_Nomes_Imobiliarias_Filtro()
        {
            List<string> imobiliarias = new List<string>();

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT DISTINCT Nome_Imobiliaria FROM Imobiliaria ORDER BY Nome_Imobiliaria";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Nome_Imobiliaria"] != DBNull.Value)
                            {
                                imobiliarias.Add(reader["Nome_Imobiliaria"].ToString());
                            }
                        }
                    }
                }
            }
            return imobiliarias;
        }

        private List<string> Obter_Nomes_Proprietarios_Filtro()
        {
            List<string> proprietarios = new List<string>();

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT DISTINCT Nome FROM Proprietarios ORDER BY Nome";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Nome"] != DBNull.Value)
                            {
                                proprietarios.Add(reader["Nome"].ToString());
                            }
                        }
                    }
                }
            }
            return proprietarios;
        }

        private void Preencher_ComboBox_Proprietarios()
        {
            var nomes = Obter_Nomes_Proprietarios_Filtro();
            Combo_Nome_Prop_Filtro_Vinculo.Items.Clear();
            Combo_Nome_Prop_Filtro_Vinculo.Items.AddRange(nomes.ToArray());
        }

        private void Preencher_ComboBox_Cidades()
        {
            var Imobiliaria = Obter_Nomes_Imobiliarias_Filtro();
            Combo_Imobiliaria_Filtro_Vinculo.Items.Clear();
            Combo_Imobiliaria_Filtro_Vinculo.Items.AddRange(Imobiliaria.ToArray());
        }

        private async Task AplicarAlteracoesPendentesAsync()
        {
            if (alteracoesPendentes.Count == 0) return;
            SetStatus("Salvando alterações...", Color.Gold);
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                await conn.OpenAsync();
                foreach (var alt in alteracoesPendentes)
                {
                    int idImobiliaria = await BuscarIdImobiliariaAsync(alt.NomeImobiliaria);
                    if (alt.Vincular)
                        await VincularAsync(alt.IdProprietario, idImobiliaria);
                    else
                        await DesvincularAsync(alt.IdProprietario, idImobiliaria);
                }
            }
            alteracoesPendentes.Clear();
            SetStatus("Alterações salvas!", Color.LimeGreen);
        }

    }

}
