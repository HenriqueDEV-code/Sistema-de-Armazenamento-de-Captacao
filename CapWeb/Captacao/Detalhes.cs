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
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Security.AccessControl;
using System.Globalization;

namespace CapWeb.Captacao
{
    public partial class Detalhes : MaterialForm
    {
        private string DBA;
        private DataTable resultado;
        
        public Detalhes(string DBA)
        {
            InitializeComponent();
            this.DBA = DBA;
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
            Combo_Cidade_Busca.StartIndex = 1;

        }

        // <<< Novo método para detectar tecla pressionada
        private void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            // Acionar busca com F5
            if (e.KeyCode == Keys.F5)
            {
                Button_Buscar_DBA.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F6)
            {
                extrair.PerformClick();
                e.Handled = true;
            }
            
        }

        bool Error_Nulos()
        {
            bool temErro = false;
            ERROR_Dados_Nulos.Clear();  // Limpa erros anteriores

            if (string.IsNullOrWhiteSpace(Combo_Nome_Prop_Busca.Text))
            {
                ERROR_Dados_Nulos.SetError(Combo_Nome_Prop_Busca, "Campo obrigatório.");
                temErro = true;
            }
            if (string.IsNullOrWhiteSpace(Combo_Cidade_Busca.Text))
            {
                ERROR_Dados_Nulos.SetError(Combo_Cidade_Busca, "Campo obrigatório.");
                temErro = true;
            }

            return temErro;
        }

        private void Detalhes_Busca_Load(object sender, EventArgs e)
        {
            Detalhes_Busca.CharacterCasing = CharacterCasing.Upper;

        }

