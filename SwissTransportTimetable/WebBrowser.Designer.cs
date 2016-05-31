namespace SwissTransportTimetable
{
    partial class frmWebBrowser
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
            this.webBrowserMaps = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // webBrowserMaps
            // 
            this.webBrowserMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowserMaps.Location = new System.Drawing.Point(0, 0);
            this.webBrowserMaps.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowserMaps.Name = "webBrowserMaps";
            this.webBrowserMaps.Size = new System.Drawing.Size(647, 460);
            this.webBrowserMaps.TabIndex = 0;
            // 
            // frmWebBrowser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(647, 460);
            this.Controls.Add(this.webBrowserMaps);
            this.Name = "frmWebBrowser";
            this.Text = "Karte";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowserMaps;
    }
}