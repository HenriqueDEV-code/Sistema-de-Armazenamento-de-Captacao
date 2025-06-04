using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data.SqlClient;

namespace CapWeb.Captacao
{
    public partial class Painel : MaterialForm
    {
        private string DBA;
        private string Status;
        private Timer timerLoad;

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

            Cadastros janelaCadastro = new Cadastros(DBA);
            janelaCadastro.FormClosed += (s, args) => this.Show();
            janelaCadastro.Show();
        }

        private void Tabela_Click(object sender, EventArgs e)
        {
            this.Hide();

            Tabela janelaTabela = new Tabela(DBA);
            janelaTabela.FormClosed += (s, args) => this.Show();
            janelaTabela.Show();
        }

        private void Relatorio_Click(object sender, EventArgs e)
        {
            this.Hide();

            Relatorio janelaRelatorio = new Relatorio(DBA);
            janelaRelatorio.FormClosed += (s, args) => this.Show();
            janelaRelatorio.Show();
        }

        private void Cadastro_Imobiliarias_Click(object sender, EventArgs e)
        {
            this.Hide();

            Cadastro_Imobiliarias janelaImobiliarias = new Cadastro_Imobiliarias(DBA);
            janelaImobiliarias.FormClosed += (s, args) => this.Show();
            janelaImobiliarias.Show();
        }

        private void Detalhes_Click(object sender, EventArgs e)
        {
            this.Hide();

            Detalhes janeladetalhes = new Detalhes(DBA);
            janeladetalhes.FormClosed += (s, args) => this.Show();
            janeladetalhes.Show();
        }





        private void Painel_Load(object sender, EventArgs e)
        {
            timerLoad = new Timer();
            timerLoad.Interval = 1000; // Tempo em milissegundos (1000ms = 1s)
            timerLoad.Tick += TimerLoad_Tick;
            timerLoad.Start();
            
        }

        private void TimerLoad_Tick(object sender, EventArgs e)
        {
            VerificarConexao();
            AtualizarTotalProprietarios();
        }

        /// <summary>
        /// Verifica se a conexão de internet e VPN estão ativas.
        /// </summary>
        /// <returns>True se ambas estiverem ativas; caso contrário, false.</returns>
        private bool VerificarConexao()
        {
            bool internetAtiva = TestarPing("8.8.8.8");    // Testa conexão com a internet
                    

            bool statusREDE = internetAtiva;

            // Atualiza visualmente os indicadores
            Conectado_VPN.Visible = statusREDE;
            Nao_Conectado_VPN.Visible = !statusREDE;

            return statusREDE;
        }

        /// <summary>
        /// Testa a conectividade com um endereço IP utilizando o protocolo ICMP (Ping).
        /// </summary>
        /// <param name="ip">Endereço IP a ser testado.</param>
        /// <returns>True se o ping for bem-sucedido; caso contrário, false.</returns>
        private bool TestarPing(string ip)
        {
            try
            {
                using (var ping = new Ping())
                {
                    var reply = ping.Send(ip, 1000);   // Timeout de 1 segundo
                    return reply.Status == IPStatus.Success;
                }
            }
            catch
            {
                return false;
            }
        }

        private void guna2Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private int Contar_Proprietarios()
        {
            int quantidade = 0;


            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT COUNT(*) FROM Proprietarios";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        quantidade = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao contar proprietários: " + ex.Message);
                    }
                }
            }
            return quantidade;
        }

        private int Contar_Imobiliarias()
        {
            int quantidade = 0;


            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT COUNT(*) FROM Imobiliaria";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        quantidade = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao contar proprietários: " + ex.Message);
                    }
                }
            }
            return quantidade;
        }


        private void AtualizarTotalProprietarios()
        {
            int total = Contar_Proprietarios();
            int totalImob = Contar_Imobiliarias();
            LB_Imoveis_Cadastrados.Text = $"{total}";
            LB_Imobiliarias_Cadastradas.Text = $"{totalImob}";
        }
    }
}