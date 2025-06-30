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

/// <summary>
/// Formulário para exibir e gerenciar a tabela de imóveis cadastrados.
/// Permite visualizar, carregar e excluir imóveis.
/// </summary>
namespace CapWeb.Captacao
{
    public partial class Tabela : MaterialForm
    {
        private string DBA; // String de conexão com o banco de dados

        /// <summary>
        /// Inicializa o formulário e associa eventos de teclado.
        /// </summary>
        public Tabela(string DBA)
        {
            this.DBA = DBA;
          
            InitializeComponent();
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
        }

        /// <summary>
        /// Captura teclas pressionadas, como F5 para buscar dados.
        /// </summary>
        private void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            // Acionar busca com F5
            if (e.KeyCode == Keys.F5)
            {
                Filtrar_Tabela();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();

                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                Excluir_Cadastro.PerformClick();

                e.Handled = true;
            }
            if (e.KeyCode == Keys.F1)
            {
                limpar();
                e.Handled = true;

            }   
            if (e.KeyCode == Keys.E)
            {
                Editar();

                e.Handled = true;
            }
        }

        /// <summary>
        /// Evento de carregamento do formulário, carrega a tabela inicial.
        /// </summary>
        private void Tabela_Load(object sender, EventArgs e)
        {
            Preencher_ComboBox_Pretensao();
            Preencher_ComboBox_TipoImovel();
            Carregar_Tabela();
        }

        /// <summary>
        /// Carrega os dados dos imóveis do banco e exibe no DataGridView.
        /// </summary>
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
        e.Numero AS [Número],
        e.Nome_Condominio AS [Nome do Condomínio],
        i.Descricao AS [Descrição],
        i.Observacao AS [Observações],
        i.Tipo_de_Imovel AS [Tipo de Imóvel],
        i.Pretensao AS [Pretensão],
        i.Valor,
        i.Comissao AS [Comissão],
        i.Complemento,
        i.IPTU,
        i.Valor_Condominio AS [Valor do Condomínio],
        i.Util AS [Área Útil],
        i.Contruida AS [Área Construída],
        i.Terreno AS [Área Terreno]
    FROM Proprietarios p
    INNER JOIN Imovel i ON i.ID_Proprietario = p.ID
    INNER JOIN Endereco e ON e.ID_End = i.ID_Endereco
    ORDER BY p.ID DESC
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

