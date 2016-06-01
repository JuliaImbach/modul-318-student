using SwissTransport;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SwissTransportTimetable
{
    public partial class Hauptform : Form
    {
        // Membervariabeln
        private Transport transport = new Transport();

        // Konstruktoren
        public Hauptform()
        {
            InitializeComponent();
        }

        // Properties
        /// <summary>
        /// Definition des Styles für die Mail
        /// </summary>
        private string CellStyle { get; } = "style=\"border: 1px solid black; padding: 3px;\"";

        /// <summary>
        ///  Beim Klick auf den Button "Suchen" werden die Verbindungen
        ///  zwischen Startstation und Endstation gesucht und in der
        ///  ListView aufgelistet.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            // Stationen auslesen und validieren
            string fromStation = txtFromStation.Text;
            string toStation = txtToStation.Text;

            lblVerbindungen.Text = "Verbindungen";

            StatusBarLabel.Text = "Die Eingaben werden überprüft...";

            // Start- und Endstation validieren
            var valid = ValidateStations(txtFromStation, true);         
            if (!valid.Result)
            {
                return;
            }

            valid = ValidateStations(txtToStation, false);
            if (!valid.Result)
            {
                return;
            }

            StatusBarLabel.Text = "Verbindungen werden geladen...";
            string date = Convert.ToDateTime(dateConnection.Text).ToString("yyyy-MM-dd");
            string time = Convert.ToDateTime(dateConnection.Text).ToShortTimeString();
            bool isArrival = chbAnkunft.Checked;

            // Verbindungen auslesen
            List<Connection> connections = null;
            try
            {
                connections = transport.GetConnectionsDate(fromStation, toStation, date, time, isArrival ? "1" : "0").ConnectionList;
            }
            catch (Exception ex)
            {
                StatusBarLabel.Text = "Verbindungen konnten vom Webservice nicht geladen werden: " + ex.Message;
            }

            // ListView leeren
            listViewConnection.Items.Clear();

            // Spalten zu ListView leeren und neue Spalten hinzufügen
            listViewConnection.Columns.Clear();
            listViewConnection.Columns.Add("Abfahrt", 50);
            listViewConnection.Columns.Add("Gleis", 40);
            listViewConnection.Columns.Add("von Station", (listViewConnection.Width - 300) / 2);
            listViewConnection.Columns.Add("Ankuft", 50);
            listViewConnection.Columns.Add("Gleis", 40);
            listViewConnection.Columns.Add("zu Station", (listViewConnection.Width - 300) / 2);
            listViewConnection.Columns.Add("Dauer", 100);

            if (connections.Count == 0)
            {
                StatusBarLabel.Text = "Keine Verbindungen verfügbar.";
                return;
            }

            // Verbindungen in ListView abfüllen
            foreach (var connection in connections)
            {
                var duration = "";

                // Dauer hinzufügen
                if (connection.Duration.Substring(0, 2) != "00")
                {
                    duration = connection.Duration.Substring(0, 2) + " Tag(e) ";
                }
                duration += Convert.ToDateTime(connection.Duration.Substring(3)).ToShortTimeString() + " Stunden";

                // Item abfüllen
                ListViewItem item = new ListViewItem(new[] {
                        Convert.ToDateTime(connection.From.Departure).ToShortTimeString(),
                        connection.From.Platform,
                        connection.From.Station.Name,
                        Convert.ToDateTime(connection.To.Arrival).ToShortTimeString(),
                        connection.To.Platform,
                        connection.To.Station.Name,
                        duration
                    });

                listViewConnection.Items.Add(item);
            }

            StatusBarLabel.Text = "Verbindungen wurden geladen.";

            // Cursor zurücksetzen
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///  Mit dem Buttonklick "Abfahrtstafel" werden alle möglichen
        ///  Verbindungen geladen und in der ListView angezeigt.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private async void btnStationBoard_Click(object sender, EventArgs e)
        {
            StatusBarLabel.Text = "Abfahrtszeiten werden geladen...";

            // Stationsname auslesen
            string fromStation = txtFromStation.Text;

            // Startstation validieren
            var valid = ValidateStations(txtFromStation, true);
            if (!valid.Result)
            {
                return;
            }

            lblVerbindungen.Text = "Verbindungen ab " + txtFromStation.Text;

            // Sanduhr einblenden
            Cursor.Current = Cursors.WaitCursor;

            // Stations-ID auslesen
            Stations station;
            try
            {
                station = await transport.GetStations(fromStation);
            }
            catch(Exception ex)
            {
                StatusBarLabel.Text = "Die Stations-ID konnte nicht ausgelsen werden: " + ex.Message;
                Cursor = Cursors.Default;
                return;
            }

            // Abfahrtszeiten auslesen
            List<StationBoard> timetables;
            try
            {
                timetables = transport.GetStationBoard(fromStation, station.StationList[0].Id).Entries;
            }
            catch (Exception ex)
            {
                StatusBarLabel.Text = "Abfahrtszeiten konnten vom Webservice nicht geladen werden: " + ex.Message;
                Cursor = Cursors.Default;
                return;
            }

            // ListView leeren und neue Spalten hinzufügen
            listViewConnection.Items.Clear();
            listViewConnection.Columns.Clear();
            listViewConnection.Columns.Add("Abfahrtszeit", 100);
            listViewConnection.Columns.Add("Linie", 100);
            listViewConnection.Columns.Add("Endstation", listViewConnection.Width - 220);

            if (timetables.Count == 0)
            {
                StatusBarLabel.Text = "Zu dieser Status sind keine Abfahrtszeiten verfügbar.";
                return;
            }

            foreach (var timetable in timetables)
            {
                // Item abfüllen
                ListViewItem item = new ListViewItem(new[] {
                    timetable.Stop.Departure.TimeOfDay.ToString().Substring(0, 5) + " h",
                    timetable.Name,
                    timetable.To
                });

                listViewConnection.Items.Add(item);
            }

            StatusBarLabel.Text = "Abfahrtszeiten wurden geladen.";

            //Cursor zurücksetzen
            Cursor = Cursors.Default;
        }

        /// <summary>
        ///  Es wird die Form Mail für den Mailversand geöffnet.
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">Click-Event</param>
        private void btnMail_Click(object sender, EventArgs e)
        {
            if (listViewConnection.SelectedItems.Count == 0)
            {
                MessageBox.Show("Sie müssen zuerst eine Verbindung wählen.");
                return;
            }

            // Nachricht zusammensetzen
            string nachricht = GetTableHeader() + GetTableLines();

            Mail mail = new Mail(nachricht);
            mail.ShowDialog();
        }

        /// <summary>
        ///  Tabellenkopf für die Mail zusammensetzen
        /// </summary>
        /// <returns>string: Tabellenkopf</returns>
        private string GetTableHeader()
        {
            // Spaltenköpfe in List laden
            List<string> names = new List<string>();
            for (int spalte = 0; spalte < listViewConnection.Columns.Count; spalte++)
            {
                var das = listViewConnection.Columns[spalte].Text;
                var deses = listViewConnection.Columns[spalte].Text.ToString();
                names.Add(listViewConnection.Columns[spalte].Text);
            }

            // Message in HTML abfüllen
            var message = "<tr>";
            foreach (var name in names)
            {
                message += $"<th {CellStyle}>{name}</th>";
            }
            message += "</tr>";
            return message;
        }

        /// <summary>
        ///  Tabellenzeile für die Mail zusammensetzen
        /// </summary>
        /// <returns>string: Tabellenzeile</returns>
        private string GetTableLines()
        {
            var index = listViewConnection.SelectedIndices[0];

            // Spalten-Werte von selektierter Zeile in List laden
            List<string> values = new List<string>();
            for (int spalte = 0; spalte < listViewConnection.Columns.Count; spalte++)
            {
                values.Add(listViewConnection.Items[index].SubItems[spalte].Text);
            }

            // Message in HTML abfüllen
            var message = "<tr>";
            foreach (var value in values)
            {
                message += $"<td {CellStyle}>{value}</td>";
            }
            message += "</tr>";
            return message;
        }

        /// <summary>
        ///  Mit einem Click auf das Label "maps" wird der
        ///  aktuelle Standort im Browser auf Googlemaps geöffnet.
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void mapsStartStation_Click(object sender, EventArgs e)
        {
            OpenMaps(txtFromStation, true);
        }

        /// <summary>
        ///  Mit einem Click auf das Label "maps" wird der
        ///  aktuelle Standort im Browser auf Googlemaps geöffnet.
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void mapsEndStation_Click(object sender, EventArgs e)
        {
            OpenMaps(txtToStation, false);
        }

        /// <summary>
        ///  Während des Schreibens in der Textbox "Startstation"
        ///  wird das AutoComplete für die Stationen geladen
        /// </summary>
        /// /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void startStation_TextChanged(object sender, EventArgs e)
        {
            // Nur bei Textlänge 3, sonst Überlastung
            if (txtFromStation.Text.Length != 3)
            {
                return;
            }
            
            // Autocomplete hinzufügen
            AutoComplete(txtFromStation, true);
        }

        /// <summary>
        ///  Während des Schreibens in der Textbox "Endstation"
        ///  wird das AutoComplete für die Stationen geladen
        /// </summary>
        /// <param name="sender">Sender</param>
        /// <param name="e">KeyUp-Event</param>
        private void endStation_TextChanged(object sender, EventArgs e)
        {
            // Nur bei Textlänge 3, sonst Überlastung
            if (txtToStation.Text.Length != 3)
            {
                return;
            }

            // Autocomplete hinzufügen
            AutoComplete(txtToStation, false);
        }

        /// <summary>
        ///  Es werden alle Statationen die mit dem Parameter
        ///  beginnen ausgelsen und als Source zurückgegeben.
        /// </summary>
        /// <param name="station">Stationsname</param>
        /// <returns>AutoCompleteStringCollection: Source mit Stationen für AutoComplete</returns>
        public async Task<AutoCompleteStringCollection> AutocompleteSource(string station)
        {
            //Liste mit allen Station in eine Liste laden
            var source = new AutoCompleteStringCollection();

            var transportList = await transport.GetStations(station);
            foreach (var transportStation in transportList.StationList)
            {
                source.Add(transportStation.Name.ToString());
            }

            return source;
        }

        /// <summary>
        ///  Öffnet Googlemaps im Browser
        /// </summary>
        ///  <param name="xKoordinate">X-Koordinate der Station</param>
        /// <param name="yKoordinate">Y-Koordinate der Station</param>
        private static void ShowGoogleMapsRoute(double xKoordinate, double yKoordinate)
        {
            string url = String.Format("http://maps.google.de/maps?q={0},{1}", xKoordinate, yKoordinate);
            frmWebBrowser webBrowser = new frmWebBrowser(url);
            webBrowser.Show();
        }

        /// <summary>
        ///  Validiert die Stationsnamen
        /// </summary>
        ///  <param name="txtStation">TextBox des Stationsnamen</param>
        /// <param name="isFromStation">Startstation</param>
        /// <returns>bool: Validation</returns>
        private async Task<bool> ValidateStations(TextBox txtStation, bool isFromStation)
        {
            // Sanduhr einblenden
            Cursor.Current = Cursors.WaitCursor;

            // Stationsname auslesen
            string stationName = txtStation.Text;

            // Prüfen, ob das Feld abgefüllt ist
            if (string.IsNullOrEmpty(stationName))
            {
                MessageBox.Show(string.Format("Sie müssen die {0} ausfüllen", isFromStation ? "Startstation" : "Endstation"));
                Cursor = Cursors.Default;
                return false;
            }

            // Prüfen, ob der Name gültig ist
            Stations foundStations;
            try
            {
                 foundStations = await transport.GetStations(stationName);
            }
            catch (Exception ex)
            {
                StatusBarLabel.Text = string.Format("Die {0} konnte nicht validiert werden: {1}", isFromStation ? "Startstation" : "Endstation", ex.Message);
                Cursor = Cursors.Default;
                return false;
            }

            if (foundStations.StationList.Find(x => x.Name.ToLower().Contains(stationName.ToLower())) == null)
            {
                MessageBox.Show(string.Format("Die {0} ist ungültig.", isFromStation ? "Startstation" : "Endstation"));
                Cursor = Cursors.Default;
                return false;
            }

            // Korrekter Stationsname abfüllen
            txtStation.Text = foundStations.StationList.Find(x => x.Name.ToLower().Contains(stationName.ToLower())).Name.ToString();
            Cursor = Cursors.Default;

            return true;
        }

        /// <summary>
        ///  Validierung der Daten für GoogleMaps-Aufruf
        /// </summary>
        ///  <param name="txtStation">TextBox des Stationsnamen</param>
        /// <param name="isFromStation">Startstation</param>
        private async void OpenMaps(TextBox txtStation, bool isFromStation)
        {
            // Station validieren
            bool valid = await ValidateStations(txtStation, isFromStation);
            if (!valid)
            {
                return;
            }

            // Station auslesen und Karte öffnen
            Stations station;
            try
            {
                station = await transport.GetStations(txtStation.Text);
            }
            catch (Exception ex)
            {
                StatusBarLabel.Text = string.Format("Die {0} konnte nicht gefunden werden: {1}", isFromStation ? "Startstation" : "Endstation", ex.Message);
                return;
            }

            StatusBarLabel.Text = "Karte wird geöffnet.";
            ShowGoogleMapsRoute(station.StationList[0].Coordinate.XCoordinate, station.StationList[0].Coordinate.YCoordinate);
        }

        /// <summary>
        ///  Fügt die AutoComplete-Werte zu der Textbox hinzu
        /// </summary>
        ///  <param name="txtStation">TextBox des Stationsnamen</param>
        /// <param name="isFromStation">Startstation</param>
        private async void AutoComplete(TextBox txtStation, bool isFromStation)
        {
            var source = await AutocompleteSource(txtStation.Text);
            var station = isFromStation ? txtFromStation : txtToStation;
            station.AutoCompleteCustomSource = source;
            station.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            station.AutoCompleteSource = AutoCompleteSource.CustomSource;
        }

        /// <summary>
        ///  Tooltips für einzelne Elemente hinzufügen
        /// </summary>
        ///  <param name="sender">Sender</param>
        /// <param name="e">Load-Event</param>
        private void OnLoad(object sender, EventArgs e)
        {
            // Tooltip erstellen
            ToolTip toolTip = new ToolTip();

            // Verzögerung definieren
            toolTip.AutoPopDelay = 5000;
            toolTip.InitialDelay = 1000;
            toolTip.ReshowDelay = 500;

            // Sichtbarkeit
            toolTip.ShowAlways = true;

            // Texte für Controls abfüllen
            toolTip.SetToolTip(this.btnStationboard, "Verbindungen ab Startstation anzeigen.");
            toolTip.SetToolTip(this.btnMail, "Markieren Sie eine Verbindung, um diese per Mail zu vernsenden.");
            toolTip.SetToolTip(this.mapsStartStation, "Station auf Googlemaps anzeigen.");
            toolTip.SetToolTip(this.mapsEndStation, "Station auf Googlemaps anzeigen.");
        }
    }
}
