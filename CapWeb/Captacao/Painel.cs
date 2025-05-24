using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CapWeb.Captacao
{
    public partial class Painel : MaterialForm
    {

        private string DBA;
        private string Status;
        
        public Painel(string DBA, string Status)
        {
            this.DBA = DBA;
            this.Status = Status;
            InitializeComponent();
            ON_OFF.Text = Status;
        }


        private void Cadastro_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cadastros Janela_Cadastro = new Cadastros(DBA);
            Janela_Cadastro.FormClosed += (s, args) =>
            {
                this.Show();
                
            };
                Janela_Cadastro.Show();
        }

        private void Tabela_Click(object sender, EventArgs e)
        {
            this.Hide();

            Tabela Janela_Tabela = new Tabela(DBA);
            Janela_Tabela.FormClosed += (s, args) => this.Show();
            Janela_Tabela.Show();
        }

        private void Relatorio_Click(object sender, EventArgs e)
        {
            this.Hide();

            Relatorio Janela_Relatorio = new Relatorio(DBA);
            Janela_Relatorio.FormClosed += (s, args) => this.Show();
            Janela_Relatorio.Show();
        }

        private void Cadastro_Imobiliarias_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cadastro_Imobiliarias Janela_Imobiliarias_ = new Cadastro_Imobiliarias(DBA);
            Janela_Imobiliarias_.FormClosed += (s, args) => this.Show();
            Janela_Imobiliarias_.Show();
        }

       
    }
}
