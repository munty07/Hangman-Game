namespace MTP_lab3_spanzuratoare
{
    partial class Utilizator
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Utilizator));
            this.lblNumeJucator = new System.Windows.Forms.Label();
            this.txtNumeJucator = new System.Windows.Forms.TextBox();
            this.btnStartGame = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblNumeJucator
            // 
            this.lblNumeJucator.AutoSize = true;
            this.lblNumeJucator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblNumeJucator.Location = new System.Drawing.Point(45, 82);
            this.lblNumeJucator.Name = "lblNumeJucator";
            this.lblNumeJucator.Size = new System.Drawing.Size(164, 24);
            this.lblNumeJucator.TabIndex = 0;
            this.lblNumeJucator.Text = "NUME JUCATOR:";
            // 
            // txtNumeJucator
            // 
            this.txtNumeJucator.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.txtNumeJucator.Location = new System.Drawing.Point(234, 79);
            this.txtNumeJucator.Multiline = true;
            this.txtNumeJucator.Name = "txtNumeJucator";
            this.txtNumeJucator.Size = new System.Drawing.Size(294, 33);
            this.txtNumeJucator.TabIndex = 1;
            // 
            // btnStartGame
            // 
            this.btnStartGame.BackColor = System.Drawing.Color.Transparent;
            this.btnStartGame.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnStartGame.Location = new System.Drawing.Point(49, 207);
            this.btnStartGame.Name = "btnStartGame";
            this.btnStartGame.Size = new System.Drawing.Size(217, 63);
            this.btnStartGame.TabIndex = 2;
            this.btnStartGame.Text = "START GAME";
            this.btnStartGame.UseVisualStyleBackColor = false;
            this.btnStartGame.Click += new System.EventHandler(this.btnStartGame_Click);
            // 
            // btnExit
            // 
            this.btnExit.BackColor = System.Drawing.Color.Transparent;
            this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.btnExit.Location = new System.Drawing.Point(311, 207);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(217, 63);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "EXIT";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // Utilizator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(82)))), ((int)(((byte)(93)))));
            this.ClientSize = new System.Drawing.Size(594, 319);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.btnStartGame);
            this.Controls.Add(this.txtNumeJucator);
            this.Controls.Add(this.lblNumeJucator);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Utilizator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Utilizator";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblNumeJucator;
        public System.Windows.Forms.TextBox txtNumeJucator;
        private System.Windows.Forms.Button btnStartGame;
        private System.Windows.Forms.Button btnExit;
    }
}