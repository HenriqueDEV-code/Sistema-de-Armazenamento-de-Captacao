using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace CapWeb.Captacao
{
    public partial class Relatorio : MaterialForm
    {

        private string DBA;
       
        public Relatorio(string DBA)
        {
           this.DBA = DBA;
            InitializeComponent();
            this.KeyPreview = true; // <<< Permite que o formulário capture teclas
            this.KeyDown += new KeyEventHandler(this.Detalhes_KeyDown); // <<< Associa o evento de tecla

        }
        private void Detalhes_KeyDown(object sender, KeyEventArgs e)
        {
            // Acionar busca com F5
            if (e.KeyCode == Keys.F5)
            {
                PreencherRelatorio();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                Close();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.F1)
            {
                limpar();
                e.Handled = true;
            }
        }

        private List<string> Obter_Nomes_Imobiliarias()
        {
            List<string> imobiliarias = new List<string>();

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                string SQL = "SELECT DISTINCT Nome_Imobiliaria FROM Imobiliaria ORDER BY Nome_Imobiliaria";

                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            if (reader["Nome_Imobiliaria"] != DBNull.Value)
                            {
                                imobiliarias.Add(reader["Nome_Imobiliaria"].ToString());
                            }
                        }
                    }
                }
            }
            return imobiliarias;
        }

        private void Preencher_ComboBox_Imobiliarias()
        {
            var imobiliarias = Obter_Nomes_Imobiliarias();
            Combo_Lista_Imobiliarias.Items.Clear();
            Combo_Lista_Imobiliarias.Items.AddRange(imobiliarias.ToArray());
        }

        private void Relatorio_Load(object sender, EventArgs e)
        {
            Preencher_ComboBox_Imobiliarias();
            Combo_Lista_Imobiliarias.Text = "Selecione o nome da imobiliária.";
        }

        int GetWeekOfMonth(DateTime date)
        {
            var firstDay = new DateTime(date.Year, date.Month, 1);
            return ((date.Day - 1) / 7) + 1;
        }

        decimal limiteAnual = 81000m;
        decimal limiteMensal = 6750m;

        private void AtualizarLimites(decimal valorRecebidoMes)
        {
            LB_Limite_Anual.Text = $"R$ {limiteAnual:N2}";
            LB_Limite_Mensal.Text = $"R$ {limiteMensal:N2}";
            if (valorRecebidoMes > limiteMensal)
                LB_Limite_Ultrapassado.Text = $"R$ {(valorRecebidoMes - limiteMensal):N2}";
            else
                LB_Limite_Ultrapassado.Text = "R$ 0,00";
        }

        void PreencherRelatorio()
        {
            DateTime dataInicio = Data_Inicio.Value.Date;
            DateTime dataFim = Data_Final.Value.Date;
            string nomeImobiliaria = Combo_Lista_Imobiliarias.Text;
            bool filtrarImobiliaria = !string.IsNullOrWhiteSpace(nomeImobiliaria) && nomeImobiliaria != "Selecione o nome da imobiliária.";

            int? idImobiliaria = null;

            if (filtrarImobiliaria)
            {
                using (SqlConnection conn = new SqlConnection(DBA))
                {
                    conn.Open();
                    string sqlId = "SELECT TOP 1 ID_Imobiliaria FROM Imobiliaria WHERE Nome_Imobiliaria = @Nome";
                    using (SqlCommand cmd = new SqlCommand(sqlId, conn))
                    {
                        cmd.Parameters.AddWithValue("@Nome", nomeImobiliaria);
                        var result = cmd.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                            idImobiliaria = Convert.ToInt32(result);
                    }
                }
            }

            // Arrays para armazenar valores por semana
            decimal[] pagosSemana = new decimal[4];
            decimal[] naoPagosSemana = new decimal[4];
            decimal totalPagoMes = 0, totalNaoPagoMes = 0;

            string query = @"
                SELECT Data_Vinculo, Data_do_Pagamento, Valor, Status, ID_Imobiliaria
                FROM Proprietario_Imobiliaria
                WHERE 
                    ((Data_Vinculo BETWEEN @DataInicio AND @DataFim)
                     OR (Data_do_Pagamento BETWEEN @DataInicio AND @DataFim))
            ";
            if (filtrarImobiliaria && idImobiliaria.HasValue)
                query += " AND ID_Imobiliaria = @ID_Imobiliaria";

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@DataInicio", dataInicio);
                cmd.Parameters.AddWithValue("@DataFim", dataFim);
                if (filtrarImobiliaria && idImobiliaria.HasValue)
                    cmd.Parameters.AddWithValue("@ID_Imobiliaria", idImobiliaria.Value);

                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    string status = reader["Status"].ToString();
                    decimal valor = Convert.ToDecimal(reader["Valor"]);
                    DateTime dataVinculo = Convert.ToDateTime(reader["Data_Vinculo"]);
                    DateTime? dataPagamento = reader["Data_do_Pagamento"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["Data_do_Pagamento"]);

                    // Para valores pagos, considerar a semana do pagamento
                    if (status == "PAGO" && dataPagamento.HasValue)
                    {
                        int semana = GetWeekOfMonth(dataPagamento.Value) - 1;
                        if (semana >= 0 && semana < 4)
                            pagosSemana[semana] += valor;
                        totalPagoMes += valor;
                    }
                    // Para valores não pagos, considerar a semana do vínculo
                    else if (status == "NAO PAGO")
                    {
                        int semana = GetWeekOfMonth(dataVinculo) - 1;
                        if (semana >= 0 && semana < 4)
                            naoPagosSemana[semana] += valor;
                        totalNaoPagoMes += valor;
                    }
                }
            }

            // Preencher labels semanais
            LB_Pago_1.Text = $"R$ {pagosSemana[0]:N2}";
            LB_Nao_Pago_1.Text = $"R$ {naoPagosSemana[0]:N2}";
            LB_Pago_2.Text = $"R$ {pagosSemana[1]:N2}";
            LB_Nao_Pago_2.Text = $"R$ {naoPagosSemana[1]:N2}";
            LB_Pago_3.Text = $"R$ {pagosSemana[2]:N2}";
            LB_Nao_Pago_3.Text = $"R$ {naoPagosSemana[2]:N2}";
            LB_Pago_4.Text = $"R$ {pagosSemana[3]:N2}";
            LB_Nao_Pago_4.Text = $"R$ {naoPagosSemana[3]:N2}";

            // Preencher totais mensais
            LB_Pago_Mes.Text = $"R$ {totalPagoMes:N2}";
            LB_Nao_Pago_Mes.Text = $"R$ {totalNaoPagoMes:N2}";

            // Preencher limites e ultrapassagem
            AtualizarLimites(totalPagoMes);
        }

        public void limpar()
        {
           
           Combo_Lista_Imobiliarias.SelectedIndex = -1;

        }


    }
}
