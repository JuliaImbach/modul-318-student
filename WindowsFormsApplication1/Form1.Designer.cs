namespace SwissTransportTimetable
{
    partial class Form1
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
            this.cmdSearch = new System.Windows.Forms.Button();
            this.txtStartStation = new System.Windows.Forms.TextBox();
            this.lstBoxConnections = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndStation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeparture = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(372, 30);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(75, 23);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "Suchen";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.cmdSearch_Click);
            // 
            // txtStartStation
            // 
            this.txtStartStation.Location = new System.Drawing.Point(13, 30);
            this.txtStartStation.Name = "txtStartStation";
            this.txtStartStation.Size = new System.Drawing.Size(335, 20);
            this.txtStartStation.TabIndex = 1;
            this.txtStartStation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.StartStation_KeyUp);
            // 
            // lstBoxConnections
            // 
            this.lstBoxConnections.FormattingEnabled = true;
            this.lstBoxConnections.Location = new System.Drawing.Point(12, 137);
            this.lstBoxConnections.Name = "lstBoxConnections";
            this.lstBoxConnections.Size = new System.Drawing.Size(335, 95);
            this.lstBoxConnections.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Startstation";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Endstation";
            // 
            // txtEndStation
            // 
            this.txtEndStation.Location = new System.Drawing.Point(13, 74);
            this.txtEndStation.Name = "txtEndStation";
            this.txtEndStation.Size = new System.Drawing.Size(335, 20);
            this.txtEndStation.TabIndex = 2;
            this.txtEndStation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.EndStation_KeyUp);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 118);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Verbindungen";
            // 
            // btnDeparture
            // 
            this.btnDeparture.Location = new System.Drawing.Point(372, 74);
            this.btnDeparture.Name = "btnDeparture";
            this.btnDeparture.Size = new System.Drawing.Size(75, 23);
            this.btnDeparture.TabIndex = 4;
            this.btnDeparture.Text = "Abfahrstafel";
            this.btnDeparture.UseVisualStyleBackColor = true;
            this.btnDeparture.Click += new System.EventHandler(this.btnStationBoard_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 274);
            this.Controls.Add(this.btnDeparture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEndStation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstBoxConnections);
            this.Controls.Add(this.txtStartStation);
            this.Controls.Add(this.cmdSearch);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtStartStation;
        private System.Windows.Forms.ListBox lstBoxConnections;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEndStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeparture;
    }
}

