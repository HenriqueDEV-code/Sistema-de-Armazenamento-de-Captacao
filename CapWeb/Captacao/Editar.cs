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

// CEP
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;  // Não pode esquecer de instalar o pacote dele via NuGet
using System.Web.UI.Design.WebControls.WebParts;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Web.Util;

namespace CapWeb.Captacao
{
    public partial class Editar : MaterialForm
    {
        private string DBA;
        private int? idProprietarioAtual = null;
        public Editar(string DBA)
        {
            InitializeComponent();
            this.DBA = DBA;
            Nome_Prop.Focus();

            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
        }

        private async void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.F6)
            {
                Button_Salvar_DBA.PerformClick();

                e.Handled = true;
            }
            if (e.KeyCode == Keys.F5)
            {
                Buscar_Proprietario();

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

        private void Nome_Prop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Telefone_Prop_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void Informe_Cep_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Logradouro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Bairro_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Cidade_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UF_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void UF_TextChanged(object sender, EventArgs e)
        {
            if (UF.Text.Length > 2)
            {
                UF.Text = UF.Text.Substring(0, 2);
                UF.SelectionStart = UF.Text.Length; // Mantém o cursor no final
            }
        }

        private void numero_residencia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Complemento_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Valor_Imovel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 'R' && e.KeyChar != '$' && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Valor_IPTU_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void area_util1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Area_Total_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void area_construida_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void valor_codominio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }


        // Método genérico para formatação de moeda
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

        private void valor_codominio_Enter(object sender, EventArgs e)
        {
            FormatarMoeda_Enter(valor_codominio);
        }

        private void valor_codominio_TextChanged(object sender, EventArgs e)
        {
            valor_codominio.TextChanged -= valor_codominio_TextChanged;
            FormatarMoeda(valor_codominio);
            valor_codominio.TextChanged += valor_codominio_TextChanged;
        }

        private void Valor_Imovel_Enter(object sender, EventArgs e)
        {
            FormatarMoeda_Enter(Valor_Imovel);
        }

        private void Valor_Imovel_TextChanged(object sender, EventArgs e)
        {
            Valor_Imovel.TextChanged -= Valor_Imovel_TextChanged; // Evita loop
            FormatarMoeda(Valor_Imovel);
            Valor_Imovel.TextChanged += Valor_Imovel_TextChanged;
        }

        private void Valor_IPTU_Enter(object sender, EventArgs e)
        {
            FormatarMoeda_Enter(Valor_IPTU);
        }

        private void Valor_IPTU_TextChanged(object sender, EventArgs e)
        {
            Valor_IPTU.TextChanged -= Valor_IPTU_TextChanged;
            FormatarMoeda(Valor_IPTU);
            Valor_IPTU.TextChanged += Valor_IPTU_TextChanged;
        }

        private void Informe_Cep_TextChanged(object sender, EventArgs e)
        {
            // Remove tudo que não for número
            string texto = new string(Informe_Cep.Text.Where(char.IsDigit).ToArray());

            // Limita a 8 caracteres
            if (texto.Length > 8)
                texto = texto.Substring(0, 8);

            // Insere hífen após o 5º caractere, se houver
            if (texto.Length > 5)
                texto = texto.Insert(5, "-");

            // Atualiza o TextBox
            Informe_Cep.Text = texto;

            // Mantém o cursor no final
            Informe_Cep.SelectionStart = Informe_Cep.Text.Length;
        }

        private void Informe_Cep_Enter(object sender, EventArgs e)
        {
            if (Informe_Cep.Text == "")
            {
                Informe_Cep.Text = "_____-___";
                Informe_Cep.SelectionStart = 0;
            }
        }

        private void Informe_Cep_Leave(object sender, EventArgs e)
        {
            if (Informe_Cep.Text == "_____-___")
            {
                Informe_Cep.Text = "";
            }
            string cep = Informe_Cep.Text.Trim().Replace(" ", "").Replace("-", "");
            Informe_Cep.Text = cep; // Atualiza o texto no campo, se desejar
            Buscar_Cep_Click(sender, e);
        }

        private string LimparCep(string cep)
        {
            return cep.Trim().Replace(" ", "").Replace("-", "");
        }

