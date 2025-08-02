using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Data.SqlClient;

namespace CapWeb.Captacao
{
    public partial class Observacao : MaterialForm
    {
        private string DBA;
        public Observacao(string DBA)
        {
            InitializeComponent();
            this.DBA = DBA;
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
            Combo_Titulo_Obs.SelectedIndexChanged += (s, e) => FiltrarTabelaObs();
            Obs_Data.ValueChanged += (s, e) => FiltrarTabelaObs();
            Obs_Data.ShowCheckBox = true;
        }


        private async void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F6)
            {

                 Salvar();
                e.Handled = true;
            } 
            if (e.KeyCode == Keys.F5)
            {

                PreencherCombo_Titulo_Obs();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {

                excluir_Observacao();
                e.Handled = true;
            }


            if (e.KeyCode == Keys.F1)
            {
                limpe();

                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();

                e.Handled = true;
            }
        }



        private void Titulo_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        void limpe()
        {
            Campo_Titulo.Clear();
            Campo_Descricao_Obs.Clear();
            Combo_Titulo_Obs.SelectedIndex = -1;
        }



        public void Inserir_Obs(Campo_De_Observacao obs)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();


                try
                {
                    // -- Inserir Dados de Observacao
                    string QUERY_OBS = @"INSERT INTO Observacao (Titulo, Observacao) VALUES (@Titulo, @Observacao); SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdOBS = new SqlCommand(QUERY_OBS, conn);
                    cmdOBS.Parameters.AddWithValue("@Titulo", obs.Titulo_Obs);
                    cmdOBS.Parameters.AddWithValue("@Observacao", obs.Descricao_Obs);

                    //int Id = Convert.ToInt32(cmdOBS.ExecuteScalar());

                    cmdOBS.ExecuteNonQuery();




                    MessageBox.Show("Observações salva com sucesso!");

                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erro ao adicionar observação: " + ex.Message);
                }

                conn.Close();
            }
        }


        bool Error_Nulos()
        {
            bool temERRO = false;

            ERROR_Dados_Nulos.Clear(); // Limpa qualquer erro anterior

            if (string.IsNullOrWhiteSpace(Campo_Titulo.Text) && string.IsNullOrWhiteSpace(Campo_Descricao_Obs.Text))
            {
                ERROR_Dados_Nulos.SetError(Campo_Titulo, "Campo obrigatório.");
                ERROR_Dados_Nulos.SetError(Campo_Descricao_Obs, "Campo obrigatório.");
                temERRO = true;
            }
            return temERRO;
        }


        private void Salvar()
        {
            if (Error_Nulos())
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;
            }

            Campo_De_Observacao ob = new Campo_De_Observacao
            {
                Titulo_Obs = Campo_Titulo.Text,
                Descricao_Obs = Campo_Descricao_Obs.Text
            };


            Inserir_Obs(ob);
            limpe();
           PreencherTabelaObs();
        }

        private void PreencherTabelaObs()
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string sql = "SELECT Titulo, Data, Observacao FROM Observacao";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView_Tabela_Obs.DataSource = dt;

                        // Ajusta os títulos das colunas
                        dataGridView_Tabela_Obs.Columns["Titulo"].HeaderText = "Título";
                        dataGridView_Tabela_Obs.Columns["Data"].HeaderText = "Data";
                        dataGridView_Tabela_Obs.Columns["Observacao"].HeaderText = "Observação";
                        dataGridView_Tabela_Obs.Columns["Observacao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                        /*
                        // Ajusta o tamanho das colunas
                        dataGridView_Tabela_Obs.Columns["Titulo"].Width = 120;
                        dataGridView_Tabela_Obs.Columns["Data"].Width = 80;
                        dataGridView_Tabela_Obs.Columns["Observacao"].Width = 350;
                        // Ou para ocupar o espaço disponível:
                        */
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao carregar observações: " + ex.Message);
                    }
                }
            }
        }

        private void Observacao_Load(object sender, EventArgs e)
        {
            PreencherTabelaObs();
            PreencherCombo_Titulo_Obs();
            FiltrarTabelaObs();
        }

        private void Combo_Titulo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FiltrarTabelaObs();
        }

        private void Data_obs_Data_ValueChanged(object sender, EventArgs e)
        {
            FiltrarTabelaObs();
        }

        private void FiltrarTabelaObs()
        {
            string titulo = Combo_Titulo_Obs.Text.Trim();
            DateTime dataSelecionada = Obs_Data.Value.Date;

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string sql = "SELECT Titulo, Data, Observacao FROM Observacao WHERE 1=1";
                if (!string.IsNullOrEmpty(titulo))
                    sql += " AND Titulo = @Titulo";
                if (Obs_Data.Checked) // Se o DateTimePicker permite desmarcar
                    sql += " AND CAST(Data AS DATE) = @Data";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    if (!string.IsNullOrEmpty(titulo))
                        cmd.Parameters.AddWithValue("@Titulo", titulo);
                    if (Obs_Data.Checked)
                        cmd.Parameters.AddWithValue("@Data", dataSelecionada);

                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView_Tabela_Obs.DataSource = dt;

                        // Ajusta os headers e larguras se quiser
                        dataGridView_Tabela_Obs.Columns["Titulo"].HeaderText = "Título";
                        dataGridView_Tabela_Obs.Columns["Data"].HeaderText = "Data";
                        dataGridView_Tabela_Obs.Columns["Observacao"].HeaderText = "Observação";
                        dataGridView_Tabela_Obs.Columns["Observacao"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao filtrar observações: " + ex.Message);
                    }
                }
            }
        }


        private List<string> Obter_Titulo_observacao()
        {
            List<string> titulos = new List<string>();

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT DISTINCT Titulo FROM Observacao";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Titulo"] != DBNull.Value)
                            {
                                titulos.Add(reader["Titulo"].ToString());
                            }
                        }
                    }
                }
            }
            return titulos;
        }



        private void PreencherCombo_Titulo_Obs()
        {
            var titulos = Obter_Titulo_observacao();
            Combo_Titulo_Obs.Items.Clear();
            Combo_Titulo_Obs.Items.AddRange(titulos.ToArray());
        }



        private void excluir_Observacao()
        {
            if (dataGridView_Tabela_Obs.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma linha para excluir a observação!");
                return;
            }

            DialogResult confirm = MessageBox.Show(
                "Tem certeza que deseja excluir esta informação? Essa ação é irreversível.",
                "Confirmação da Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
            {
                return;
            }

            string titulo = dataGridView_Tabela_Obs.SelectedRows[0].Cells["Titulo"].Value.ToString();

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                string sql = "DELETE FROM Observacao WHERE Titulo = @Titulo";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Titulo", titulo);
                    int linhasAfetadas = cmd.ExecuteNonQuery();

                    if (linhasAfetadas > 0)
                    {
                        MessageBox.Show("Observação excluída com sucesso.");
                        // Recarregue a tabela aqui ou remova a linha da grid, por exemplo:
                        dataGridView_Tabela_Obs.Rows.RemoveAt(dataGridView_Tabela_Obs.SelectedRows[0].Index);
                    }
                    else
                    {
                        MessageBox.Show("Nenhuma observação foi excluída. Verifique o título.");
                    }
                }
            }

        }


    }
}
