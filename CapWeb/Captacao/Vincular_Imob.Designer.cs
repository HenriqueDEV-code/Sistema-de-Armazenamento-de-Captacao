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
            this.components = new System.ComponentModel.Container();
            this.dgvVinculos = new System.Windows.Forms.DataGridView();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label6 = new System.Windows.Forms.Label();
            this.LB_Status = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculos)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvVinculos
            // 
            this.dgvVinculos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVinculos.Location = new System.Drawing.Point(-1, 101);
            this.dgvVinculos.Name = "dgvVinculos";
            this.dgvVinculos.Size = new System.Drawing.Size(1921, 979);
            this.dgvVinculos.TabIndex = 0;
            this.dgvVinculos.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvVinculos_CellValueChanged);
            // 
            // timer1
            // 
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(7, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(108, 30);
            this.label6.TabIndex = 10;
            this.label6.Text = "Status - ";
            // 
            // LB_Status
            // 
            this.LB_Status.AutoSize = true;
            this.LB_Status.BackColor = System.Drawing.Color.Transparent;
            this.LB_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.8F);
            this.LB_Status.ForeColor = System.Drawing.Color.Black;
            this.LB_Status.Location = new System.Drawing.Point(108, 68);
            this.LB_Status.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LB_Status.Name = "LB_Status";
            this.LB_Status.Size = new System.Drawing.Size(23, 30);
            this.LB_Status.TabIndex = 11;
            this.LB_Status.Text = "*";
            // 
            // Vincular_Imob
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1920, 1080);
            this.Controls.Add(this.LB_Status);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.dgvVinculos);
            this.MaximizeBox = false;
            this.Name = "Vincular_Imob";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vinculação de imobiliárias ";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Vincular_Imob_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVinculos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvVinculos;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label LB_Status;
    }
}