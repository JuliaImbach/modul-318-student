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
            //Stationen auslesen und validieren
            bool isValid = true;
            string startStation = txtStartStation.Text;
            if (!string.IsNullOrEmpty(startStation))
            {
                if (SearchStation(startStation) != startStation)
                {
                    isValid = false;
                    MessageBox.Show("Die Startstation ist ungültig.");
                }
            }

            string endStation = txtEndStation.Text;
            if (!string.IsNullOrEmpty(endStation))
            {
                if (SearchStation(endStation) != endStation)
                {
                    isValid = false;
                    MessageBox.Show("Die Endstation ist ungültig.");
                }
            }
            else
            {
                MessageBox.Show("Sie müssen eine Endstation angeben.");
            }

            //Verbindungen auslesen
            if(isValid)
            {
                var connections = SearchConnection(startStation, endStation);

                foreach(var connection in connections)
                {
                    string connectionString = string.Format(Convert.ToDateTime(connection.From.Departure).ToShortTimeString() + " "
                        + connection.From.Station.Name.ToString() + "  -  "
                        + Convert.ToDateTime(connection.To.Arrival).ToShortTimeString() + " "
                        + connection.To.Station.Name.ToString() + "  ");

                    //Dauer hinzufügen
                    if(connection.Duration.Substring(0,2) != "00")
                    {
                        connectionString += connection.Duration.Substring(0, 2) + " Tag(e) ";
                    }

                    connectionString += Convert.ToDateTime(connection.Duration.Substring(3)).ToShortTimeString() + " Stunden";

                    lstBoxConnections.Items.Add(connectionString);
                }
            }
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

        private void btnStationBoard_Click(object sender, EventArgs e)
        {
            string fromStation = txtStartStation.Text;

            if (!string.IsNullOrEmpty(fromStation))
            {
                if (SearchStation(fromStation) != fromStation)
                {
                    MessageBox.Show("Die Startstation ist ungültig.");
                }
                else
                {
                    //Abfahrtszeiten auslesen
                    var timetables = SearchStationBoard(fromStation);

                    //Abfahrszeiten in List-View abfüllen
                    foreach (var timetable in timetables)
                    {
                        string connectionString = string.Format(timetable.Stop.Departure.TimeOfDay.ToString().Substring(0,5) + " h  "
                            + timetable.Name.ToString() + " "
                            + timetable.To.ToString() + " ");

                        lstBoxConnections.Items.Add(connectionString);
                    }
                }
            }
        }

        private List<StationBoard> SearchStationBoard(string fromStation)
        {
            //Abfahrsplan auslesen
            Transport transport = new Transport();
            return transport.GetStationBoard(fromStation, transport.GetStations(fromStation).StationList[0].Id).Entries;
        }
    }
}
