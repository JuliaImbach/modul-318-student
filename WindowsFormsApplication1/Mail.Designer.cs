namespace SwissTransportTimetable
{
    partial class Mail
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtAbsender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEmpfaenger = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBetreff = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.rtxtNachricht = new System.Windows.Forms.RichTextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Absender";
            // 
            // txtAbsender
            // 
            this.txtAbsender.Location = new System.Drawing.Point(16, 30);
            this.txtAbsender.Name = "txtAbsender";
            this.txtAbsender.Size = new System.Drawing.Size(150, 20);
            this.txtAbsender.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Empfänger";
            // 
            // txtEmpfaenger
            // 
            this.txtEmpfaenger.Location = new System.Drawing.Point(16, 73);
            this.txtEmpfaenger.Name = "txtEmpfaenger";
            this.txtEmpfaenger.Size = new System.Drawing.Size(150, 20);
            this.txtEmpfaenger.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Betreff";
            // 
            // txtBetreff
            // 
            this.txtBetreff.Location = new System.Drawing.Point(16, 117);
            this.txtBetreff.Name = "txtBetreff";
            this.txtBetreff.Size = new System.Drawing.Size(150, 20);
            this.txtBetreff.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "Nachricht";
            // 
            // rtxtNachricht
            // 
            this.rtxtNachricht.Location = new System.Drawing.Point(16, 165);
            this.rtxtNachricht.Name = "rtxtNachricht";
            this.rtxtNachricht.Size = new System.Drawing.Size(264, 96);
            this.rtxtNachricht.TabIndex = 8;
            this.rtxtNachricht.Text = "";
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(16, 267);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Senden";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // Mail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 296);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.rtxtNachricht);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBetreff);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtEmpfaenger);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtAbsender);
            this.Controls.Add(this.label1);
            this.MaximumSize = new System.Drawing.Size(300, 323);
            this.MinimumSize = new System.Drawing.Size(300, 323);
            this.Name = "Mail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mail";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAbsender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEmpfaenger;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBetreff;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RichTextBox rtxtNachricht;
        private System.Windows.Forms.Button btnSend;
    }
}