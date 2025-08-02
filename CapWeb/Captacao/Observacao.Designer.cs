namespace CapWeb.Captacao
{
    partial class Observacao
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Observacao));
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.Pag_add_Observacao = new System.Windows.Forms.TabPage();
            this.atalho = new System.Windows.Forms.Label();
            this.Campo_Descricao_Obs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.Campo_Titulo = new Guna.UI2.WinForms.Guna2TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Pag_Visualizar_Observacao = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.guna2GroupBox1 = new Guna.UI2.WinForms.Guna2GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.Obs_Data = new System.Windows.Forms.DateTimePicker();
            this.Combo_Titulo_Obs = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView_Tabela_Obs = new System.Windows.Forms.DataGridView();
            this.ERROR_Dados_Nulos = new System.Windows.Forms.ErrorProvider(this.components);
            this.tabControl1.SuspendLayout();
            this.Pag_add_Observacao.SuspendLayout();
            this.Pag_Visualizar_Observacao.SuspendLayout();
            this.panel1.SuspendLayout();
            this.guna2GroupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Tabela_Obs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERROR_Dados_Nulos)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.Pag_add_Observacao);
            this.tabControl1.Controls.Add(this.Pag_Visualizar_Observacao);
            this.tabControl1.Location = new System.Drawing.Point(-2, 65);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1303, 499);
            this.tabControl1.TabIndex = 0;
            // 
            // Pag_add_Observacao
            // 
            this.Pag_add_Observacao.AutoScroll = true;
            this.Pag_add_Observacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Pag_add_Observacao.Controls.Add(this.atalho);
            this.Pag_add_Observacao.Controls.Add(this.Campo_Descricao_Obs);
            this.Pag_add_Observacao.Controls.Add(this.label2);
            this.Pag_add_Observacao.Controls.Add(this.Campo_Titulo);
            this.Pag_add_Observacao.Controls.Add(this.label6);
            this.Pag_add_Observacao.Location = new System.Drawing.Point(4, 22);
            this.Pag_add_Observacao.Name = "Pag_add_Observacao";
            this.Pag_add_Observacao.Padding = new System.Windows.Forms.Padding(3);
            this.Pag_add_Observacao.Size = new System.Drawing.Size(1295, 473);
            this.Pag_add_Observacao.TabIndex = 0;
            this.Pag_add_Observacao.Text = "Adicionar alguma observação";
            // 
            // atalho
            // 
            this.atalho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.atalho.AutoSize = true;
            this.atalho.BackColor = System.Drawing.Color.Transparent;
            this.atalho.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.atalho.ForeColor = System.Drawing.Color.Yellow;
            this.atalho.Location = new System.Drawing.Point(8, 419);
            this.atalho.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.atalho.Name = "atalho";
            this.atalho.Size = new System.Drawing.Size(489, 44);
            this.atalho.TabIndex = 62;
            this.atalho.Text = " Atalhos:\r\n F1 - Limpa tudo     F6 - Salva tudo     ESC - Fechar a janela";
            // 
            // Campo_Descricao_Obs
            // 
            this.Campo_Descricao_Obs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Campo_Descricao_Obs.BackColor = System.Drawing.Color.Gainsboro;
            this.Campo_Descricao_Obs.Font = new System.Drawing.Font("Arial", 18.25F);
            this.Campo_Descricao_Obs.Location = new System.Drawing.Point(164, 123);
            this.Campo_Descricao_Obs.Multiline = true;
            this.Campo_Descricao_Obs.Name = "Campo_Descricao_Obs";
            this.Campo_Descricao_Obs.Size = new System.Drawing.Size(1005, 229);
            this.Campo_Descricao_Obs.TabIndex = 61;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label2.Location = new System.Drawing.Point(24, 123);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(135, 30);
            this.label2.TabIndex = 60;
            this.label2.Text = "Descrição:";
            // 
            // Campo_Titulo
            // 
            this.Campo_Titulo.Animated = true;
            this.Campo_Titulo.AutoRoundedCorners = true;
            this.Campo_Titulo.BorderColor = System.Drawing.Color.Black;
            this.Campo_Titulo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Campo_Titulo.DefaultText = "";
            this.Campo_Titulo.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Campo_Titulo.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Campo_Titulo.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Campo_Titulo.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Campo_Titulo.FillColor = System.Drawing.Color.Gainsboro;
            this.Campo_Titulo.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Campo_Titulo.Font = new System.Drawing.Font("Arial", 14.25F);
            this.Campo_Titulo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Campo_Titulo.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Campo_Titulo.Location = new System.Drawing.Point(185, 54);
            this.Campo_Titulo.Margin = new System.Windows.Forms.Padding(6);
            this.Campo_Titulo.Name = "Campo_Titulo";
            this.Campo_Titulo.PlaceholderForeColor = System.Drawing.Color.Black;
            this.Campo_Titulo.PlaceholderText = "Título da observação";
            this.Campo_Titulo.SelectedText = "";
            this.Campo_Titulo.Size = new System.Drawing.Size(554, 41);
            this.Campo_Titulo.TabIndex = 57;
            this.Campo_Titulo.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Titulo_KeyPress);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label6.Location = new System.Drawing.Point(76, 56);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 30);
            this.label6.TabIndex = 58;
            this.label6.Text = "Título:";
            // 
            // Pag_Visualizar_Observacao
            // 
            this.Pag_Visualizar_Observacao.AutoScroll = true;
            this.Pag_Visualizar_Observacao.Controls.Add(this.panel1);
            this.Pag_Visualizar_Observacao.Location = new System.Drawing.Point(4, 22);
            this.Pag_Visualizar_Observacao.Name = "Pag_Visualizar_Observacao";
            this.Pag_Visualizar_Observacao.Padding = new System.Windows.Forms.Padding(3);
            this.Pag_Visualizar_Observacao.Size = new System.Drawing.Size(1295, 473);
            this.Pag_Visualizar_Observacao.TabIndex = 1;
            this.Pag_Visualizar_Observacao.Text = "Visualizar todas as observações";
            this.Pag_Visualizar_Observacao.UseVisualStyleBackColor = true;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.guna2GroupBox1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.dataGridView_Tabela_Obs);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1295, 473);
            this.panel1.TabIndex = 0;
            // 
            // guna2GroupBox1
            // 
            this.guna2GroupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.guna2GroupBox1.Controls.Add(this.label3);
            this.guna2GroupBox1.Controls.Add(this.Obs_Data);
            this.guna2GroupBox1.Controls.Add(this.Combo_Titulo_Obs);
            this.guna2GroupBox1.Controls.Add(this.label12);
            this.guna2GroupBox1.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.guna2GroupBox1.Font = new System.Drawing.Font("Arial", 14.25F);
            this.guna2GroupBox1.ForeColor = System.Drawing.Color.Black;
            this.guna2GroupBox1.Location = new System.Drawing.Point(6, 11);
            this.guna2GroupBox1.Name = "guna2GroupBox1";
            this.guna2GroupBox1.Size = new System.Drawing.Size(1280, 150);
            this.guna2GroupBox1.TabIndex = 64;
            this.guna2GroupBox1.Text = "Filtro";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label3.Location = new System.Drawing.Point(606, 83);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 30);
            this.label3.TabIndex = 55;
            this.label3.Text = "Data:";
            // 
            // Obs_Data
            // 
            this.Obs_Data.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Obs_Data.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Obs_Data.Location = new System.Drawing.Point(685, 86);
            this.Obs_Data.Name = "Obs_Data";
            this.Obs_Data.Size = new System.Drawing.Size(365, 26);
            this.Obs_Data.TabIndex = 54;
            this.Obs_Data.ValueChanged += new System.EventHandler(this.Data_obs_Data_ValueChanged);
            // 
            // Combo_Titulo_Obs
            // 
            this.Combo_Titulo_Obs.BackColor = System.Drawing.Color.Transparent;
            this.Combo_Titulo_Obs.BorderColor = System.Drawing.Color.Black;
            this.Combo_Titulo_Obs.BorderRadius = 8;
            this.Combo_Titulo_Obs.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combo_Titulo_Obs.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_Titulo_Obs.FillColor = System.Drawing.Color.Gainsboro;
            this.Combo_Titulo_Obs.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Titulo_Obs.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Titulo_Obs.Font = new System.Drawing.Font("Arial", 14.25F);
            this.Combo_Titulo_Obs.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Combo_Titulo_Obs.ItemHeight = 30;
            this.Combo_Titulo_Obs.Location = new System.Drawing.Point(185, 83);
            this.Combo_Titulo_Obs.Name = "Combo_Titulo_Obs";
            this.Combo_Titulo_Obs.Size = new System.Drawing.Size(350, 36);
            this.Combo_Titulo_Obs.TabIndex = 27;
            this.Combo_Titulo_Obs.SelectedIndexChanged += new System.EventHandler(this.Combo_Titulo_SelectedIndexChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.label12.Location = new System.Drawing.Point(22, 83);
            this.label12.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(158, 30);
            this.label12.TabIndex = 28;
            this.label12.Text = "Observação:";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(7, 423);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(746, 44);
            this.label1.TabIndex = 63;
            this.label1.Text = " Atalhos:\r\n F1 - Limpa tudo     F5 - Atualiza tabela     F6 - Filtra    DEL - Del" +
    "ete     ESC - Fechar a janela";
            // 
            // dataGridView_Tabela_Obs
            // 
            this.dataGridView_Tabela_Obs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView_Tabela_Obs.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.dataGridView_Tabela_Obs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView_Tabela_Obs.Location = new System.Drawing.Point(6, 167);
            this.dataGridView_Tabela_Obs.Name = "dataGridView_Tabela_Obs";
            this.dataGridView_Tabela_Obs.Size = new System.Drawing.Size(1280, 244);
            this.dataGridView_Tabela_Obs.TabIndex = 0;
            // 
            // ERROR_Dados_Nulos
            // 
            this.ERROR_Dados_Nulos.ContainerControl = this;
            this.ERROR_Dados_Nulos.Icon = ((System.Drawing.Icon)(resources.GetObject("ERROR_Dados_Nulos.Icon")));
            // 
            // Observacao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1301, 563);
            this.Controls.Add(this.tabControl1);
            this.Name = "Observacao";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Observação";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Observacao_Load);
            this.tabControl1.ResumeLayout(false);
            this.Pag_add_Observacao.ResumeLayout(false);
            this.Pag_add_Observacao.PerformLayout();
            this.Pag_Visualizar_Observacao.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.guna2GroupBox1.ResumeLayout(false);
            this.guna2GroupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView_Tabela_Obs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ERROR_Dados_Nulos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage Pag_add_Observacao;
        private System.Windows.Forms.TabPage Pag_Visualizar_Observacao;
        private System.Windows.Forms.Panel panel1;
        private Guna.UI2.WinForms.Guna2TextBox Campo_Titulo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Campo_Descricao_Obs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label atalho;
        private System.Windows.Forms.ErrorProvider ERROR_Dados_Nulos;
        private System.Windows.Forms.DataGridView dataGridView_Tabela_Obs;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2GroupBox guna2GroupBox1;
        private Guna.UI2.WinForms.Guna2ComboBox Combo_Titulo_Obs;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker Obs_Data;
    }
}