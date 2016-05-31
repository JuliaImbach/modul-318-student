namespace SwissTransportTimetable
{
    partial class Hauptform
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtEndStation = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnDeparture = new System.Windows.Forms.Button();
            this.listViewConnection = new System.Windows.Forms.ListView();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.StatusBarLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnMail = new System.Windows.Forms.Button();
            this.dateConnection = new System.Windows.Forms.DateTimePicker();
            this.chbAnkunft = new System.Windows.Forms.CheckBox();
            this.mapsStartStation = new System.Windows.Forms.LinkLabel();
            this.mapsEndStation = new System.Windows.Forms.LinkLabel();
            this.StatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdSearch
            // 
            this.cmdSearch.Location = new System.Drawing.Point(236, 110);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(93, 36);
            this.cmdSearch.TabIndex = 3;
            this.cmdSearch.Text = "Verbindungen Suchen";
            this.cmdSearch.UseVisualStyleBackColor = true;
            this.cmdSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // txtStartStation
            // 
            this.txtStartStation.AutoCompleteCustomSource.AddRange(new string[] {
            "Hall",
            "Zürhc"});
            this.txtStartStation.HideSelection = false;
            this.txtStartStation.Location = new System.Drawing.Point(13, 30);
            this.txtStartStation.Name = "txtStartStation";
            this.txtStartStation.Size = new System.Drawing.Size(335, 20);
            this.txtStartStation.TabIndex = 1;
            this.txtStartStation.TextChanged += new System.EventHandler(this.startStation_TextChanged);
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
            this.txtEndStation.TextChanged += new System.EventHandler(this.endStation_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Verbindungen";
            // 
            // btnDeparture
            // 
            this.btnDeparture.Location = new System.Drawing.Point(502, 30);
            this.btnDeparture.Name = "btnDeparture";
            this.btnDeparture.Size = new System.Drawing.Size(75, 23);
            this.btnDeparture.TabIndex = 4;
            this.btnDeparture.Text = "Abfahrstafel";
            this.btnDeparture.UseVisualStyleBackColor = true;
            this.btnDeparture.Click += new System.EventHandler(this.btnStationBoard_Click);
            // 
            // listViewConnection
            // 
            this.listViewConnection.FullRowSelect = true;
            this.listViewConnection.Location = new System.Drawing.Point(13, 160);
            this.listViewConnection.MultiSelect = false;
            this.listViewConnection.Name = "listViewConnection";
            this.listViewConnection.Size = new System.Drawing.Size(516, 140);
            this.listViewConnection.TabIndex = 8;
            this.listViewConnection.UseCompatibleStateImageBehavior = false;
            this.listViewConnection.View = System.Windows.Forms.View.Details;
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBarLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 311);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(596, 22);
            this.StatusBar.TabIndex = 9;
            this.StatusBar.Text = "statusStrip1";
            // 
            // StatusBarLabel
            // 
            this.StatusBarLabel.Name = "StatusBarLabel";
            this.StatusBarLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // btnMail
            // 
            this.btnMail.Location = new System.Drawing.Point(536, 160);
            this.btnMail.Name = "btnMail";
            this.btnMail.Size = new System.Drawing.Size(41, 23);
            this.btnMail.TabIndex = 10;
            this.btnMail.Text = "Mail";
            this.btnMail.UseVisualStyleBackColor = true;
            this.btnMail.Click += new System.EventHandler(this.btnMail_Click);
            // 
            // dateConnection
            // 
            this.dateConnection.CustomFormat = "dd.MM.yyyy HH:mm";
            this.dateConnection.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateConnection.Location = new System.Drawing.Point(367, 30);
            this.dateConnection.Name = "dateConnection";
            this.dateConnection.Size = new System.Drawing.Size(114, 20);
            this.dateConnection.TabIndex = 13;
            // 
            // chbAnkunft
            // 
            this.chbAnkunft.AutoSize = true;
            this.chbAnkunft.Checked = true;
            this.chbAnkunft.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chbAnkunft.Location = new System.Drawing.Point(367, 57);
            this.chbAnkunft.Name = "chbAnkunft";
            this.chbAnkunft.Size = new System.Drawing.Size(84, 17);
            this.chbAnkunft.TabIndex = 14;
            this.chbAnkunft.Text = "Ankunftszeit";
            this.chbAnkunft.UseVisualStyleBackColor = true;
            // 
            // mapsStartStation
            // 
            this.mapsStartStation.AutoSize = true;
            this.mapsStartStation.Location = new System.Drawing.Point(80, 13);
            this.mapsStartStation.Name = "mapsStartStation";
            this.mapsStartStation.Size = new System.Drawing.Size(32, 13);
            this.mapsStartStation.TabIndex = 15;
            this.mapsStartStation.TabStop = true;
            this.mapsStartStation.Text = "maps";
            this.mapsStartStation.Click += new System.EventHandler(this.mapsStartStation_Click);
            // 
            // mapsEndStation
            // 
            this.mapsEndStation.AutoSize = true;
            this.mapsEndStation.Location = new System.Drawing.Point(80, 57);
            this.mapsEndStation.Name = "mapsEndStation";
            this.mapsEndStation.Size = new System.Drawing.Size(32, 13);
            this.mapsEndStation.TabIndex = 16;
            this.mapsEndStation.TabStop = true;
            this.mapsEndStation.Text = "maps";
            this.mapsEndStation.Click += new System.EventHandler(this.mapsEndStation_Click);
            // 
            // Hauptform
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(596, 333);
            this.Controls.Add(this.mapsEndStation);
            this.Controls.Add(this.mapsStartStation);
            this.Controls.Add(this.chbAnkunft);
            this.Controls.Add(this.dateConnection);
            this.Controls.Add(this.btnMail);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.listViewConnection);
            this.Controls.Add(this.btnDeparture);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtEndStation);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtStartStation);
            this.Controls.Add(this.cmdSearch);
            this.MaximumSize = new System.Drawing.Size(750, 360);
            this.MinimumSize = new System.Drawing.Size(550, 342);
            this.Name = "Hauptform";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ÖV-Verbindungen";
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.TextBox txtStartStation;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtEndStation;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnDeparture;
        private System.Windows.Forms.ListView listViewConnection;
        private System.Windows.Forms.StatusStrip StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusBarLabel;
        private System.Windows.Forms.Button btnMail;
        private System.Windows.Forms.DateTimePicker dateConnection;
        private System.Windows.Forms.CheckBox chbAnkunft;
        private System.Windows.Forms.LinkLabel mapsStartStation;
        private System.Windows.Forms.LinkLabel mapsEndStation;
    }
}

