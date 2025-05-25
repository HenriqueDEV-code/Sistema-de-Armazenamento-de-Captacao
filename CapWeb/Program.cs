using CapWeb.Captacao;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CapWeb
{
    internal static class Program
    {
        private const string servidor = "26.31.45.203\\SQLEXPRESS"; //Ainda da falhas de conexao mesmo sendo pc local
        private const string usuario = "sa";
        private const string senha = "Sfc@196722";

        public static readonly string DBA = $"Data Source={servidor};Initial Catalog=Cap_Imoveis;Persist Security Info=True;User ID={usuario};Password={senha};Encrypt=False";

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
                Console.WriteLine($"Erro ao conectar: {ex.Message}");
                return false;
            }
        }
    }
}
