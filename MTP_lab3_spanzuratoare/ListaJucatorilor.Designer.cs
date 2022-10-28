namespace MTP_lab3_spanzuratoare
{
    partial class ListaJucatorilor
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
            this.btnBack = new System.Windows.Forms.Button();
            this.lblJucatorLog = new System.Windows.Forms.Label();
            this.dgvJucatori = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvJucatori)).BeginInit();
            this.SuspendLayout();
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(272, 321);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 1;
            this.btnBack.Text = "BACK";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // lblJucatorLog
            // 
            this.lblJucatorLog.AutoSize = true;
            this.lblJucatorLog.Location = new System.Drawing.Point(37, 21);
            this.lblJucatorLog.Name = "lblJucatorLog";
            this.lblJucatorLog.Size = new System.Drawing.Size(51, 17);
            this.lblJucatorLog.TabIndex = 2;
            this.lblJucatorLog.Text = "jucator";
            // 
            // dgvJucatori
            // 
            this.dgvJucatori.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvJucatori.Location = new System.Drawing.Point(40, 65);
            this.dgvJucatori.Name = "dgvJucatori";
            this.dgvJucatori.RowTemplate.Height = 24;
            this.dgvJucatori.Size = new System.Drawing.Size(307, 251);
            this.dgvJucatori.TabIndex = 3;
            // 
            // ListaJucatorilor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(82)))), ((int)(((byte)(93)))));
            this.ClientSize = new System.Drawing.Size(369, 363);
            this.Controls.Add(this.dgvJucatori);
            this.Controls.Add(this.lblJucatorLog);
            this.Controls.Add(this.btnBack);
            this.Name = "ListaJucatorilor";
            this.Text = "ListaJucatorilor";
            ((System.ComponentModel.ISupportInitialize)(this.dgvJucatori)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnBack;
        private System.Windows.Forms.Label lblJucatorLog;
        private System.Windows.Forms.DataGridView dgvJucatori;
    }
}