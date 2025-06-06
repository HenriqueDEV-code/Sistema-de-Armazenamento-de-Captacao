using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapWeb.Captacao
{
    public partial class Vincular_Imob : MaterialForm
    {
        private string DBA;
        public Vincular_Imob(string DBA)
        {
            this.DBA = DBA;
            InitializeComponent();
        }



        private void Carregar_Vinculos()
        {
            this.Text = "Carregando...";

            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();

                // 1. Obter lista de Imobiliárias
                string sqlImobiliarias = "SELECT ID_Imobiliaria, Nome_Imobiliaria FROM Imobiliaria";
                SqlDataAdapter daImob = new SqlDataAdapter(sqlImobiliarias, conn);
                DataTable dtImob = new DataTable();
                daImob.Fill(dtImob);

                // 2. Obter lista de Proprietários
                string sqlProprietarios = "SELECT ID, Nome FROM Proprietarios";
                SqlDataAdapter daProp = new SqlDataAdapter(sqlProprietarios, conn);
                DataTable dtProp = new DataTable();
                daProp.Fill(dtProp);

                // 3. Obter vinculações
                string sqlVinculos = "SELECT ID_Proprietario, ID_Imobiliaria FROM Proprietario_Imobiliaria";
                SqlDataAdapter daVinc = new SqlDataAdapter(sqlVinculos, conn);
                DataTable dtVinc = new DataTable();
                daVinc.Fill(dtVinc);

                // 4. Criar DataTable de exibição
                DataTable dtGrid = new DataTable();
                dtGrid.Columns.Add("ID_Proprietario", typeof(int));
                dtGrid.Columns.Add("Proprietario", typeof(string));

                // Adicionar colunas dinamicamente para cada imobiliária
                foreach (DataRow row in dtImob.Rows)
                {
                    string nomeImobiliaria = row["Nome_Imobiliaria"].ToString();
                    dtGrid.Columns.Add(nomeImobiliaria, typeof(bool));
                }

                // 5. Preencher linhas
                foreach (DataRow propRow in dtProp.Rows)
                {
                    DataRow newRow = dtGrid.NewRow();
                    newRow["ID_Proprietario"] = propRow["ID"];
                    newRow["Proprietario"] = propRow["Nome"];

                    foreach (DataRow imobRow in dtImob.Rows)
                    {
                        int idProp = (int)propRow["ID"];
                        int idImob = (int)imobRow["ID_Imobiliaria"];

                        bool vinculado = dtVinc.Select($"ID_Proprietario = {idProp} AND ID_Imobiliaria = {idImob}").Length > 0;

                        newRow[imobRow["Nome_Imobiliaria"].ToString()] = vinculado;
                    }

                    dtGrid.Rows.Add(newRow);
                }

                // 6. Bindar no DataGridView
                dgvVinculos.DataSource = dtGrid;

                // 7. Configurar CheckBox nas colunas de Imobiliária
                foreach (DataRow row in dtImob.Rows)
                {
                    string colName = row["Nome_Imobiliaria"].ToString();
                    if (!(dgvVinculos.Columns[colName] is DataGridViewCheckBoxColumn))
                    {
                        int colIndex = dgvVinculos.Columns[colName].Index;

                        dgvVinculos.Columns.RemoveAt(colIndex);

                        DataGridViewCheckBoxColumn chk = new DataGridViewCheckBoxColumn
                        {
                            Name = colName,
                            HeaderText = colName,
                            DataPropertyName = colName
                        };

                        dgvVinculos.Columns.Insert(colIndex, chk);
                    }
                }
            }

            this.Text = "Vinculação de imobiliárias";
        }

        private void dgvVinculos_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                string colName = dgvVinculos.Columns[e.ColumnIndex].Name;

                // Ignorar colunas que não são imobiliárias
                if (colName == "ID_Proprietario" || colName == "Proprietario") return;

                int idProprietario = Convert.ToInt32(dgvVinculos.Rows[e.RowIndex].Cells["ID_Proprietario"].Value);

                // Buscar ID da imobiliária pelo nome
                int idImobiliaria = BuscarIdImobiliaria(colName);

                bool vinculado = Convert.ToBoolean(dgvVinculos.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);

                if (vinculado)
                {
                    Vincular(idProprietario, idImobiliaria);
                    LB_Status.Text = $"Vinculado com sucesso!";
                }
                else
                {
                    Desvincular(idProprietario, idImobiliaria);
                    LB_Status.Text = $"Desvinculado com sucesso!";
                }
            }

        }
        private int BuscarIdImobiliaria(string nomeImobiliaria)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                string sql = "SELECT ID_Imobiliaria FROM Imobiliaria WHERE Nome_Imobiliaria = @nome";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@nome", nomeImobiliaria);
                    object result = cmd.ExecuteScalar();
                    if (result != null)
                    {
                        return Convert.ToInt32(result);
                    }
                    else
                    {
                        throw new Exception("Imobiliária não encontrada: " + nomeImobiliaria);
                    }
                }
            }
        }

        void Vincular(int idProprietario, int idImobiliaria)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                string sql = @"
            IF NOT EXISTS (SELECT 1 FROM Proprietario_Imobiliaria 
                           WHERE ID_Proprietario = @idP AND ID_Imobiliaria = @idI)
            BEGIN
                INSERT INTO Proprietario_Imobiliaria (ID_Proprietario, ID_Imobiliaria) 
                VALUES (@idP, @idI)
            END";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idP", idProprietario);
                    cmd.Parameters.AddWithValue("@idI", idImobiliaria);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        void Desvincular(int idProprietario, int idImobiliaria)
        {
            using (SqlConnection conn = new SqlConnection(DBA))
            {
                conn.Open();
                string sql = "DELETE FROM Proprietario_Imobiliaria WHERE ID_Proprietario = @idP AND ID_Imobiliaria = @idI";

                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@idP", idProprietario);
                    cmd.Parameters.AddWithValue("@idI", idImobiliaria);
                    cmd.ExecuteNonQuery();
                }
            }
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            Carregar_Vinculos();
        }


        private void Vincular_Imob_Load(object sender, EventArgs e)
        {
            Carregar_Vinculos();
            timer1.Start();
        }

       
    }
}
