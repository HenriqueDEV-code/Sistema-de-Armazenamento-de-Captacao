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
using System.Data.SqlClient;


namespace CapWeb.Captacao
{
    public partial class Tabela : MaterialForm
    {
        private string DBA;
        public Tabela(string DBA)
        {
            this.DBA = DBA;
          
            InitializeComponent();
            
        }

        private void Tabela_Load(object sender, EventArgs e)
        {

            Carregar_Tabela();
        }

        private void Carregar_Tabela()
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string sql = @"
                 SELECT 
        p.ID,
        p.Nome AS [Nome do Proprietário],
        p.Telefone,
        e.Cidade,
        e.Bairro,
        e.Logradouro,
        e.Numero,
        e.Nome_Condominio AS [Nome do Condomínio],
        i.Descricao,
        i.Tipo_de_Imovel AS [Tipo de Imóvel],
        i.Pretensao,
        i.Valor,
        i.Comissao,
        i.Complemento,
        i.IPTU,
        i.Valor_Condominio AS [Valor do Condomínio],
        i.Util AS [Área Útil],
        i.Contruida AS [Área Construída],
        i.Terreno AS [Área Terreno]
     
    FROM Proprietarios p
    INNER JOIN Imovel i ON i.ID_Proprietario = p.ID
    INNER JOIN Endereco e ON e.ID_End = i.ID_Endereco
            ";

                try
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    dataGridViewInfo.DataSource = tabela;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                }
            }
        }


        private void Excluir_Cadastro_Click(object sender, EventArgs e)
        {
            if (dataGridViewInfo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma linha para excluir o imóvel!");
                return;
            }

            // Confirmação do usuário
            DialogResult confirm = MessageBox.Show(
                "Tem certeza que deseja excluir este cadastro? Essa ação é irreversível.",
                "Confirmação de Exclusão",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning
            );

            if (confirm != DialogResult.Yes)
            {
                return; // Cancela a exclusão
            }

            // Pega o ID do proprietário
            int idProprietario = Convert.ToInt32(dataGridViewInfo.SelectedRows[0].Cells["ID"].Value);

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();

                // Transação para garantir consistência
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Pega o ID do imóvel e do endereço relacionados ao proprietário
                    int idImovel = 0;
                    int idEndereco = 0;

                    string buscarIds = @"
                SELECT TOP 1 i.ID_Imovel, i.ID_Endereco
                FROM Imovel i
                WHERE i.ID_Proprietario = @ID_Proprietario
            ";

                    using (SqlCommand cmdBuscar = new SqlCommand(buscarIds, conn, transaction))
                    {
                        cmdBuscar.Parameters.AddWithValue("@ID_Proprietario", idProprietario);
                        using (SqlDataReader reader = cmdBuscar.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idImovel = reader.GetInt32(0);
                                idEndereco = reader.GetInt32(1);
                            }
                            else
                            {
                                MessageBox.Show("Nenhum imóvel encontrado para este proprietário.");
                                transaction.Rollback();
                                return;
                            }
                        }
                    }

                    // 1. Excluir o imóvel
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Imovel WHERE ID_Imovel = @ID", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", idImovel);
                        cmd.ExecuteNonQuery();
                    }

                    // 2. Excluir o endereço
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Endereco WHERE ID_End = @ID", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", idEndereco);
                        cmd.ExecuteNonQuery();
                    }

                    // 3. Excluir o proprietário
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Proprietarios WHERE ID = @ID", conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", idProprietario);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Proprietário, imóvel e endereço excluídos com sucesso!");
                    Carregar_Tabela(); // Atualiza o DataGridView
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }


        }
    }
}
