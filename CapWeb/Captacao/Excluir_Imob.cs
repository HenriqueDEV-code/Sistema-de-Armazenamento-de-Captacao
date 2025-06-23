using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MaterialSkin.Controls;

namespace CapWeb.Captacao
{
    public partial class Excluir_Imob : MaterialForm
    {
        private string DBA;
        public Excluir_Imob(string DBA)
        {
            InitializeComponent();
            this.DBA = DBA;
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
        }


        private async void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Delete)
            {
                Excluir.PerformClick();

                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();

                e.Handled = true;
            }
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

        private void Excluir_Click(object sender, EventArgs e)
        {
            if (Combo_Lista_Imobiliarias.SelectedItem == null)
            {
                MessageBox.Show("Selecione uma imobiliária para excluir.");
                return;
            }

            string nomeImobiliaria = Combo_Lista_Imobiliarias.SelectedItem.ToString();

            DialogResult confirmacao = MessageBox.Show(
                $"Tem certeza que deseja excluir a imobiliária '{nomeImobiliaria}'?\nOs vínculos com proprietários e outros registros serão removidos.",
                "Confirmação de Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirmacao != DialogResult.Yes)
                return;

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                SqlTransaction transacao = conn.BeginTransaction();

                try
                {
                    // 0. Remove vínculos da tabela intermediária Proprietario_Imobiliaria
                    string desvincularTabelaIntermediaria = @"
                DELETE FROM Proprietario_Imobiliaria
                WHERE ID_Imobiliaria IN (
                    SELECT ID_Imobiliaria FROM Imobiliaria WHERE Nome_Imobiliaria = @Nome
                )";

                    using (SqlCommand cmd = new SqlCommand(desvincularTabelaIntermediaria, conn, transacao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nomeImobiliaria);
                        cmd.ExecuteNonQuery();
                    }

                    // 1. Remove o vínculo dos proprietários (seta como NULL)
                    string desvincularProprietarios = @"
                UPDATE Proprietarios
                SET ID_Imobiliaria = NULL
                WHERE ID_Imobiliaria IN (
                    SELECT ID_Imobiliaria FROM Imobiliaria WHERE Nome_Imobiliaria = @Nome
                )";

                    using (SqlCommand cmd = new SqlCommand(desvincularProprietarios, conn, transacao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nomeImobiliaria);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Exclui a imobiliária
                    string excluirSQL = @"DELETE FROM Imobiliaria WHERE Nome_Imobiliaria = @Nome";

                    using (SqlCommand cmd = new SqlCommand(excluirSQL, conn, transacao))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nomeImobiliaria);
                        int linhasAfetadas = cmd.ExecuteNonQuery();

                        if (linhasAfetadas > 0)
                        {
                            transacao.Commit();
                            MessageBox.Show("Imobiliária excluída com sucesso.");
                            Preencher_ComboBox_Imobiliarias(); // Atualiza a lista
                        }
                        else
                        {
                            transacao.Rollback();
                            MessageBox.Show("Nenhuma imobiliária foi excluída.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    transacao.Rollback();
                    MessageBox.Show("Erro ao excluir imobiliária: " + ex.Message);
                }
            }
        }

        private void Excluir_Imob_Load(object sender, EventArgs e)
        {
            Preencher_ComboBox_Imobiliarias();
        }
    }
}