        private async void Buscar_Cep_Click(object sender, EventArgs e)
        {
            string cep = LimparCep(Informe_Cep.Text);

            try
            {
                // Validação básica do CEP (8 dígitos numéricos)
                if (cep.Length != 8 || !Regex.IsMatch(cep, @"^\d{8}$"))
                {
                    MessageBox.Show("CEP inválido. Certifique-se de que contém exatamente 8 dígitos numéricos.",
                        "Erro no CEP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // URL da API ViaCEP
                string url = $"https://viacep.com.br/ws/{cep}/json/";

                using (HttpClient client = new HttpClient())
                {
                    // Enviar requisição e obter resposta
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    // Ler o JSON retornado
                    string jsonResponse = await response.Content.ReadAsStringAsync();

                    // Verifica se a API retornou algum erro
                    if (jsonResponse.Contains("\"erro\""))
                    {
                        MessageBox.Show("CEP não encontrado. Verifique o número informado.",
                            "Erro no CEP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Parse do JSON usando NewtonSoft.Json
                    JObject json = JObject.Parse(jsonResponse);

                    string logradouro = json["logradouro"]?.ToString() ?? "Informação não disponível";
                    string bairro = json["bairro"]?.ToString() ?? "Informação não disponível";
                    string localidade = json["localidade"]?.ToString() ?? "Informação não disponível";
                    string uf = json["uf"]?.ToString() ?? "Informação não disponível";

                    // Preencher os dados do CEP
                    Logradouro.Text = logradouro;
                    Bairro.Text = bairro;
                    Cidade.Text = localidade;
                    UF.Text = uf;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao buscar o endereço via CEP: {ex.Message}",
                    "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Telefone_Prop_TextChanged(object sender, EventArgs e)
        {
            // Salva a posição inicial do cursor
            int cursor = Telefone_Prop.SelectionStart;

            // Remove tudo que não for número
            string texto = new string(Telefone_Prop.Text.Where(char.IsDigit).ToArray());

            // Limita a 11 dígitos
            if (texto.Length > 11)
                texto = texto.Substring(0, 11);

            // Formata o telefone
            string telefoneFormatado = "";

            if (texto.Length <= 2)
            {
                telefoneFormatado = "(" + texto;
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
            if (Telefone_Prop.Text != telefoneFormatado)
            {
                Telefone_Prop.Text = telefoneFormatado;

                // Ajusta cursor para ignorar os caracteres fixos: '(', ')', espaço, '-'
                int contador = 0;

                for (int i = 0; i < telefoneFormatado.Length && contador < cursor; i++)
                {
                    if (char.IsDigit(telefoneFormatado[i]))
                        contador++;
                }

                // Posição final: onde está o contador
                Telefone_Prop.SelectionStart = contador + (telefoneFormatado.Length - texto.Length);
            }
        }


        private void Telefone_Prop_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Telefone_Prop.Text))
            {
                Telefone_Prop.Text = "(__) _____-____";
            }
            Telefone_Prop.SelectionStart = 1; // Coloca o cursor dentro do parêntese
        }
        private void Telefone_Prop_Leave(object sender, EventArgs e)
        {
            // Se não preencheu o telefone, limpa o campo
            string texto = new string(Telefone_Prop.Text.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(texto))
            {
                Telefone_Prop.Text = "";
            }
        }

        private void UF_Load(object sender, EventArgs e)
        {
            UF.CharacterCasing = CharacterCasing.Upper;
        }


        void limpe()
        {
            Nome_Prop.Clear();
            Telefone_Prop.Clear();
            Informe_Cep.Clear();
            Logradouro.Clear();
            Bairro.Clear();
            numero_residencia.Clear();
            Cidade.Clear();
            UF.Clear();
            Valor_Imovel.Clear();
            Valor_IPTU.Clear();
            descricao_cadastro.Clear();
            valor_codominio.Clear();
            area_util1.Clear();
            area_construida.Clear();
            Area_Total.Clear();
            nome_condominio.Clear();
            Complemento.Clear();
            Observacoes.Clear();
            Combo_Comissao.SelectedIndex = -1;
            Combo_Pretensao.SelectedIndex = -1;
            Combo_Tipo_de_imovel.SelectedIndex = -1;
        }

        bool Error_Nulos()
        {
            bool temErro = false;
            ERROR_Dados_Nulos.Clear();  // Limpa erros anteriores

            if (string.IsNullOrWhiteSpace(Nome_Prop.Text))
            {
                ERROR_Dados_Nulos.SetError(Nome_Prop, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Telefone_Prop.Text))
            {
                ERROR_Dados_Nulos.SetError(Telefone_Prop, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Combo_Tipo_de_imovel.Text))
            {
                ERROR_Dados_Nulos.SetError(Combo_Tipo_de_imovel, "Campo obrigatóro.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Combo_Pretensao.Text))
            {
                ERROR_Dados_Nulos.SetError(Combo_Pretensao, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Valor_Imovel.Text))
            {
                ERROR_Dados_Nulos.SetError(Valor_Imovel, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Combo_Comissao.Text))
            {
                ERROR_Dados_Nulos.SetError(Combo_Comissao, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Valor_IPTU.Text))
            {
                ERROR_Dados_Nulos.SetError(Valor_IPTU, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(descricao_cadastro.Text))
            {
                ERROR_Dados_Nulos.SetError(descricao_cadastro, "Campo obrigatório.");
                temErro = true;
            }

            return temErro;
        }

        private void Button_Salvar_DBA_Click(object sender, EventArgs e)
        {
            if (Error_Nulos())
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;  // Impede de salvar
            }
            
            if (idProprietarioAtual == null)
            {
                MessageBox.Show("Busque um proprietário antes de salvar alterações.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Atualiza Proprietario
                    string sqlProprietario = @"
                        UPDATE Proprietarios
                        SET Nome = @Nome, Telefone = @Telefone
                        WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(sqlProprietario, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Nome", Nome_Prop.Text);
                        cmd.Parameters.AddWithValue("@Telefone", Telefone_Prop.Text);
                        cmd.Parameters.AddWithValue("@ID", idProprietarioAtual.Value);
                        cmd.ExecuteNonQuery();
                    }

                    // Atualiza Endereco
                    // Primeiro, pegue o ID_Endereco do proprietário
                    int? idEndereco = null;
                    string sqlGetEndereco = "SELECT ID_Endereco FROM Proprietarios WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(sqlGetEndereco, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@ID", idProprietarioAtual.Value);
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && result != null)
                            idEndereco = Convert.ToInt32(result);
                    }

                    if (idEndereco != null)
                    {
                        string sqlEndereco = @"
                            UPDATE Endereco
                            SET Logradouro = @Logradouro, Numero = @Numero, Bairro = @Bairro, Cidade = @Cidade, UF = @UF, CEP = @CEP, Nome_Condominio = @Nome_Condominio
                            WHERE ID_End = @ID_End";
                        using (SqlCommand cmd = new SqlCommand(sqlEndereco, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@Logradouro", Logradouro.Text);
                            cmd.Parameters.AddWithValue("@Numero", string.IsNullOrWhiteSpace(numero_residencia.Text) ? (object)DBNull.Value : numero_residencia.Text);
                            cmd.Parameters.AddWithValue("@Bairro", Bairro.Text);
                            cmd.Parameters.AddWithValue("@Cidade", Cidade.Text);
                            cmd.Parameters.AddWithValue("@UF", UF.Text);
                            cmd.Parameters.AddWithValue("@CEP", Informe_Cep.Text);
                            cmd.Parameters.AddWithValue("@Nome_Condominio", nome_condominio.Text);
                            cmd.Parameters.AddWithValue("@ID_End", idEndereco.Value);
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // Atualiza Imovel (assumindo que só tem um imóvel por proprietário)
                    string sqlImovel = @"
                        UPDATE Imovel
                        SET Descricao = @Descricao, Observacao = @Observacao, Valor = @Valor, Tipo_de_Imovel = @Tipo_de_Imovel, Pretensao = @Pretensao, Comissao = @Comissao, 
                            Complemento = @Complemento, IPTU = @IPTU, Valor_Condominio = @Valor_Condominio, Util = @Util, Contruida = @Contruida, Terreno = @Terreno
                        WHERE ID_Proprietario = @ID_Proprietario";
                    using (SqlCommand cmd = new SqlCommand(sqlImovel, conn, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Descricao", descricao_cadastro.Text);
                        cmd.Parameters.AddWithValue("@Observacao", Observacoes.Text);
                        cmd.Parameters.AddWithValue("@Valor", Valor_Imovel.Text);
                        cmd.Parameters.AddWithValue("@Tipo_de_Imovel", Combo_Tipo_de_imovel.Text);
                        cmd.Parameters.AddWithValue("@Pretensao", Combo_Pretensao.Text);
                        cmd.Parameters.AddWithValue("@Comissao", Combo_Comissao.Text);
                        cmd.Parameters.AddWithValue("@Complemento", Complemento.Text);
                        cmd.Parameters.AddWithValue("@IPTU", Valor_IPTU.Text);
                        cmd.Parameters.AddWithValue("@Valor_Condominio", valor_codominio.Text);
                        cmd.Parameters.AddWithValue("@Util", area_util1.Text);
                        cmd.Parameters.AddWithValue("@Contruida", area_construida.Text);
                        cmd.Parameters.AddWithValue("@Terreno", Area_Total.Text);
                        cmd.Parameters.AddWithValue("@ID_Proprietario", idProprietarioAtual.Value);
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    MessageBox.Show("Cadastro alterado com sucesso!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show("Erro ao alterar cadastro: " + ex.Message);
                }
                limpe();
            }
        }

        // Busca o cadastro do proprietário pelo nome informado em Nome_Prop
        private void Buscar_Proprietario()
        {
            string nomeProprietario = Nome_Prop.Text.Trim();

            if (string.IsNullOrWhiteSpace(nomeProprietario))
            {
                MessageBox.Show("Digite o nome do proprietário para buscar.");
                return;
            }

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                try
                {
                    conn.Open();
                    string sql = @"
                        SELECT TOP 1 
                            p.ID,
                            p.Nome, p.Telefone, 
                            e.Logradouro, e.Numero, e.Bairro, e.Cidade, e.UF, e.CEP, e.Nome_Condominio,
                            i.Descricao, i.Observacao, i.Valor, i.Tipo_de_Imovel, i.Pretensao, i.Comissao, i.Complemento, i.IPTU, i.Valor_Condominio, i.Util, i.Contruida, i.Terreno
                        FROM Proprietarios p
                        LEFT JOIN Endereco e ON p.ID_Endereco = e.ID_End
                        LEFT JOIN Imovel i ON p.ID = i.ID_Proprietario
                        WHERE p.Nome = @Nome";

                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nomeProprietario);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idProprietarioAtual = Convert.ToInt32(reader["ID"]);
                                // Preenche os campos do formulário com os dados encontrados
                                Nome_Prop.Text = reader["Nome"]?.ToString();
                                Telefone_Prop.Text = reader["Telefone"]?.ToString();

                                Logradouro.Text = reader["Logradouro"]?.ToString();
                                numero_residencia.Text = reader["Numero"]?.ToString();
                                Bairro.Text = reader["Bairro"]?.ToString();
                                Cidade.Text = reader["Cidade"]?.ToString();
                                UF.Text = reader["UF"]?.ToString();
                                Informe_Cep.Text = reader["CEP"]?.ToString();
                                nome_condominio.Text = reader["Nome_Condominio"]?.ToString();

                                descricao_cadastro.Text = reader["Descricao"]?.ToString();
                                Observacoes.Text = reader["Observacao"]?.ToString();
                                Valor_Imovel.Text = reader["Valor"]?.ToString();
                                Combo_Tipo_de_imovel.Text = reader["Tipo_de_Imovel"]?.ToString();
                                Combo_Pretensao.Text = reader["Pretensao"]?.ToString();
                                Combo_Comissao.Text = reader["Comissao"]?.ToString();
                                Complemento.Text = reader["Complemento"]?.ToString();
                                Valor_IPTU.Text = reader["IPTU"]?.ToString();
                                valor_codominio.Text = reader["Valor_Condominio"]?.ToString();
                                area_util1.Text = reader["Util"]?.ToString();
                                area_construida.Text = reader["Contruida"]?.ToString();
                                Area_Total.Text = reader["Terreno"]?.ToString();

                                MessageBox.Show("Cadastro encontrado e carregado para edição.");
                            }
                            else
                            {
                                MessageBox.Show("Proprietário não encontrado.");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao buscar proprietário: " + ex.Message);
                }
            }
        }

    }
}

