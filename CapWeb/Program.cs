using CapWeb.Captacao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapWeb;
using Npgsql;

namespace CapWeb
{
    internal static class Program
    {

        public const string DBA = "Data Source=henrique\\SQLEXPRESS;Initial Catalog=Cap_Imoveis;Persist Security Info=True;User ID=sa;Password=Sfc@196722;Encrypt=False";

        /// <summary>
        /// Ponto de entrada principal para o aplicativo.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        { 

                Painel painel = new Painel(DBA);
                Application.Run(painel); // inicia a aplicação com a tela de login
        }
    }
}



