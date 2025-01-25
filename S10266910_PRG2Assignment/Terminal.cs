using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================
namespace Classes
{
    class Terminal
    {
        public string terminalName { get; set; }
        public Dictionary<string, Airline> airlines { get; set; }
        public Dictionary<string, Flight> flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> boardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> gateFees { get; set; } = new Dictionary<string, double>();
        public Terminal() { }
        public Terminal(string tn)
        {
            terminalName = tn;
            airlines = new Dictionary<string, Airline>();
            flights = new Dictionary<string, Flight>();
            boardingGates = new Dictionary<string, BoardingGate>();
            gateFees = new Dictionary<string, double>();
        }
        public bool AddAirline(Airline airline)
        {
            if (airlines.ContainsKey(airline.Code))
                return false;

            airlines.Add(airline.Code, airline);
            return true;
        }

        public bool AddBoardingGate(BoardingGate gate)
        {
            if (boardingGates.ContainsKey(gate.gateName))
                return false;

            boardingGates.Add(gate.gateName, gate);
            return true;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            foreach (var airline in airlines.Values)
            {
                if (airline.Flights.ContainsKey(flight.FlightNumber))
                {
                    return airline;
                }
            }
            return null;
        }

        public void PrintAirlineFees()
        {
            foreach (Airline airline in airlines.Values)
            {
                foreach (var flight in airline.Flights.Values)
                {
                    Console.WriteLine($"Airline: {airline.Name}, Flight: {flight.FlightNumber}, Fee: {flight.CalculateFees()}");
                }
            }
        }

        public override string ToString()
        {
            return $"Terminal: {terminalName}, Airlines: {airlines.Count}, Flights: {flights.Count}, Gates: {boardingGates.Count}";
        }
    }
}
