using MaterialSkin.Controls;
using System;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Data.SqlClient;
using System.Data;

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
            atalho.Focus();
            ON_OFF.Text = Status;
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla
        }


        private void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            /*// Acionar busca com F5
            if (e.KeyCode == Keys.F5)
            {
                Button_Buscar_DBA.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }*/

            if (e.KeyCode == Keys.F1)
            {
                Cadastro.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F2)
            {
                Cadastro_Imobiliarias.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }


            if (e.KeyCode == Keys.F3)
            {
                Imobiliarias.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F4)
            {
                Detalhes.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F5)
            {
                AtualizarTotalProprietarios();
                PreencherDataGridFaltaDePagamento();
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F6)
            {
                Tabela.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }

            if (e.KeyCode == Keys.F7)
            {
                Financa.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F8)
            {
                Excluir.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F9)
            {
                Relatorio.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F10)
            {
                Observacao.PerformClick(); // Simula o clique do botão Buscar
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
        }





        private void Cadastro_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Cadastros janelaCadastro = new Cadastros(DBA);
            janelaCadastro.FormClosed += (s, args) => this.Show();
            janelaCadastro.Show();
        }

        private void Tabela_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Tabela janelaTabela = new Tabela(DBA);
            janelaTabela.FormClosed += (s, args) => this.Show();
            janelaTabela.Show();
        }

        private void Relatorio_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Relatorio janelaRelatorio = new Relatorio(DBA);
            janelaRelatorio.FormClosed += (s, args) => this.Show();
            janelaRelatorio.Show();
        }

        private void Cadastro_Imobiliarias_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Cadastro_Imobiliarias janelaImobiliarias = new Cadastro_Imobiliarias(DBA);
            janelaImobiliarias.FormClosed += (s, args) => this.Show();
            janelaImobiliarias.Show();
        }

        private void Detalhes_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Detalhes janeladetalhes = new Detalhes(DBA);
            janeladetalhes.FormClosed += (s, args) => this.Show();
            janeladetalhes.Show();
        }

        private void Imobiliarias_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Vincular_Imob janelaImobiliarias = new Vincular_Imob(DBA);
            janelaImobiliarias.FormClosed += (s, args) => this.Show();
            janelaImobiliarias.Show();
        }

        private void Financa_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Valores janelaValores = new Valores(DBA);
            janelaValores.FormClosed += (s, args) => this.Show();
            janelaValores.Show();
        }
        private void Excluir_Click(object sender, EventArgs e)
        {
            //this.Hide();

            Excluir_Imob janelaExcluir = new Excluir_Imob(DBA);
            janelaExcluir.FormClosed += (s, args) => this.Show();
            janelaExcluir.Show();
        }
        private void Observacao_Click(object sender, EventArgs e)
        {
            Observacao janelaObservacao = new Observacao(DBA);
            janelaObservacao.FormClosed += (s, args) => this.Show();
            janelaObservacao.Show();
        }


        private void Painel_Load(object sender, EventArgs e)
        {
            
            timerLoad = new Timer();
            timerLoad.Interval = 1000; // Tempo em milissegundos (1000ms = 1s)
            timerLoad.Tick += TimerLoad_Tick;
            timerLoad.Start();
            
            //PreencherDataGridFaltaDePagamento();
        }

        private void TimerLoad_Tick(object sender, EventArgs e)
        {
            VerificarConexao();
            AtualizarTotalProprietarios();
            //PreencherDataGridFaltaDePagamento();
        }

        private void PreencherDataGridFaltaDePagamento()
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = @"
                    SELECT 
                        i.ID_Imobiliaria AS [ID],
                        i.Nome_Imobiliaria AS [Nome],
                        COUNT(*) AS Quantidade,
                        SUM(pi.Valor) AS Valor,
                        MIN(pi.Data_Vinculo) AS [Data de envio],
                        pi.Status
                    FROM 
                        Proprietario_Imobiliaria pi
                    INNER JOIN 
                        Imobiliaria i ON pi.ID_Imobiliaria = i.ID_Imobiliaria
                    WHERE 
                        pi.Status = 'NAO PAGO'
                    GROUP BY 
                        i.ID_Imobiliaria, i.Nome_Imobiliaria, pi.Status
                ";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);
                        dataGridView_Falta_de_Pagamento.DataSource = dt;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao preencher a grid: " + ex.Message);
                    }
                }
            }
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
                        MessageBox.Show("Erro ao contar imobiliárias: " + ex.Message);
                    }
                }
            }
            return quantidade;
        }
        
        private int Contar_Vinculo()
        {
            int quantidade = 0;


            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT COUNT(*) FROM Proprietario_Imobiliaria";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        quantidade = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao contar os vinculos: " + ex.Message);
                    }
                }
            }
            return quantidade;
        }
        
        private int Nao_Vinculado()
        {
            int quantidade = 0;


            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = @"
    SELECT COUNT(*) 
    FROM Proprietarios p
    LEFT JOIN Proprietario_Imobiliaria pi ON p.ID = pi.ID_Proprietario
    WHERE pi.ID_Proprietario IS NULL
";


                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        quantidade = (int)cmd.ExecuteScalar();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao contar os vinculos: " + ex.Message);
                    }
                }
            }
            return quantidade;
        }


        private decimal Valores_Pagos()
        {
            decimal totalPago = 0;

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = @"
                    select sum(Valor) from Proprietario_Imobiliaria pi2 where pi2.Status = 'PAGO'
                ";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && decimal.TryParse(result.ToString(), out decimal valor))
                            totalPago = valor;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao calcular valores pagos: " + ex.Message);
                    }
                }
            }

            return totalPago;
        }

        private decimal Valores_Nao_Pagos()
        {
            decimal totalNaoPago = 0;

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = @"
                    select sum(Valor) from Proprietario_Imobiliaria pi2 where pi2.Status = 'NAO PAGO'
                ";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    try
                    {
                        conn.Open();
                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value && decimal.TryParse(result.ToString(), out decimal valor))
                            totalNaoPago = valor;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erro ao calcular valores não pagos: " + ex.Message);
                    }
                }
            }

            return totalNaoPago;
        }



        private void AtualizarTotalProprietarios()
        {
            int total = Contar_Proprietarios();
            int totalImob = Contar_Imobiliarias();
            int totalVinculo = Contar_Vinculo();
            int total_N_vinculado = Nao_Vinculado();
            decimal total_Pagos = Valores_Pagos();
            decimal total_N_Pagos = Valores_Nao_Pagos();
            LB_Imoveis_Cadastrados.Text = $"{total}";
            LB_Imobiliarias_Cadastradas.Text = $"{totalImob}";
            LB_Imoveis_Envi.Text = $"{totalVinculo}";
            LB_Imoveis_N_Envi.Text = $"{total_N_vinculado}";
            LB_Imov_Pago.Text = $"R$ {total_Pagos}";
            LB_Imov_nao_Pago.Text = $"R$ {total_N_Pagos}";
        }

        
    }
}