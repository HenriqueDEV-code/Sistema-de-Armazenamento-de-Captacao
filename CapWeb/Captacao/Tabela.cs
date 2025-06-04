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
            // TODO: esta linha de código carrega dados na tabela 'cap_Imoveis.Imovel'. Você pode movê-la ou removê-la conforme necessário.
            this.imovelTableAdapter.Fill(this.cap_Imoveis.Imovel);
            // TODO: esta linha de código carrega dados na tabela 'cap_Imoveis.Endereco'. Você pode movê-la ou removê-la conforme necessário.
            this.enderecoTableAdapter.Fill(this.cap_Imoveis.Endereco);
            // TODO: esta linha de código carrega dados na tabela 'cap_Imoveis.Proprietarios'. Você pode movê-la ou removê-la conforme necessário.
            this.proprietariosTableAdapter.Fill(this.cap_Imoveis.Proprietarios);

        }
    }
}
