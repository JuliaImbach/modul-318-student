using System.Threading.Tasks;

namespace SwissTransport
{
    public interface ITransport
    {
        Task<Stations> GetStations(string query);
        StationBoardRoot GetStationBoard(string station, string id);
        Connections GetConnections(string fromStation, string toStation);
        Connections GetConnectionsDate(string fromStation, string toStattion, string date, string time, string isArrivalTime);
    }
}