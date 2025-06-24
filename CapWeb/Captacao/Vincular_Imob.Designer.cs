namespace CapWeb.Captacao
{
    partial class Vincular_Imob
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Vincular_Imob));
            this.dgvVinculos = new System.Windows.Forms.DataGridView();
            this.label6 = new System.Windows.Forms.Label();
            this.LB_Status = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.Combo_Imobiliaria_Filtro_Vinculo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.Combo_Nome_Prop_Filtro_Vinculo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculos)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvVinculos
            // 
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.dgvVinculos.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvVinculos.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvVinculos.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.dgvVinculos.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.Info;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVinculos.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvVinculos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.ControlLight;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvVinculos.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvVinculos.Location = new System.Drawing.Point(-1, 144);
            this.dgvVinculos.Name = "dgvVinculos";
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvVinculos.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvVinculos.Size = new System.Drawing.Size(1456, 647);
            this.dgvVinculos.TabIndex = 0;
            this.dgvVinculos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVinculos_CellValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(381, 29);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(77, 22);
            this.label6.TabIndex = 10;
            this.label6.Text = "Status - ";
            // 
            // LB_Status
            // 
            this.LB_Status.AutoSize = true;
            this.LB_Status.BackColor = System.Drawing.Color.Transparent;
            this.LB_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.LB_Status.ForeColor = System.Drawing.Color.White;
            this.LB_Status.Location = new System.Drawing.Point(470, 27);
            this.LB_Status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB_Status.Name = "LB_Status";
            this.LB_Status.Size = new System.Drawing.Size(17, 22);
            this.LB_Status.TabIndex = 11;
            this.LB_Status.Text = "*";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.panel1.Controls.Add(this.Combo_Imobiliaria_Filtro_Vinculo);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.Combo_Nome_Prop_Filtro_Vinculo);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Location = new System.Drawing.Point(-1, 50);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1456, 95);
            this.panel1.TabIndex = 12;
            // 
            // Combo_Imobiliaria_Filtro_Vinculo
            // 
            this.Combo_Imobiliaria_Filtro_Vinculo.BackColor = System.Drawing.Color.Transparent;
            this.Combo_Imobiliaria_Filtro_Vinculo.BorderColor = System.Drawing.Color.Black;
            this.Combo_Imobiliaria_Filtro_Vinculo.BorderRadius = 8;
            this.Combo_Imobiliaria_Filtro_Vinculo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combo_Imobiliaria_Filtro_Vinculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_Imobiliaria_Filtro_Vinculo.FillColor = System.Drawing.Color.Gainsboro;
            this.Combo_Imobiliaria_Filtro_Vinculo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Imobiliaria_Filtro_Vinculo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Imobiliaria_Filtro_Vinculo.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.Combo_Imobiliaria_Filtro_Vinculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Combo_Imobiliaria_Filtro_Vinculo.ItemHeight = 30;
            this.Combo_Imobiliaria_Filtro_Vinculo.Location = new System.Drawing.Point(584, 32);
            this.Combo_Imobiliaria_Filtro_Vinculo.Name = "Combo_Imobiliaria_Filtro_Vinculo";
            this.Combo_Imobiliaria_Filtro_Vinculo.Size = new System.Drawing.Size(281, 36);
            this.Combo_Imobiliaria_Filtro_Vinculo.TabIndex = 54;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label1.Location = new System.Drawing.Point(439, 32);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(137, 30);
            this.label1.TabIndex = 53;
            this.label1.Text = "Imobiliária:";
            // 
            // Combo_Nome_Prop_Filtro_Vinculo
            // 
            this.Combo_Nome_Prop_Filtro_Vinculo.BackColor = System.Drawing.Color.Transparent;
            this.Combo_Nome_Prop_Filtro_Vinculo.BorderColor = System.Drawing.Color.Black;
            this.Combo_Nome_Prop_Filtro_Vinculo.BorderRadius = 8;
            this.Combo_Nome_Prop_Filtro_Vinculo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combo_Nome_Prop_Filtro_Vinculo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_Nome_Prop_Filtro_Vinculo.FillColor = System.Drawing.Color.Gainsboro;
            this.Combo_Nome_Prop_Filtro_Vinculo.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Nome_Prop_Filtro_Vinculo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Nome_Prop_Filtro_Vinculo.Font = new System.Drawing.Font("Arial", 10.25F, System.Drawing.FontStyle.Bold);
            this.Combo_Nome_Prop_Filtro_Vinculo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Combo_Nome_Prop_Filtro_Vinculo.ItemHeight = 30;
            this.Combo_Nome_Prop_Filtro_Vinculo.Location = new System.Drawing.Point(188, 32);
            this.Combo_Nome_Prop_Filtro_Vinculo.Name = "Combo_Nome_Prop_Filtro_Vinculo";
            this.Combo_Nome_Prop_Filtro_Vinculo.Size = new System.Drawing.Size(246, 36);
            this.Combo_Nome_Prop_Filtro_Vinculo.TabIndex = 52;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label5.Location = new System.Drawing.Point(30, 32);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(153, 30);
            this.label5.TabIndex = 51;
            this.label5.Text = "Proprietário:";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.panel2.Controls.Add(this.label16);
            this.panel2.Location = new System.Drawing.Point(0, 977);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1941, 103);
            this.panel2.TabIndex = 13;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(11, 10);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(175, 88);
            this.label16.TabIndex = 44;
            this.label16.Text = " Atalhos:\r\n F5 - Atualizar tabela\r\n F6 - Aplicar o filtro\r\n ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(870, 26);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(702, 22);
            this.label2.TabIndex = 14;
            this.label2.Text = "Informação:  Antes de sair da janela, por favor atualize a tabela pelo F5 por seg" +
    "urança.\r\n";
            // 
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(79)))));
            this.panel3.Controls.Add(this.label3);
            this.panel3.Location = new System.Drawing.Point(-1, 791);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1456, 102);
            this.panel3.TabIndex = 15;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(11, 12);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(548, 88);
            this.label3.TabIndex = 44;
            this.label3.Text = " Atalhos:\r\n F5 - Atualizar a tabela      F6 - Aplicar o filtro    ESC - Fechar ja" +
    "nela\r\n\r\n ";
            // 
            // Vincular_Imob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1455, 894);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.LB_Status);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvVinculos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Vincular_Imob";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Envios de imóveis para as imobiliárias";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Vincular_Imob_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculos)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVinculos;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LB_Status;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2ComboBox Combo_Imobiliaria_Filtro_Vinculo;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox Combo_Nome_Prop_Filtro_Vinculo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label3;
    }
}