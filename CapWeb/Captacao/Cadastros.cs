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

// CEP
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using System.Linq.Expressions;  // Não pode esquecer de instalar o pacote dele via NuGet
using System.Web.UI.Design.WebControls.WebParts;

namespace CapWeb.Captacao
{
    public partial class Cadastros : MaterialForm
    {
        private string DBA;
        public Cadastros(string DBA)
        {
            this.DBA = DBA;
           
            InitializeComponent();
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

        private void Valor_Imovel_Enter(object sender, EventArgs e)
        {
            if (Valor_Imovel.Text == "")
            {
                Valor_Imovel.Text = "R$ 0,00"; 
            }
            Valor_Imovel.SelectionStart = Valor_Imovel.Text.Length;
        }

        private void Valor_Imovel_TextChanged(object sender, EventArgs e)
        {
            if (Valor_Imovel.Text.Length < 4)
            {
                Valor_Imovel.Text = "R$ 0,00";
                Valor_Imovel.SelectionStart = Valor_Imovel.Text.Length;
                return;
            }

            string texto = Valor_Imovel.Text.Replace("R$", "").Replace(",", "").Replace(".", "").Trim();

            if (decimal.TryParse(texto, out decimal valor))
            {
                valor = valor / 100; // mantem a precisao dos centavos
                Valor_Imovel.Text = "R$" + valor.ToString("N2");
                Valor_Imovel.SelectionStart = Valor_Imovel.Text.Length;
            }
            else
            {
                Valor_Imovel.Text = "R$ 0,00";
                Valor_Imovel.SelectionStart = Valor_Imovel.Text.Length;
            }
        }

        private void Valor_IPTU_TextChanged(object sender, EventArgs e)
        {
            if (Valor_IPTU.Text.Length < 4)
            {
                Valor_IPTU.Text = "R$ 0,00";
                Valor_IPTU.SelectionStart = Valor_IPTU.Text.Length;
                return;
            }

            string texto = Valor_IPTU.Text.Replace("R$", "").Replace(",", "").Replace(".", "").Trim();

            if (decimal.TryParse(texto, out decimal valor))
            {
                valor = valor / 100; // mantem a precisao dos centavos
                Valor_IPTU.Text = "R$" + valor.ToString("N2");
                Valor_IPTU.SelectionStart = Valor_IPTU.Text.Length;
            }
            else
            {
                Valor_IPTU.Text = "R$ 0,00";
                Valor_IPTU.SelectionStart = Valor_IPTU.Text.Length;
            }
        }

        private void Valor_IPTU_Enter(object sender, EventArgs e)
        {
            if (Valor_IPTU.Text == "")
            {
                Valor_IPTU.Text = "R$ 0,00";
            }
            Valor_IPTU.SelectionStart = Valor_IPTU.Text.Length;
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
    }
}
