using System;
using System.Windows.Forms;

namespace SwissTransportTimetable
{
    public partial class WebBrowserForm : Form
    {
        //Konstruktoren
        public WebBrowserForm()
        {
            InitializeComponent();
        }

        public WebBrowserForm(string url) : this()
        {
            this.Url = url;
        }

        //Properties
        private string Url { get; set; }

        /// <summary>
        ///  Lädt die Url in die Form.
        /// </summary>
        ///  <param name="sender">Sender</param>
        ///  <param name="e">Load-Event</param>
        private void OnLoad(object sender, EventArgs e)
        {
            webBrowserMaps.Navigate(new Uri(this.Url));
        }
    }
}
