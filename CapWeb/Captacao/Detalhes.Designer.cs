﻿namespace CapWeb.Captacao
{
    partial class Detalhes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Detalhes));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label16 = new System.Windows.Forms.Label();
            this.Combo_Nome_Prop_Busca = new Guna.UI2.WinForms.Guna2ComboBox();
            this.extrair = new Guna.UI2.WinForms.Guna2Button();
            this.Button_Buscar_DBA = new Guna.UI2.WinForms.Guna2Button();
            this.Detalhes_Busca = new Guna.UI2.WinForms.Guna2TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.Combo_Cidade_Busca = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ERROR_Dados_Nulos = new System.Windows.Forms.ErrorProvider(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ERROR_Dados_Nulos)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.panel1.Controls.Add(this.label16);
            this.panel1.Controls.Add(this.Combo_Nome_Prop_Busca);
            this.panel1.Controls.Add(this.extrair);
            this.panel1.Controls.Add(this.Button_Buscar_DBA);
            this.panel1.Controls.Add(this.Detalhes_Busca);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.Combo_Cidade_Busca);
            this.panel1.Controls.Add(this.label11);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Location = new System.Drawing.Point(-1, 64);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1128, 694);
            this.panel1.TabIndex = 0;
            // 
            // label16
            // 
            this.label16.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label16.AutoSize = true;
            this.label16.BackColor = System.Drawing.Color.Transparent;
            this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.8F);
            this.label16.ForeColor = System.Drawing.Color.White;
            this.label16.Location = new System.Drawing.Point(28, 615);
            this.label16.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(315, 66);
            this.label16.TabIndex = 43;
            this.label16.Text = " Atalhos:\r\n F5 - Buscar imóveis\r\n F6 - Extrair as informações para o .txt";
            // 
            // Combo_Nome_Prop_Busca
            // 
            this.Combo_Nome_Prop_Busca.BackColor = System.Drawing.Color.Transparent;
            this.Combo_Nome_Prop_Busca.BorderColor = System.Drawing.Color.Black;
            this.Combo_Nome_Prop_Busca.BorderRadius = 8;
            this.Combo_Nome_Prop_Busca.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combo_Nome_Prop_Busca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_Nome_Prop_Busca.FillColor = System.Drawing.Color.DimGray;
            this.Combo_Nome_Prop_Busca.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Nome_Prop_Busca.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Nome_Prop_Busca.Font = new System.Drawing.Font("Arial", 14.25F);
            this.Combo_Nome_Prop_Busca.ForeColor = System.Drawing.Color.White;
            this.Combo_Nome_Prop_Busca.ItemHeight = 30;
            this.Combo_Nome_Prop_Busca.Location = new System.Drawing.Point(128, 43);
            this.Combo_Nome_Prop_Busca.Name = "Combo_Nome_Prop_Busca";
            this.Combo_Nome_Prop_Busca.Size = new System.Drawing.Size(351, 36);
            this.Combo_Nome_Prop_Busca.TabIndex = 42;
            // 
            // extrair
            // 
            this.extrair.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.extrair.Animated = true;
            this.extrair.AutoRoundedCorners = true;
            this.extrair.BackColor = System.Drawing.Color.Transparent;
            this.extrair.BorderColor = System.Drawing.Color.Blue;
            this.extrair.BorderRadius = 22;
            this.extrair.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.extrair.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.extrair.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.extrair.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.extrair.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.extrair.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.extrair.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.extrair.IndicateFocus = true;
            this.extrair.Location = new System.Drawing.Point(963, 635);
            this.extrair.Name = "extrair";
            this.extrair.Size = new System.Drawing.Size(153, 46);
            this.extrair.TabIndex = 41;
            this.extrair.Text = "Extrair";
            this.extrair.UseTransparentBackground = true;
            this.extrair.Click += new System.EventHandler(this.extrair_Click);
            // 
            // Button_Buscar_DBA
            // 
            this.Button_Buscar_DBA.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Button_Buscar_DBA.Animated = true;
            this.Button_Buscar_DBA.AutoRoundedCorners = true;
            this.Button_Buscar_DBA.BackColor = System.Drawing.Color.Transparent;
            this.Button_Buscar_DBA.BorderRadius = 19;
            this.Button_Buscar_DBA.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.Button_Buscar_DBA.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.Button_Buscar_DBA.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.Button_Buscar_DBA.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.Button_Buscar_DBA.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.Button_Buscar_DBA.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.Button_Buscar_DBA.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Button_Buscar_DBA.IndicateFocus = true;
            this.Button_Buscar_DBA.Location = new System.Drawing.Point(963, 42);
            this.Button_Buscar_DBA.Name = "Button_Buscar_DBA";
            this.Button_Buscar_DBA.Size = new System.Drawing.Size(153, 41);
            this.Button_Buscar_DBA.TabIndex = 40;
            this.Button_Buscar_DBA.Text = "Buscar";
            this.Button_Buscar_DBA.UseTransparentBackground = true;
            this.Button_Buscar_DBA.Click += new System.EventHandler(this.Button_Buscar_DBA_Click);
            // 
            // Detalhes_Busca
            // 
            this.Detalhes_Busca.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Detalhes_Busca.Animated = true;
            this.Detalhes_Busca.BorderColor = System.Drawing.Color.Black;
            this.Detalhes_Busca.BorderRadius = 10;
            this.Detalhes_Busca.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.Detalhes_Busca.DefaultText = "";
            this.Detalhes_Busca.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.Detalhes_Busca.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.Detalhes_Busca.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Detalhes_Busca.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.Detalhes_Busca.FillColor = System.Drawing.Color.DimGray;
            this.Detalhes_Busca.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Detalhes_Busca.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold);
            this.Detalhes_Busca.ForeColor = System.Drawing.Color.White;
            this.Detalhes_Busca.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Detalhes_Busca.Location = new System.Drawing.Point(32, 142);
            this.Detalhes_Busca.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Detalhes_Busca.Multiline = true;
            this.Detalhes_Busca.Name = "Detalhes_Busca";
            this.Detalhes_Busca.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.Detalhes_Busca.PlaceholderText = "";
            this.Detalhes_Busca.ReadOnly = true;
            this.Detalhes_Busca.SelectedText = "";
            this.Detalhes_Busca.Size = new System.Drawing.Size(1056, 468);
            this.Detalhes_Busca.TabIndex = 39;
            this.Detalhes_Busca.Load += new System.EventHandler(this.Detalhes_Busca_Load);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.BackColor = System.Drawing.Color.Transparent;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label18.ForeColor = System.Drawing.Color.Silver;
            this.label18.Location = new System.Drawing.Point(27, 107);
            this.label18.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(520, 30);
            this.label18.TabIndex = 38;
            this.label18.Text = "Detalhes do(os) imóvel(eis) e do proprietário";
            // 
            // Combo_Cidade_Busca
            // 
            this.Combo_Cidade_Busca.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.Combo_Cidade_Busca.BackColor = System.Drawing.Color.Transparent;
            this.Combo_Cidade_Busca.BorderColor = System.Drawing.Color.Black;
            this.Combo_Cidade_Busca.BorderRadius = 8;
            this.Combo_Cidade_Busca.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.Combo_Cidade_Busca.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.Combo_Cidade_Busca.FillColor = System.Drawing.Color.DimGray;
            this.Combo_Cidade_Busca.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Cidade_Busca.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.Combo_Cidade_Busca.Font = new System.Drawing.Font("Arial", 14.25F);
            this.Combo_Cidade_Busca.ForeColor = System.Drawing.Color.White;
            this.Combo_Cidade_Busca.ItemHeight = 30;
            this.Combo_Cidade_Busca.Items.AddRange(new object[] {
            ""});
            this.Combo_Cidade_Busca.Location = new System.Drawing.Point(654, 43);
            this.Combo_Cidade_Busca.Name = "Combo_Cidade_Busca";
            this.Combo_Cidade_Busca.Size = new System.Drawing.Size(293, 36);
            this.Combo_Cidade_Busca.TabIndex = 27;
            // 
            // label11
            // 
            this.label11.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label11.AutoSize = true;
            this.label11.BackColor = System.Drawing.Color.Transparent;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label11.ForeColor = System.Drawing.Color.Silver;
            this.label11.Location = new System.Drawing.Point(554, 44);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(101, 30);
            this.label11.TabIndex = 26;
            this.label11.Text = "Cidade:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label6.ForeColor = System.Drawing.Color.Silver;
            this.label6.Location = new System.Drawing.Point(27, 44);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 30);
            this.label6.TabIndex = 13;
            this.label6.Text = "Nome: ";
            // 
            // ERROR_Dados_Nulos
            // 
            this.ERROR_Dados_Nulos.ContainerControl = this;
            this.ERROR_Dados_Nulos.Icon = ((System.Drawing.Icon)(resources.GetObject("ERROR_Dados_Nulos.Icon")));
            // 
            // Detalhes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1127, 760);
            this.Controls.Add(this.panel1);
            this.Name = "Detalhes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Detalhes dos imóveis";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Detalhes_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ERROR_Dados_Nulos)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label6;
        private Guna.UI2.WinForms.Guna2ComboBox Combo_Cidade_Busca;
        private System.Windows.Forms.Label label11;
        private Guna.UI2.WinForms.Guna2TextBox Detalhes_Busca;
        private System.Windows.Forms.Label label18;
        private Guna.UI2.WinForms.Guna2Button Button_Buscar_DBA;
        private Guna.UI2.WinForms.Guna2Button extrair;
        private System.Windows.Forms.ErrorProvider ERROR_Dados_Nulos;
        private Guna.UI2.WinForms.Guna2ComboBox Combo_Nome_Prop_Busca;
        private System.Windows.Forms.Label label16;
    }
}