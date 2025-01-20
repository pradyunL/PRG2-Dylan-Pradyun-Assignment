using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

namespace Classes
{
    class Airline
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public Dictionary<string, Flight> Flights { get; set; }
        public Airline() { }
        public Airline(string n, string c)
        {
            Name = n;
            Code = c;
            Flights = new Dictionary<string, Flight>();
        }
        public bool AddFlight(Flight f)
        {
            if (Flights.ContainsKey(f.FlightNumber))
                return false;
            Flights.Add(f.FlightNumber, f);
            return true;
        }
        public bool RemoveFlight(string f)
        {
            if (!Flights.ContainsKey(f))
                return false;
            Flights.Remove(f);
            return true;
        }
        public double CalculateFees()
        {
            double TotalFee = 0.0;
            foreach (KeyValuePair<string, Flight> flight in Flights)
            {
                TotalFee += flight.Value.CalculateFees();
            }
            return TotalFee;

        }
        public override string ToString()
        {
            string s = "Airline: " + Name + " (" + Code + ")\n";
            foreach (KeyValuePair<string, Flight> f in Flights)
                s += f.Value.ToString() + "\n";
            return s;
        }
    }
}

