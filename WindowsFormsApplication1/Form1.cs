using SwissTransport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwissTransportTimetable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void cmdSearch_Click(object sender, EventArgs e)
        {
            string message = "Station ungültig";

            //Stationen auslesen und validieren
            string startStation = txtStartStation.Text;
            txtStartStation.Text = SearchStation(startStation) == startStation ? startStation : message;

            string endStation = txtEndStation.Text;
            txtEndStation.Text = SearchStation(endStation) == endStation ? endStation : message;
        }

        private void StartStation_KeyUp(object sender, KeyEventArgs e)
        {
            //Autocomplete hinzufügen
            string startStation = txtStartStation.Text;
            if (e.KeyCode.ToString() != "Back" && e.KeyCode.ToString() != "Delete" && startStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung
            {

                txtStartStation.AutoCompleteCustomSource = Autocomplete(startStation);
                txtStartStation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtStartStation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private void EndStation_KeyUp(object sender, KeyEventArgs e)
        {
            //Autocomplete hinzufügen
            string endStation = txtEndStation.Text;
            if (endStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung
            {             
                txtEndStation.AutoCompleteCustomSource = Autocomplete(endStation);
                txtEndStation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtEndStation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        private string SearchStation(string station)
        {
            //Station auslesen
            Transport transport = new Transport();
            return transport.GetStations(station) != null ? transport.GetStations(station).StationList[0].Name.ToString() : null;
            
        }

        public AutoCompleteStringCollection Autocomplete(string station)
        {
            //Liste mit allen Station in eine Liste laden
            var source = new AutoCompleteStringCollection();
            Transport transport = new Transport();

            var transportList = transport.GetStations(station).StationList;
            foreach (var transportStation in transportList)
            {
                source.Add(transportStation.Name.ToString());
            }

            return source;
        }

        private List<Connection> SearchConnection(string fromStation, string toStation)
        {
            //Verbindungen auslesen
            Transport transport = new Transport();
            return transport.GetConnections(fromStation, toStation).ConnectionList;
        }
    }
}
