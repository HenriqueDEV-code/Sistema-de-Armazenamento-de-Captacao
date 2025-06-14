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
using System.Globalization;


namespace CapWeb.Captacao
{
    public partial class Cadastro_Imobiliarias : MaterialForm
    {

        private string DBA;
        public Cadastro_Imobiliarias(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
           
        }

        private void Nome_Imobiliaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Telefone_imobiliaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != '(' && e.KeyChar != ')' && e.KeyChar != '-')
            {
                e.Handled = true;
            }
        }

        private void Nome_Respon_Imobiliaria_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void Valor_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar) && e.KeyChar != 'R' && e.KeyChar != '$' && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void Telefone_imobiliaria_TextChanged(object sender, EventArgs e)
        {
            // Salva a posição inicial do cursor
            int cursor = Telefone_imobiliaria.SelectionStart;

            // Remove tudo que não for número
            string texto = new string(Telefone_imobiliaria.Text.Where(char.IsDigit).ToArray());

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
            if (Telefone_imobiliaria.Text != telefoneFormatado)
            {
                Telefone_imobiliaria.Text = telefoneFormatado;

                // Ajusta cursor para ignorar os caracteres fixos: '(', ')', espaço, '-'
                int contador = 0;

                for (int i = 0; i < telefoneFormatado.Length && contador < cursor; i++)
                {
                    if (char.IsDigit(telefoneFormatado[i]))
                        contador++;
                }

                // Posição final: onde está o contador
                Telefone_imobiliaria.SelectionStart = contador + (telefoneFormatado.Length - texto.Length);
            }
        }

        private void Telefone_imobiliaria_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Telefone_imobiliaria.Text))
            {
                Telefone_imobiliaria.Text = "(__) _____-____";
            }
            Telefone_imobiliaria.SelectionStart = 1; // Coloca o cursor dentro do parêntese
        }

        private void Nome_Imobiliaria_Load(object sender, EventArgs e)
        {
            Nome_Imobiliaria.CharacterCasing = CharacterCasing.Upper;
        }

        private void Nome_Respon_Imobiliaria_Load(object sender, EventArgs e)
        {
            Nome_Respon_Imobiliaria.CharacterCasing = CharacterCasing.Upper;
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

        private void Valor_Enter(object sender, EventArgs e)
        {
            FormatarMoeda_Enter(Valor);
        }

        private void Valor_TextChanged(object sender, EventArgs e)
        {
            Valor.TextChanged -= Valor_TextChanged;
            FormatarMoeda(Valor);
            Valor.TextChanged += Valor_TextChanged;
        }

        // -- Salvar Imobiliarias

        public void Salvar_Imobiliaria(Imobiliaria imob)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();

                try
                {
                    // Verificar se já existe uma imobiliária com o mesmo nome
                    string checarDuplicidade = @"
                SELECT COUNT(*) 
                FROM Imobiliaria 
                WHERE Nome_Imobiliaria = @Nome_Imobiliaria";

                    using (SqlCommand cmdCheck = new SqlCommand(checarDuplicidade, conn, transaction))
                    {
                        cmdCheck.Parameters.AddWithValue("@Nome_Imobiliaria", imob.Nome_Imobiliaria);
                        int qtd = Convert.ToInt32(cmdCheck.ExecuteScalar());

                        if (qtd > 0)
                        {
                            transaction.Rollback();
                            MessageBox.Show("Já existe uma imobiliária cadastrada com esse nome.", "Duplicidade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    // Inserir nova imobiliária
                    string QUERY_IMOBILIARIA = @"
                INSERT INTO Imobiliaria (Nome_Imobiliaria, Nome_Responsavel, Telefone_Imobiliaria, Valor_Cobrado)
                VALUES (@Nome_Imobiliaria, @Nome_Responsavel, @Telefone_Imobiliaria, @Valor_Cobrado);
                SELECT SCOPE_IDENTITY();";

                    using (SqlCommand cmdImobiliaria = new SqlCommand(QUERY_IMOBILIARIA, conn, transaction))
                    {
                        cmdImobiliaria.Parameters.AddWithValue("@Nome_Imobiliaria", imob.Nome_Imobiliaria);
                        cmdImobiliaria.Parameters.AddWithValue("@Nome_Responsavel", imob.Nome_Responsavel);
                        cmdImobiliaria.Parameters.AddWithValue("@Telefone_Imobiliaria", imob.Telefone_Imobiliaria);
                        cmdImobiliaria.Parameters.AddWithValue("@Valor_Cobrado", imob.Valor_cobrado);

                        int idImobiliaria = Convert.ToInt32(cmdImobiliaria.ExecuteScalar());
                    }

                    transaction.Commit();
                    MessageBox.Show("Dados inseridos com sucesso!");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show($"Erro ao inserir dados: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        void Clear()
        {
            Nome_Imobiliaria.Clear();
            Nome_Respon_Imobiliaria.Clear();
            Telefone_imobiliaria.Clear();
            Valor.Clear();
        }

        bool Error_Nulos()
        {
            bool temErro = false;
            ERROR_Dados_Nulos.Clear();  // Limpa erros anteriores

            if (string.IsNullOrWhiteSpace(Nome_Imobiliaria.Text))
            {
                ERROR_Dados_Nulos.SetError(Nome_Imobiliaria, "Campo obrigatório.");
                temErro = true;
            }
            if (string.IsNullOrWhiteSpace(Nome_Respon_Imobiliaria.Text))
            {
                ERROR_Dados_Nulos.SetError(Nome_Respon_Imobiliaria, "Campo obrigatório.");
                temErro = true;
            }
            if (string.IsNullOrWhiteSpace(Telefone_imobiliaria.Text))
            {
                ERROR_Dados_Nulos.SetError(Telefone_imobiliaria, "Campo obrigatório.");
                temErro = true;
            }
            if (string.IsNullOrWhiteSpace(Valor.Text))
            {
                ERROR_Dados_Nulos.SetError(Valor, "Campo obrigatório.");
                temErro = true;
            }

            return temErro;
        }



        private void Salvar_Click(object sender, EventArgs e)
        {
            if (Error_Nulos())
            {
                MessageBox.Show("Por favor, preencha todos os campos obrigatórios.");
                return;  // Impede de salvar
            }

            Imobiliaria imob = new Imobiliaria
            {
                Nome_Imobiliaria = Nome_Imobiliaria.Text,
                Nome_Responsavel = Nome_Respon_Imobiliaria.Text,
                Telefone_Imobiliaria = Telefone_imobiliaria.Text,
                Valor_cobrado = Valor.Text
            };

            Salvar_Imobiliaria(imob);
            Clear();

        }
    }
}
