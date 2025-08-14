using CapWeb.Captacao;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapWeb
{
    internal static class Program
    {


        public static readonly string DBA = @"Data Source=Henrique\SQLEXPRESS;Initial Catalog=Cap_Imoveis;Integrated Security=True;Encrypt=False;TrustServerCertificate=True;";

        [STAThread]
        static void Main(string[] args)
        {
            string Status;
            
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Status = TestarConexao(DBA) ? "ON" : "OFF";

            if (Status == "OFF")
            {
                MessageBox.Show("Não foi possível conectar ao banco de dados. Verifique as configurações.", "Erro de Conexão", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Painel painel = new Painel(DBA, Status);
            Application.Run(painel);
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
                Console.WriteLine($"Erro ao conectar.");
                return false;
            }
        }
    }
}