        private List<string> ObterNomesProprietarios()
        {
            List<string> nomes = new List<string>();

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
                                nomes.Add(reader["Nome"].ToString());
                            }
                        }
                    }
                }
            }

            return nomes;
        }


        private List<string> ObertCidades()
        {
            List<string> cidades = new List<string>();

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT DISTINCT Cidade FROM Endereco ORDER BY Cidade";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read()) 
                        {
                            if (reader["Cidade"] != DBNull.Value)
                            {
                                cidades.Add(reader["Cidade"].ToString());
                            }
                        }
                    }
                }
            }
            return cidades;
        }

        private void Preencher_ComboBox_Proprietarios()
        {
            var nomes = ObterNomesProprietarios();
            Combo_Nome_Prop_Busca.Items.Clear();
            Combo_Nome_Prop_Busca.Items.AddRange(nomes.ToArray());
        }


        private void Preencher_ComboBox_Cidades()
        {
            var cidades = ObertCidades();
            Combo_Cidade_Busca.Items.Clear();
            Combo_Cidade_Busca.Items.AddRange(cidades.ToArray());
        }
        private void Detalhes_Load(object sender, EventArgs e)
        {
            Preencher_ComboBox_Cidades();
            Preencher_ComboBox_Proprietarios();

            Combo_Cidade_Busca.Text = "Selecione a cidade";
            Combo_Nome_Prop_Busca.Text = "Selecione o nome";

        }

       private DataTable FiltrarDetalhes(string nome, string cidade)
        {
            DataTable dt = new DataTable();
            
            using (SqlConnection con = new SqlConnection(DBA))
            {
                string SQL = @"
                                SELECT 
                P.Nome AS Nome_Proprietario,
                P.Telefone,
                E_Prop.Logradouro AS Logradouro_Proprietario,
                E_Prop.Numero AS Numero_Proprietario,
                E_Prop.Bairro AS Bairro_Proprietario,
                E_Prop.Cidade AS Cidade_Proprietario,
                E_Prop.UF AS UF_Proprietario,
                E_Prop.CEP AS CEP_Proprietario,
                E_Prop.Nome_Condominio AS NOME_Condominio,
                I.Descricao,
                I.Valor,
                I.Tipo_de_Imovel,
                I.Pretensao,
                I.Comissao,
                I.Complemento AS Complemento_Imovel,
                I.IPTU,
                I.Util AS util,
                I.Contruida AS construida,
                I.Terreno AS terreno,
                I.Valor_Condominio AS valor_Cond,
                E_Imovel.Logradouro AS Logradouro_Imovel,
                E_Imovel.Numero AS Numero_Imovel,
                E_Imovel.Bairro AS Bairro_Imovel,
                E_Imovel.Cidade AS Cidade_Imovel,
                E_Imovel.UF AS UF_Imovel,
                E_Imovel.CEP AS CEP_Imovel
            FROM Proprietarios P
            INNER JOIN Endereco E_Prop ON P.ID_Endereco = E_Prop.ID_End
            INNER JOIN Imovel I ON P.ID = I.ID_Proprietario
            INNER JOIN Endereco E_Imovel ON I.ID_Endereco = E_Imovel.ID_End
            WHERE P.Nome LIKE @nome AND E_Prop.Cidade = @cidade;
                              ";
                using (SqlCommand cmd = new SqlCommand(SQL, con))
                {
                    cmd.Parameters.AddWithValue("@nome", "%" + nome);
                    cmd.Parameters.AddWithValue("@cidade", cidade);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }
            return dt;
        }


        private void Button_Buscar_DBA_Click(object sender, EventArgs e)
        {
            if (Error_Nulos())
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;  // Impede de salvar
            }
            string nome = Combo_Nome_Prop_Busca.Text;
            string cidade = Combo_Cidade_Busca.SelectedItem.ToString();

            resultado = FiltrarDetalhes(nome, cidade);

            if (resultado.Rows.Count > 0)
            {
                Detalhes_Busca.Text = MontarDetalhes(resultado);
            }
            else
            {
                Detalhes_Busca.Text = "Nenhum resultado encontrado.";
            }

        }

        private void extrair_Click(object sender, EventArgs e)
        {
            if (Error_Nulos())
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;  // Impede de salvar
            }

            if (resultado == null || resultado.Rows.Count == 0)
            {
                MessageBox.Show("Nenhum dado para exportar!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Arquivo de texto (*.txt)|*.txt";
            saveFileDialog.Title = "Salvar Arquivo de Detalhes";
            saveFileDialog.FileName = "detalhes_imovel.txt";

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    string detalhes = MontarDetalhes(resultado);

                    System.IO.File.WriteAllText(saveFileDialog.FileName, detalhes);

                    MessageBox.Show("Arquivo exportado com sucesso!", "Exportação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao exportar: " + ex.Message);
                }
            }
        }

        // Método para montar os detalhes, evita código duplicado
        private string MontarDetalhes(DataTable tabela)
        {
            StringBuilder detalhes = new StringBuilder();

            foreach (DataRow row in tabela.Rows)
            {
                detalhes.AppendLine(new string('=', 100));
               
                detalhes.AppendLine("Proprietário:");
                detalhes.AppendLine(row["Nome_Proprietario"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Telefone:");
                detalhes.AppendLine(row["Telefone"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Endereço Completo:");
                detalhes.AppendLine($"{row["Logradouro_Proprietario"]} - {row["Numero_Proprietario"]}");
                detalhes.AppendLine($"{row["Bairro_Proprietario"]}");
                detalhes.AppendLine($"{row["Cidade_Proprietario"]}");
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Nome do condomínio:");
                detalhes.AppendLine(row["NOME_Condominio"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Complemento (quadra e lote/apartamento/casa):");
                detalhes.AppendLine(row["Complemento_Imovel"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Descrição do Imóvel:");
                detalhes.AppendLine(row["Descricao"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Tamanho da área (útil | construída | terreno):");
                detalhes.AppendLine($"Área útil: {row["util"]} m² | Área construída: {row["construida"]} m² | Área total: {row["terreno"]} m²");
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Valor do condomínio:");
                detalhes.AppendLine(row["valor_Cond"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("IPTU:");
                detalhes.AppendLine(row["IPTU"].ToString());
                detalhes.AppendLine();
                detalhes.AppendLine(new string('-', 100));

                detalhes.AppendLine("Valor do imóvel com ou sem comissão:");
                detalhes.AppendLine($"{row["Valor"]} - {row["Comissao"]}");
                detalhes.AppendLine($"{row["Tipo_de_Imovel"]} - {row["Pretensao"]}");
                detalhes.AppendLine();
                detalhes.AppendLine(new string('=', 100));
                detalhes.AppendLine();
            }

            return detalhes.ToString();
        }

       
    }
}
