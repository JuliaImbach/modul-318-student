using System;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using System.Configuration;

namespace SwissTransportTimetable
{
    public partial class Mail : Form
    {
        //Membervariabeln
        string m_Nachricht = "";

        //Konstruktoren
        public Mail()
        {
            InitializeComponent();
        }

        public Mail(string nachricht)
        {
            InitializeComponent();
            this.Nachricht = nachricht;
        }

        //Get-/Set-Methoden
        public string Nachricht
        {
            get { return m_Nachricht; }
            set { m_Nachricht = value; }
        }

        /// <summary>
        ///  Der Mailversand wird ausgelöst und Mail aus den
        ///  Textfeldern zusammengesetzt.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void btnSend_Click(object sender, EventArgs e)
        {
            string absender = txtAbsender.Text;
            string empfaenger = txtEmpfaenger.Text;
            string betreff = txtBetreff.Text;
            string nachricht = rtxtNachricht.Text;

            try
            {
                sendMail(absender, empfaenger, betreff, nachricht + "\n\n" + this.Nachricht, ConfigurationManager.AppSettings["smtp-server"], Convert.ToInt32(ConfigurationManager.AppSettings["port"]), txtPasswort.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mail konnte nicht gesendet werden: " + ex.Message);
            }

            this.Close();
        }

        /// <summary>
        ///  Versendet eine Mail
        /// </summary>
        /// <param name="absender">Mail-Adresse Absender</param>
        /// <param name="empfänger">Mail-Adresse Empfänger</param>
        /// <param name="betreff">Betreffzeile</param>
        /// <param name="nachricht">Mail-Nachricht</param>
        /// <param name="server">Mail-Server</param>
        /// <param name="port">Port des Mail-Servers</param>
        /// <param name="user">Benutzername</param>
        /// <param name="passwort">Passwort</param>
        /// <returns>StationBoard-List: Liste mit Abfahrtszeiten</returns>
        public void sendMail(string absender, string empfaenger, string betreff, string nachricht, string server, int port, string passwort)
        {
            MailMessage Email = new MailMessage();

            //Absender konfigurieren
            Email.From = new MailAddress(absender);

            //Empfänger konfigurieren
            Email.To.Add(empfaenger);

            //Betreff einrichten
            Email.Subject = betreff;

            //Hinzufügen der eigentlichen Nachricht
            Email.Body = nachricht;

            //Ausgangsserver initialisieren
            SmtpClient MailClient = new SmtpClient(server, port);
            MailClient.EnableSsl = true;
            MailClient.Credentials = new NetworkCredential(absender, passwort);


            //Email absenden
            MailClient.Send(Email);
        }
    }
}
