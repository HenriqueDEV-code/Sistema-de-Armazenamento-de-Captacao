using CapWeb.Captacao;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CapWeb;


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
            string Status;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (TestarConexao(DBA))
            {
                Status = "ON";
                Painel painel = new Painel(DBA, Status);
                Application.Run(painel); // inicia a aplicação com a tela de login
            }
            else
            {
                MessageBox.Show("Não foi possível conectar ao banco de dados. Verifique as configurações.", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Status = "OFF";
                Painel painel = new Painel(DBA, Status);
                Application.Run(painel); // inicia a aplicação com a tela de login
            }
        }


        private static bool TestarConexao(string conexaoString)
        {
            try
            {
                using (SqlConnection conexao = new SqlConnection(conexaoString))
                {
                    conexao.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}



