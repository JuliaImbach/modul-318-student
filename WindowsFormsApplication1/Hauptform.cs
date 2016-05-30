using SwissTransport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SwissTransportTimetable
{
    public partial class Hauptform : Form
    {
        public Hauptform()
        {
            InitializeComponent();
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
            /*string startStation = txtStartStation.Text;
            if (e.KeyCode.ToString() != "Back" && e.KeyCode.ToString() != "Delete" && e.KeyCode.ToString() != "Up" && e.KeyCode.ToString() != "Down" && startStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung
            {
                var source = Task.Factory.StartNew(() => Autocomplete(startStation));
                txtStartStation.AutoCompleteCustomSource = source.Result;
                txtStartStation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtStartStation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }*/
        }

        /// <summary>
        ///  Während des Schreibens in der Textbox "Endstation"
        ///  wird das AutoComplete für die Stationen geladen
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void EndStation_KeyUp(object sender, KeyEventArgs e)
        {
            //Autocomplete hinzufügen
            /*string endStation = txtEndStation.Text;
            if (e.KeyCode.ToString() != "Back" && e.KeyCode.ToString() != "Delete" && e.KeyCode.ToString() != "Up" && e.KeyCode.ToString() != "Down" && endStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung         
            {
                var source = Task.Factory.StartNew(() => Autocomplete(endStation));
                txtEndStation.AutoCompleteCustomSource = source.Result;
                txtEndStation.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                txtEndStation.AutoCompleteSource = AutoCompleteSource.CustomSource;
            }*/
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

                //Stationen auslesen und validieren
                bool isValid = true;
                string startStation = txtStartStation.Text;
                string endStation = txtEndStation.Text;
                if (!string.IsNullOrEmpty(startStation))
                {
                    var foundEndStation = Task.Factory.StartNew(() => SearchStation(startStation));
                    if (foundEndStation.Result.Find(x => x.Name.Contains(startStation)) == null)
                    {
                        isValid = false;
                        Cursor = Cursors.Default;
                        MessageBox.Show("Die Startstation ist ungültig.");
                    }
                }

                if (isValid)
                {
                    if (!string.IsNullOrEmpty(endStation))
                    {
                        var foundEndStation = Task.Factory.StartNew(() => SearchStation(endStation));
                        if (foundEndStation.Result.Find(x => x.Name.Contains(endStation)) == null)
                        {
                            isValid = false;
                            Cursor = Cursors.Default;
                            MessageBox.Show("Die Endstation ist ungültig.");
                        }
                    }
                    else
                    {
                        Cursor = Cursors.Default;
                        MessageBox.Show("Sie müssen eine Endstation angeben.");
                    }
                }

                if (isValid)
                {
                    StatusBarLabel.Text = "Verbindungen werden geladen...";
                    string date = Convert.ToDateTime(dateConnection.Text).ToString("yyyy-MM-dd");
                    string time = Convert.ToDateTime(dateConnection.Text).ToShortTimeString();
                    bool isArrival = chbAnkunft.Checked;

                    //Verbindungen auslesen
                    var connections = Task.Factory.StartNew(() => SearchConnectionDate(startStation, endStation, date, time, isArrival));

                    //ListView leeren
                    listViewConnection.Items.Clear();

                    //Spalten zu ListView leeren und neue Spalten hinzufügen
                    listViewConnection.Columns.Clear();
                    listViewConnection.Columns.Add("Abfahrt", 50);
                    listViewConnection.Columns.Add("Gleis", 40);
                    listViewConnection.Columns.Add("von Station", (listViewConnection.Width - 300) / 2);
                    listViewConnection.Columns.Add("Ankuft", 50);
                    listViewConnection.Columns.Add("Gleis", 40);
                    listViewConnection.Columns.Add("zu Station", (listViewConnection.Width - 300) / 2);
                    listViewConnection.Columns.Add("Dauer", 100);

                    if (connections.Result.Count != 0)
                    {
                        foreach (var connection in connections.Result)
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
                            item.SubItems.Add(connection.From.Platform);
                            item.SubItems.Add(connection.From.Station.Name);
                            item.SubItems.Add(Convert.ToDateTime(connection.To.Arrival).ToShortTimeString());
                            item.SubItems.Add(connection.To.Platform);
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
                    var foundFromStation = Task.Factory.StartNew(() => SearchStation(fromStation));
                    if (foundFromStation.Result.Find(x => x.Name.Contains(fromStation)) == null)
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
        ///  Es wird die Form Mail für den Mailversand geöffnet
        ///  und der Nachrichtentext mit der gewählten Verbindung
        ///  zusammengesetzt.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void btnMail_Click(object sender, EventArgs e)
        {
            string nachricht = "";
            bool head = true;

            if (listViewConnection.SelectedItems.Count > 0)
            {
                var index = listViewConnection.SelectedIndices[0];
                do
                {
                    for (int spalte = 0; spalte < listViewConnection.Columns.Count; spalte++)
                    {
                        //Spaltenkopf
                        if (head)
                        {
                            nachricht += listViewConnection.Columns[spalte].Text + "\t";
                        }
                        else
                        {
                            nachricht += listViewConnection.Items[index].SubItems[spalte].Text + "\t";
                        }
                    }

                    head = head ? false : true;
                    nachricht += "\n";
                } while (!head);

                Mail mail = new Mail(nachricht);
                mail.ShowDialog();

            }
            else
            {
                MessageBox.Show("Sie müssen zuerst eine Verbindung wählen.");
            }
        }

        /// <summary>
        ///  Mit einem Click auf das Label "maps" wird der
        ///  aktuelle Standort im Browser auf Googlemaps geöffnet.
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void mapsStartStation_Click(object sender, EventArgs e)
        {
            string stationName = txtStartStation.Text;

            if (!string.IsNullOrEmpty(stationName))
            {
                var foundEndStation = Task.Factory.StartNew(() => SearchStation(stationName));
                if (foundEndStation.Result.Find(x => x.Name.Contains(stationName)) == null)
                {
                    MessageBox.Show("Die Startstation ist ungültig.");
                }
                else
                {
                    Station station = GetStation(stationName);

                    StatusBarLabel.Text = "Karte wird geöffnet.";
                    ShowGoogleMapsRoute(station.Coordinate.XCoordinate, station.Coordinate.YCoordinate);
                }

            }

        }

        /// <summary>
        ///  Mit einem Click auf das Label "maps" wird der
        ///  aktuelle Standort im Browser auf Googlemaps geöffnet.
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void mapsEndStation_Click(object sender, EventArgs e)
        {
            string stationName = txtEndStation.Text;
            if (!string.IsNullOrEmpty(stationName))
            {
                var foundEndStation = Task.Factory.StartNew(() => SearchStation(stationName));
                if (foundEndStation.Result.Find(x => x.Name.Contains(stationName)) == null)
                {
                    MessageBox.Show("Die Endstation ist ungültig.");
                }
                else
                {
                    Station station = GetStation(stationName);

                    StatusBarLabel.Text = "Karte wird geöffnet.";
                    ShowGoogleMapsRoute(station.Coordinate.XCoordinate, station.Coordinate.YCoordinate);
                }

            }
        }

        /// <summary>
        ///  Während des Schreibens in der Textbox "Startstation"
        ///  wird das AutoComplete für die Stationen geladen
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void startStation_TextChanged(object sender, EventArgs e)
        {
            //Autocomplete hinzufügen
            string startStation = txtStartStation.Text;
            if (startStation.Length == 3) //Nur bei Textlänge 3, sonst Überlastung
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
        private void endStation_TextChanged(object sender, EventArgs e)
        {
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
        public List<Station> SearchStation(string station)
        {
            //Station auslesen
            Transport transport = new Transport();
            var transportStation = transport.GetStations(station).StationList;
            return transportStation;
            //return transportStation != null ? transportStation.StationList[0].Name.ToString() : null;
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
        ///  Es werden alle Verbindungen zwischen fromStation
        ///  und toStation zu einem bestimmten Zeitpunkt ausgelesen 
        ///  und als Liste zurückgegeben.
        /// </summary>
        /// <param name="fromStation">Stationsname Startstation</param>
        /// <param name="endStation">Stationsname Endstation</param>
        /// <param name="date">Datum der Verbindung</param>
        /// <param name="time">Zeitpunkt der Verbindung</param>
        /// <param name="isArrival">Unterscheidung Ankunfts und Abfahrtszeit</param>
        /// <returns>Connection-List: Liste mit Verbindungen</returns>
        private List<Connection> SearchConnectionDate(string fromStation, string toStation, string date, string time, bool isArrivalDate)
        {
            //Verbindungen auslesen
            Transport transport = new Transport();
            return transport.GetConnectionsDate(fromStation, toStation, date, time, (isArrivalDate ? "1" : "0")).ConnectionList;
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
        ///  Öffnet Googlemaps im Browser
        /// </summary>
        ///  <param name="xKoordinate">X-Koordinate der Station</param>
        /// <param name="yKoordinate">Y-Koordinate der Station</param>
        public static void ShowGoogleMapsRoute(double xKoordinate, double yKoordinate)
        {
            Process.Start(String.Format("http://maps.google.de/maps?q={0},{1}", xKoordinate, yKoordinate));
        }

        /// <summary>
        ///  List das Stationobjekt zu einem Stationsname aus.
        /// </summary>
        ///  <param name="stationName">Stationsname</param>
        ///  <returns>Station: Station</returns>
        public Station GetStation(string stationName)
        {
            Transport transport = new Transport();
            var transportStation = transport.GetStations(stationName).StationList[0];
            return transportStation;
        }
    }
}
