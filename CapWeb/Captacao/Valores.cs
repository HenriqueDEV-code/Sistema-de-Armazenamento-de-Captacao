using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapWeb.Captacao
{
    public partial class Valores : MaterialForm
    {
        private string DBA;
        public Valores(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
        }

        // <<< Novo método para detectar tecla pressionada
        private void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            // Acionar busca com F5
            if (e.KeyCode == Keys.F1)
            {
                Button_Pesquisar_DBA.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F2)
            {
                Pago.PerformClick();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();

                e.Handled = true;
            }

        }







        private void Quantidade_Total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 'R' && e.KeyChar != '$' && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Valor_Total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 'R' && e.KeyChar != '$' && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        // Formatao valor em Real
        private void FormatarMoeda(Guna.UI2.WinForms.Guna2TextBox txt)
        {
            if (txt.Text.Length < 4)
            {
                txt.Text = "R$ 0,00";
                txt.SelectionStart = txt.Text.Length;
                return;
            }

            string texto = txt.Text.Replace("R$", "").Replace(".", "").Replace(",", "").Trim();

            if (decimal.TryParse(texto, out decimal valor))
            {
                valor = valor / 100; // Mantém precisão dos centavos
                txt.Text = "R$ " + valor.ToString("N2", CultureInfo.GetCultureInfo("pt-BR"));
                txt.SelectionStart = txt.Text.Length;
            }
            else
            {
                txt.Text = "R$ 0,00";
                txt.SelectionStart = txt.Text.Length;
            }
        }

        private void FormatarMoeda_Enter(Guna.UI2.WinForms.Guna2TextBox txt)
        {
            if (string.IsNullOrWhiteSpace(txt.Text))
            {
                txt.Text = "R$ 0,00";
            }
            txt.SelectionStart = txt.Text.Length;
        }

        private void Valor_Total_Enter(object sender, EventArgs e)
        {
            FormatarMoeda_Enter(Valor_Total);
        }

        private void Valor_Total_TextChanged(object sender, EventArgs e)
        {
            Valor_Total.TextChanged -= Valor_Total_TextChanged;
            FormatarMoeda(Valor_Total);
            Valor_Total.TextChanged += Valor_Total_TextChanged;
        }

        private List<string> Obter_Nomes_Imobiliarias()
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

        private void Preencher_ComboBox_Imobiliarias()
        {
            var imobiliarias = Obter_Nomes_Imobiliarias();
            Combo_Lista_Imobiliarias.Items.Clear();
            Combo_Lista_Imobiliarias.Items.AddRange(imobiliarias.ToArray());
        }

        private void Valores_Load(object sender, EventArgs e)
        {
            // TODO: esta linha de código carrega dados na tabela 'cap_ImoveisDataSet.Proprietario_Imobiliaria'. Você pode movê-la ou removê-la conforme necessário.
            this.proprietario_ImobiliariaTableAdapter.Connection = new SqlConnection(DBA);
            this.proprietario_ImobiliariaTableAdapter.Fill(this.cap_ImoveisDataSet.Proprietario_Imobiliaria);
            Preencher_ComboBox_Imobiliarias();
            Combo_Lista_Imobiliarias.Text = "Selecione o nome da imobiliária.";
            
        }

        private void Button_Pesquisar_DBA_Click(object sender, EventArgs e)
        {

            if (Combo_Lista_Imobiliarias.SelectedItem != null)
            {
                Nome_Imov.Text = Combo_Lista_Imobiliarias.SelectedItem.ToString();
            }

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string sql = @"
            SELECT 
                pi.ID_Proprietario AS [Proprietários],
                pi.ID_Imobiliaria AS [Imobiliárias],
                pi.Data_Vinculo AS [Enviados],
                pi.Data_do_Pagamento AS [Recebido],
                pi.Valor,
                pi.Status
            FROM Proprietario_Imobiliaria pi
            INNER JOIN Imobiliaria i ON i.ID_Imobiliaria = pi.ID_Imobiliaria
            WHERE i.Nome_Imobiliaria = @Nome_Imobiliaria
              AND pi.Status = 'NAO PAGO'
        ";

                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                adapter.SelectCommand.Parameters.AddWithValue("@Nome_Imobiliaria", Nome_Imov.Text);

                DataTable tabela = new DataTable();
                adapter.Fill(tabela);
                DB_TABELA_VALORES_IMOBIIARIAS.DataSource = tabela;

                // --- CÁLCULO ---
                int countDatas = tabela.Rows.Count;
                decimal somaValor = 0;

                foreach (DataRow row in tabela.Rows)
                {
                    if (row["Valor"] != DBNull.Value)
                    {
                        somaValor += Convert.ToDecimal(row["Valor"]);
                    }
                }

                Quantidade_Total.Text = countDatas.ToString();
                Valor_Total.Text = somaValor.ToString("C2", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"));
            }
        }


        private void Pago_Click(object sender, EventArgs e)
        {
            // Permitir seleção múltipla
            DB_TABELA_VALORES_IMOBIIARIAS.MultiSelect = true;

            if (DB_TABELA_VALORES_IMOBIIARIAS.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma ou mais linhas para marcar como pagas.");
                return;
            }

            // Verifica se a data foi informada
            if (Date_Time_Pagamento.Value == null || Date_Time_Pagamento.Value == DateTime.MinValue)
            {
                MessageBox.Show("Informe a data de pagamento antes de marcar como pago.");
                return;
            }

            int totalAtualizados = 0;
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                foreach (DataGridViewRow row in DB_TABELA_VALORES_IMOBIIARIAS.SelectedRows)
                {
                    int idProprietario = Convert.ToInt32(row.Cells["Proprietários"].Value);
                    int idImobiliaria = Convert.ToInt32(row.Cells["Imobiliárias"].Value);

                    string sql = @"
            UPDATE Proprietario_Imobiliaria
            SET Status = 'PAGO', Data_do_Pagamento = @DataPagamento
            WHERE ID_Proprietario = @ID_Proprietario
              AND ID_Imobiliaria = @ID_Imobiliaria
              AND Status = 'NAO PAGO'
        ";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Proprietario", idProprietario);
                        cmd.Parameters.AddWithValue("@ID_Imobiliaria", idImobiliaria);
                        cmd.Parameters.AddWithValue("@DataPagamento", Date_Time_Pagamento.Value);

                        int linhasAfetadas = cmd.ExecuteNonQuery();
                        if (linhasAfetadas > 0)
                        {
                            totalAtualizados++;
                        }
                    }
                }
            }

            if (totalAtualizados > 0)
            {
                MessageBox.Show($"Status alterado para 'PAGO' em {totalAtualizados} registro(s) com sucesso.");
            }
            else
            {
                MessageBox.Show("Nenhum registro foi atualizado. Verifique se o status já está como 'PAGO'.");
            }

            // Recarrega a tabela após atualização
            Button_Pesquisar_DBA_Click(null, null);
        }
    } // Fim da classe Valores
} // FIm do namespace
