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
        public Dictionary<string, Airline> airlines { get; set; } = new Dictionary<string, Airline>();
        public Dictionary<string, Flight> flights { get; set; } = new Dictionary<string, Flight>();
        public Dictionary<string, BoardingGate> boardingGates { get; set; } = new Dictionary<string, BoardingGate>();
        public Dictionary<string, double> gateFees { get; set; } = new Dictionary<string, double>();
        public Terminal() { }
        public Terminal(string tn)
        {
            terminalName = tn;
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
            {
                Console.WriteLine($"Boarding gate {gate.gateName} already exists.");
                return false;
            }

            boardingGates.Add(gate.gateName, gate);
            Console.WriteLine($"Boarding gate {gate.gateName} added successfully.");
            return true;
        }

        public Airline GetAirlineFromFlight(Flight flight)
        {
            string[] parts = flight.FlightNumber.Split(' ');
            string airlineCode = parts[0];

            if (airlines.TryGetValue(airlineCode, out Airline airline))
            {
                return airline;
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
