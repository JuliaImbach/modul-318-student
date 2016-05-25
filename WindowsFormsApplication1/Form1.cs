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

        /// <summary>
        ///  Beim Klick auf den Button "Suchen" werden die Verbindungen
        ///  zwischen Startstation und Endstation gesucht und in der
        ///  ListView aufgelistet.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void cmdSearch_Click(object sender, EventArgs e)
        {
            try
            {
                //Sanduhr einblenden
                Cursor.Current = Cursors.WaitCursor;
                StatusBarLabel.Text = "Verbindungen werden geladen...";

                //Stationen auslesen und validieren
                bool isValid = true;
                string startStation = txtStartStation.Text;
                if (!string.IsNullOrEmpty(startStation))
                {
                    var foundEndStation = Task.Factory.StartNew(() => SearchStation(startStation));
                    if (foundEndStation.Result != startStation)
                    {
                        isValid = false;
                        MessageBox.Show("Die Startstation ist ungültig.");
                    }
                }

                string endStation = txtEndStation.Text;
                if (!string.IsNullOrEmpty(endStation))
                {
                    var foundEndStation = Task.Factory.StartNew(() => SearchStation(endStation));
                    if (foundEndStation.Result != endStation)
                    {
                        isValid = false;
                        MessageBox.Show("Die Endstation ist ungültig.");
                    }
                }
                else
                {
                    MessageBox.Show("Sie müssen eine Endstation angeben.");
                }

                if (isValid)
                {
                    //Verbindungen auslesen
                    var connections = SearchConnection(startStation, endStation);

                    //ListView leeren
                    listViewConnection.Items.Clear();

                    //Spalten zu ListView leeren und neue Spalten hinzufügen
                    listViewConnection.Columns.Clear();
                    listViewConnection.Columns.Add("Abfahrt", 50);
                    listViewConnection.Columns.Add("von Station", (listViewConnection.Width - 220) / 2);
                    listViewConnection.Columns.Add("Ankuft", 50);
                    listViewConnection.Columns.Add("zu Station", (listViewConnection.Width - 220) / 2);
                    listViewConnection.Columns.Add("Dauer", 100);

                    if (connections.Count != 0)
                    {
                        foreach (var connection in connections)
                        {
                            var duration = "";

                            //Dauer hinzufügen
                            if (connection.Duration.Substring(0, 2) != "00")
                            {
                                duration = connection.Duration.Substring(0, 2) + " Tag(e) ";
                            }

                            duration += Convert.ToDateTime(connection.Duration.Substring(3)).ToShortTimeString() + " Stunden";

                            //Item abfüllen
                            ListViewItem item = new ListViewItem(Convert.ToDateTime(connection.From.Departure).ToShortTimeString());
                            item.SubItems.Add(connection.From.Station.Name);
                            item.SubItems.Add(Convert.ToDateTime(connection.To.Arrival).ToShortTimeString());
                            item.SubItems.Add(connection.To.Station.Name);
                            item.SubItems.Add(duration);

                            //Item zu Listview hinzufügen
                            listViewConnection.Items.Add(item);
                        }

                        StatusBarLabel.Text = "Verbindungen wurden geladen.";
                    }
                    else
                    {
                        StatusBarLabel.Text = "Keine Verbindungen verfügbar.";
                    }
                }
            }
            catch (Exception ex)
            {
                StatusBarLabel.Text = "Verbindungen konnten nicht geladen werden: " + ex.Message;
            }

            //Cursor zurücksetzen
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///  Während des Schreibens in der Textbox "Startstation"
        ///  wird das AutoComplete für die Stationen geladen
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void StartStation_KeyUp(object sender, KeyEventArgs e)
        {
            //Autocomplete hinzufügen
            string startStation = txtStartStation.Text;
            if (e.KeyCode.ToString() != "Back" && e.KeyCode.ToString() != "Delete" && startStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung
            {
                var source = Task.Factory.StartNew(() => Autocomplete(startStation));
                txtStartStation.AutoCompleteCustomSource = source.Result;
                txtStartStation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtStartStation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        /// <summary>
        ///  Während des Schreibens in der Textbox "Endstation"
        ///  wird das AutoComplete für die Stationen geladen
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void EndStation_KeyUp(object sender, KeyEventArgs e)
        {
            Console.WriteLine(e.KeyCode.ToString());

            //Autocomplete hinzufügen
            string endStation = txtEndStation.Text;
            if (endStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung         
            {
                var source = Task.Factory.StartNew(() => Autocomplete(endStation));
                txtEndStation.AutoCompleteCustomSource = source.Result;
                txtEndStation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtEndStation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }
        }

        /// <summary>
        ///  Die Funktion überprüft, ob die eigegebene Station
        ///  exisitiert.
        /// </summary>
        /// <param name="station">Stationsname</param>
        /// <returns>string: Vollständiger Stationsname</returns>
        public string SearchStation(string station)
        {
            //Station auslesen
            Transport transport = new Transport();
            var transportStation = transport.GetStations(station);
            return transportStation != null ? transportStation.StationList[0].Name.ToString() : null;
        }

        /// <summary>
        ///  Es werden alle Statationen die mit dem Parameter
        ///  beginnen ausgelsen und als Source zurückgegeben.
        /// </summary>
        /// <param name="station">Stationsname</param>
        /// <returns>AutoCompleteStringCollection: Source mit Stationen für AutoComplete</returns>
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

        /// <summary>
        ///  Es werden alle Verbindungen zwischen fromStation
        ///  und toStation ausgelesen und als Liste zurückgegeben.
        /// </summary>
        /// <param name="fromStation">Stationsname Startstation</param>
        /// <param name="endStation">Stationsname Endstation</param>
        /// <returns>Connection-List: Liste mit Verbindungen</returns>
        private List<Connection> SearchConnection(string fromStation, string toStation)
        {
            //Verbindungen auslesen
            Transport transport = new Transport();
            return transport.GetConnections(fromStation, toStation).ConnectionList;
        }

        /// <summary>
        ///  Mit dem Buttonklick "Abfahrtstafel" werden alle möglichen
        ///  Verbindungen geladen und in der ListView angezeigt.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void btnStationBoard_Click(object sender, EventArgs e)
        {
            try
            {
                StatusBarLabel.Text = "Abfahrtszeiten werden geladen...";

                //Sanduhr einblenden
                Cursor.Current = Cursors.WaitCursor;

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

                        //ListVie leeren
                        listViewConnection.Items.Clear();

                        //Spalten zu ListView leeren und neue Spalten hinzufügen
                        listViewConnection.Columns.Clear();
                        listViewConnection.Columns.Add("Abfahrtszeit", 100);
                        listViewConnection.Columns.Add("Linie", 100);
                        listViewConnection.Columns.Add("Endstation", listViewConnection.Width - 220);

                        if (timetables.Count != 0)
                        {
                            foreach (var timetable in timetables)
                            {
                                //Item abfüllen
                                ListViewItem item = new ListViewItem(timetable.Stop.Departure.TimeOfDay.ToString().Substring(0, 5) + " h");
                                item.SubItems.Add(timetable.Name);
                                item.SubItems.Add(timetable.To);

                                //Item zu Listview hinzufügen
                                listViewConnection.Items.Add(item);                                
                            }

                            StatusBarLabel.Text = "Abfahrtszeiten wurden geladen.";
                        }
                        else
                        {
                            StatusBarLabel.Text = "Zu dieser Status sind keine Abfahrtszeiten verfügbar.";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                StatusBarLabel.Text = "Abfahrtstafel konnte nicht geladen werden: " + ex.Message;
            }

            //Cursor zurücksetzen
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///  Die Funktion lädt alle Abfahrtszeiten und Endstationen.
        /// </summary>
        /// <param name="station">Stationsname</param>
        /// <returns>StationBoard-List: Liste mit Abfahrtszeiten</returns>
        private List<StationBoard> SearchStationBoard(string station)
        {

            //Abfahrsplan auslesen
            Transport transport = new Transport();
            return transport.GetStationBoard(station, transport.GetStations(station).StationList[0].Id).Entries;
        }

        /// <summary>
        ///  Mit einem Klick auf den Button "Map" wird GoogleMaps
        ///  mit der Endstation geladen.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void btnMap_Click(object sender, EventArgs e)
        {
            string toStation = txtEndStation.Text;
        }

        /// <summary>
        ///  GoogelMaps Karte laden.
        /// </summary>
        /// <param name="station">Stationsname</param>
        private void OpenGoogleMaps(string station)
        {
            Transport transport = new Transport();
            var stationCor = transport.GetStations(station).StationList[0];
            var CorX = stationCor.Coordinate.XCoordinate;
            var CorY = stationCor.Coordinate.YCoordinate;
        }

        /// <summary>
        ///  
        /// </summary>
        private void FormLoad(object sender, EventArgs e)
        {

        }
    }
}
