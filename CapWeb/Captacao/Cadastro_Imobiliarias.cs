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

        private void Valor_Enter(object sender, EventArgs e)
        {
            if (Valor.Text == "")
            {
                Valor.Text = "R$ 0,00";
            }
            Valor.SelectionStart = Valor.Text.Length;
        }

        private void Valor_TextChanged(object sender, EventArgs e)
        {
            if (Valor.Text.Length < 4)
            {
                Valor.Text = "R$ 0,00";
                Valor.SelectionStart = Valor.Text.Length;
                return;
            }

            string texto = Valor.Text.Replace("R$", "").Replace(",", "").Replace(".", "").Trim();

            if (decimal.TryParse(texto, out decimal valor))
            {
                valor = valor / 100; // mantem a precisao dos centavos
                Valor.Text = "R$" + valor.ToString("N2");
                Valor.SelectionStart = Valor.Text.Length;
            }
            else
            {
                Valor.Text = "R$ 0,00";
                Valor.SelectionStart = Valor.Text.Length;
            }
        }
    }
}
