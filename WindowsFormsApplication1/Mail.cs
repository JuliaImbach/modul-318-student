﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;

namespace SwissTransportTimetable
{
    public partial class Mail : Form
    {
        //Membervariabeln
        string m_Nachricht = "";

        public Mail()
        {
            InitializeComponent();
        }

        //Konstruktor
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
                string das = nachricht + "<BR>" + this.Nachricht;
                sendMail(absender, empfaenger, betreff, nachricht + "<BR>" + this.Nachricht, "smtp.gmail.com", 465);
            }
            catch(Exception ex)
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
        public void sendMail(string absender, string empfaenger, string betreff, string nachricht, string server, int port)
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

            //Email absenden
            MailClient.Send(Email);
        }
    }
}