        /// <summary>
        /// Exclui o cadastro selecionado, removendo imóvel, endereço e proprietário.
        /// </summary>
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
                    MessageBox.Show("Cadastro excluído com sucesso.");
                    Carregar_Tabela(); // Atualiza o DataGridView
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erro ao excluir: " + ex.Message);
                }
            }
        }

        /// <summary>
        /// Sistema de filtro para facilitar a busca de imovel
        /// </summary>



        // Direcionar que tipo de informação será aceita nesse campo
        private void Nome_Prop_Filtro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Telefone_Filtro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void Telefone_Filtro_TextChanged(object sender, EventArgs e)
        {
            // Salva a posição inicial do cursor
            int cursor = Telefone_Filtro.SelectionStart;

            // Remove tudo que não for número
            string texto = new string(Telefone_Filtro.Text.Where(char.IsDigit).ToArray());

            // Limita a 11 dígitos
            if (texto.Length > 11)
                texto = texto.Substring(0, 11);

            // Formata o telefone
            string telefoneFormatado = "";

            if (texto.Length <= 2)
            {
                telefoneFormatado = "" + texto;
            }
            else if (texto.Length <= 6)
            {
                telefoneFormatado = "(" + texto.Substring(0, 2) + ") " + texto.Substring(2);
            }
            else if (texto.Length <= 10)
            {
                telefoneFormatado = "(" + texto.Substring(0, 2) + ") " + texto.Substring(2, 4) + "-" + texto.Substring(6);
            }
            else
            {
                telefoneFormatado = "(" + texto.Substring(0, 2) + ") " + texto.Substring(2, 5) + "-" + texto.Substring(7);
            }

            // Evita loop infinito: só atualiza se realmente mudou
            if (Telefone_Filtro.Text != telefoneFormatado)
            {
                Telefone_Filtro.Text = telefoneFormatado;

                // Ajusta cursor para ignorar os caracteres fixos: '(', ')', espaço, '-'
                int contador = 0;

                for (int i = 0; i < telefoneFormatado.Length && contador < cursor; i++)
                {
                    if (char.IsDigit(telefoneFormatado[i]))
                        contador++;
                }

                // Posição final: onde está o contador
                Telefone_Filtro.SelectionStart = contador + (telefoneFormatado.Length - texto.Length);
            }
        }

        private void Telefone_Filtro_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Telefone_Filtro.Text))
            {
                Telefone_Filtro.Text = "(__) _____-____";
            }
            Telefone_Filtro.SelectionStart = 1; // Coloca o cursor dentro do parêntese
        }

        private void Telefone_Filtro_Leave(object sender, EventArgs e)
        {
            // Se não preencheu o telefone, limpa o campo
            string texto = new string(Telefone_Filtro.Text.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(texto))
            {
                Telefone_Filtro.Text = "";
            }
        }

        private void Preencher_ComboBox_Pretensao()
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string sql = "SELECT DISTINCT Pretensao FROM Imovel ORDER BY Pretensao";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Combo_Pretensao_Filtro.Items.Clear();
                    while (reader.Read())
                    {
                        if (reader["Pretensao"] != DBNull.Value)
                            Combo_Pretensao_Filtro.Items.Add(reader["Pretensao"].ToString());
                    }
                }
                catch { }
            }
        }

        private void Preencher_ComboBox_TipoImovel()
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string sql = "SELECT DISTINCT Tipo_de_Imovel FROM Imovel ORDER BY Tipo_de_Imovel";
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    Combo_Tipo_Imovel_Filtro.Items.Clear();
                    while (reader.Read())
                    {
                        if (reader["Tipo_de_Imovel"] != DBNull.Value)
                            Combo_Tipo_Imovel_Filtro.Items.Add(reader["Tipo_de_Imovel"].ToString());
                    }
                }
                catch { }
            }
        }

        private void Filtrar_Tabela()
        {
            string nome = Nome_Prop_Filtro.Text.Trim();
            string telefone = Telefone_Filtro.Text.Trim();
            string bairro = Bairro_Filtro.Text.Trim();
            string pretensao = Combo_Pretensao_Filtro.SelectedItem != null ? Combo_Pretensao_Filtro.SelectedItem.ToString() : string.Empty;
            string tipoImovel = Combo_Tipo_Imovel_Filtro.SelectedItem != null ? Combo_Tipo_Imovel_Filtro.SelectedItem.ToString() : string.Empty;

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                var filtros = new List<string>();
                if (!string.IsNullOrEmpty(nome)) filtros.Add("p.Nome LIKE @nome");
                if (!string.IsNullOrEmpty(telefone)) filtros.Add("p.Telefone LIKE @telefone");
                if (!string.IsNullOrEmpty(bairro)) filtros.Add("e.Bairro LIKE @bairro");
                if (!string.IsNullOrEmpty(pretensao)) filtros.Add("i.Pretensao = @pretensao");
                if (!string.IsNullOrEmpty(tipoImovel)) filtros.Add("i.Tipo_de_Imovel = @tipoImovel");

                string where = filtros.Count > 0 ? ("WHERE " + string.Join(" AND ", filtros)) : "";

                string sql = $@"
                 SELECT 
        p.ID,
        p.Nome AS [Nome do Proprietário],
        p.Telefone,
        e.Cidade,
        e.Bairro,
        e.Logradouro,
        e.Numero AS [Número],
        e.Nome_Condominio AS [Nome do Condomínio],
        i.Descricao AS [Descrição],
        i.Observacao AS [Observações],
        i.Tipo_de_Imovel AS [Tipo de Imóvel],
        i.Pretensao AS [Pretensão],
        i.Valor,
        i.Comissao AS [Comissão],
        i.Complemento,
        i.IPTU,
        i.Valor_Condominio AS [Valor do Condomínio],
        i.Util AS [Área Útil],
        i.Contruida AS [Área Construída],
        i.Terreno AS [Área Terreno]
    FROM Proprietarios p
    INNER JOIN Imovel i ON i.ID_Proprietario = p.ID
    INNER JOIN Endereco e ON e.ID_End = i.ID_Endereco
    {where}
    ORDER BY p.ID DESC
                ";
                try
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    if (!string.IsNullOrEmpty(nome)) cmd.Parameters.AddWithValue("@nome", "%" + nome + "%");
                    if (!string.IsNullOrEmpty(telefone)) cmd.Parameters.AddWithValue("@telefone", "%" + telefone + "%");
                    if (!string.IsNullOrEmpty(bairro)) cmd.Parameters.AddWithValue("@bairro", "%" + bairro + "%");
                    if (!string.IsNullOrEmpty(pretensao)) cmd.Parameters.AddWithValue("@pretensao", pretensao);
                    if (!string.IsNullOrEmpty(tipoImovel)) cmd.Parameters.AddWithValue("@tipoImovel", tipoImovel);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tabela = new DataTable();
                    adapter.Fill(tabela);
                    dataGridViewInfo.DataSource = tabela;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao filtrar dados: " + ex.Message);
                }
            }
        }


        public void limpar()
        {
            Nome_Prop_Filtro.Clear();
            Telefone_Filtro.Clear();
            Bairro_Filtro.Clear();
            Combo_Pretensao_Filtro.SelectedIndex = -1;
            Combo_Tipo_Imovel_Filtro.SelectedIndex = -1;
            
        }



        // editar

        private void Editar()
        {
            if (dataGridViewInfo.SelectedRows.Count == 0)
            {
                MessageBox.Show("Selecione uma linha para editar o imóvel!");
                return;
            }

            
        }
    }
}
