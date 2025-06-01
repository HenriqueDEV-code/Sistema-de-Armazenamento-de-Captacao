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
    public partial class Cadastros : MaterialForm
    {
        private string DBA;
        public Cadastros(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
            Nome_Prop.Focus();
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

        private void nome_condominio_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void area_util_KeyPress(object sender, KeyPressEventArgs e)
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

        private void Valor_IPTU_TextChanged(object sender, EventArgs e)
        {
            Valor_IPTU0.TextChanged -= Valor_IPTU_TextChanged;
            FormatarMoeda(Valor_IPTU0);
            Valor_IPTU0.TextChanged += Valor_IPTU_TextChanged;
        }

        private void Valor_IPTU_Enter(object sender, EventArgs e)
        {
            FormatarMoeda_Enter(Valor_IPTU0);
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
        private void Telefone_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Telefone_Prop.Text))
            {
                Telefone_Prop.Text = "(__) _____-____";
            }
            Telefone_Prop.SelectionStart = 1; // Coloca o cursor dentro do parêntese
        }

        private void Telefone_Leave(object sender, EventArgs e)
        {
            // Se não preencheu o telefone, limpa o campo
            string texto = new string(Telefone_Prop.Text.Where(char.IsDigit).ToArray());
            if (string.IsNullOrWhiteSpace(texto))
            {
                Telefone_Prop.Text = "";
            }
        }

        private void Nome_Prop_Load(object sender, EventArgs e)
        {
            Nome_Prop.CharacterCasing = CharacterCasing.Upper;
        }

        private void Logradouro_Load(object sender, EventArgs e)
        {
            Logradouro.CharacterCasing = CharacterCasing.Upper;

        }

        private void Bairro_Load(object sender, EventArgs e)
        {
            Bairro.CharacterCasing = CharacterCasing.Upper;

        }

        private void Cidade_Load(object sender, EventArgs e)
        {

            Cidade.CharacterCasing = CharacterCasing.Upper;
        }

        private void Complemento_Load(object sender, EventArgs e)
        {
            Complemento.CharacterCasing = CharacterCasing.Upper;

        }

        private void UF_Load(object sender, EventArgs e)
        {
            UF.CharacterCasing = CharacterCasing.Upper;

        }

        private void Descricao_Load(object sender, EventArgs e)
        {
            Descricao.CharacterCasing = CharacterCasing.Upper;

        }
        private void nome_condominio_Load(object sender, EventArgs e)
        {
            nome_condominio.CharacterCasing = CharacterCasing.Upper;
        }

        // -- Salvar Pessoas -- 


        public void SalvarPessoa(Pessoas pessoa, Endereco end, Imovel imoveis)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();

                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // -- Inserir Endereco --
                    string QUERY_ENDERECO = @"INSERT INTO Endereco (Logradouro, Numero, Bairro, Cidade, UF, CEP, Nome_Condominio)
                                      VALUES (@Logradouro, @Numero, @Bairro, @Cidade, @UF, @CEP, @Nome_Condominio);
                                      SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdEndereco = new SqlCommand(QUERY_ENDERECO, conn, transaction);
                    cmdEndereco.Parameters.AddWithValue("@Logradouro", end.Logradouro);
                    cmdEndereco.Parameters.AddWithValue("@Numero", end.Numero);
                    cmdEndereco.Parameters.AddWithValue("@Bairro", end.Bairro);
                    cmdEndereco.Parameters.AddWithValue("@Cidade", end.Cidade);
                    cmdEndereco.Parameters.AddWithValue("@UF", end.UF);
                    cmdEndereco.Parameters.AddWithValue("@CEP", end.CEP);
                    cmdEndereco.Parameters.AddWithValue("@Nome_Condominio", end.Nome_Condominio);

                    int idEndereco = Convert.ToInt32(cmdEndereco.ExecuteScalar());

                    // -- Inserir Proprietarios --
                    string QUERY_PROPRIETARIO = @"INSERT INTO Proprietarios (Nome, Telefone, ID_Endereco)
                                          VALUES (@Nome, @Telefone, @ID_Endereco);
                                          SELECT SCOPE_IDENTITY();";

                    SqlCommand cmdProprietarios = new SqlCommand(QUERY_PROPRIETARIO, conn, transaction);
                    cmdProprietarios.Parameters.AddWithValue("@Nome", pessoa.Nome);
                    cmdProprietarios.Parameters.AddWithValue("@Telefone", pessoa.Telefone);
                    cmdProprietarios.Parameters.AddWithValue("@ID_Endereco", idEndereco);

                    int idProprietario = Convert.ToInt32(cmdProprietarios.ExecuteScalar());

                    // -- Inserir Imovel --
                    string QUERY_IMOVEL = @"INSERT INTO Imovel (Descricao, Valor, Tipo_de_Imovel, Pretensao,Comissao, Complemento, IPTU, ID_Endereco, ID_Proprietario, Valor_Condominio, Util, Contruida)
                                    VALUES (@Descricao, @Valor, @Tipo_de_Imovel, @Pretensao, @Comissao, @Complemento, @IPTU, @ID_Endereco, @ID_Proprietario, @Valor_Condominio, @Util, @Contruida);";

                    SqlCommand cmdImovel = new SqlCommand(QUERY_IMOVEL, conn, transaction);
                    cmdImovel.Parameters.AddWithValue("@Descricao", imoveis.Descricao);
                    cmdImovel.Parameters.AddWithValue("@Valor", imoveis.Valor);
                    cmdImovel.Parameters.AddWithValue("@Tipo_de_Imovel", imoveis.Tipo_de_imovel);
                    cmdImovel.Parameters.AddWithValue("@Pretensao", imoveis.Pretensao);
                    cmdImovel.Parameters.AddWithValue("@Comissao", imoveis.Comissao);
                    cmdImovel.Parameters.AddWithValue("@Complemento", imoveis.Complemento);
                    cmdImovel.Parameters.AddWithValue("@IPTU", imoveis.IPTU);
                    cmdImovel.Parameters.AddWithValue("@ID_Endereco", idEndereco);
                    cmdImovel.Parameters.AddWithValue("@ID_Proprietario", idProprietario);
                    cmdImovel.Parameters.AddWithValue("@Valor_Condominio", imoveis.Valor_Condominio);
                    cmdImovel.Parameters.AddWithValue("@Util", imoveis.Util);
                    cmdImovel.Parameters.AddWithValue("@Contruida", imoveis.Construida);
                    

                    cmdImovel.ExecuteNonQuery();

                    transaction.Commit();

                    MessageBox.Show("Dados inseridos com sucesso!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Erro ao inserir dados: {ex.Message}");
                }
            }
        }

        void Clear()
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
            Valor_IPTU0.Clear();
            Descricao.Clear();
            valor_codominio.Clear();
            area_util1.Clear();
            area_construida.Clear();
            nome_condominio.Clear();
            Complemento.Clear();
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

            if (string.IsNullOrWhiteSpace(Informe_Cep.Text))
            {
                ERROR_Dados_Nulos.SetError(Informe_Cep, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(numero_residencia.Text))
            {
                ERROR_Dados_Nulos.SetError(numero_residencia, "Campo obrigatório.");
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

            if (string.IsNullOrWhiteSpace(Valor_IPTU0.Text))
            {
                ERROR_Dados_Nulos.SetError(Valor_IPTU0, "Campo obrigatório.");
                temErro = true;
            }

            if (string.IsNullOrWhiteSpace(Descricao.Text))
            {
                ERROR_Dados_Nulos.SetError(Descricao, "Campo obrigatório.");
                temErro = true;
            }
            if (string.IsNullOrWhiteSpace(area_util1.Text))
            {
                ERROR_Dados_Nulos.SetError(area_util1, "Campo obrigatório.");
                temErro=true;
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

            Pessoas pessoa = new Pessoas
            {
                Nome = Nome_Prop.Text,
                Telefone = Telefone_Prop.Text
            };

            Endereco endereco = new Endereco
            {
                Logradouro = Logradouro.Text,
                Numero = int.Parse(numero_residencia.Text),
                Bairro = Bairro.Text,
                Cidade = Cidade.Text,
                UF = UF.Text,
                CEP = Informe_Cep.Text,
                Nome_Condominio = nome_condominio.Text
            };

           

            Imovel imovel = new Imovel
            {
                Descricao = Descricao.Text,
                Valor = Valor_Imovel.Text,                // Ex: "R$ 1.234,56"
                Tipo_de_imovel = Combo_Tipo_de_imovel.Text,
                Pretensao = Combo_Pretensao.Text,
                Comissao = Combo_Comissao.Text,
                Complemento = Complemento.Text,
                IPTU = Valor_IPTU0.Text,                   // Ex: "R$ 987,65"
                Valor_Condominio = valor_codominio.Text,  // Ex: "R$ 432,10"
                Util = area_util1.Text,
                Construida = area_construida.Text
            };

            SalvarPessoa(pessoa, endereco, imovel);
            Clear();
        }

       
    }
}

