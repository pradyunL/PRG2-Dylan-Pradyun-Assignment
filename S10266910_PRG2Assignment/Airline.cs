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
        public Dictionary<string, Flight> Flights { get; set; } = new Dictionary<string, Flight>();
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
            double discount = 0;
            double totalFee = 0;
            foreach (var flight in Flights.Values)
            {
                if (int.Parse(flight.FlightNumber) % 3 == 0)
                {
                    discount += 350;
                }
                if (flight.ExpectedTime.ToString("hh tt") == "11 AM" || flight.ExpectedTime.ToString("hh tt") == "09 PM")
                {
                    discount += 110;
                }
                if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
                {
                    discount += 25;
                }
                if (flight is NORMFlight)
                {
                    discount += 50;
                }
                if (flight.FlightNumber.Length < 5)
                {
                    totalFee *= 0.97;
                }
            }
            return totalFee - discount;
        }
        public override string ToString()
        {
            return "Airline: (" + Code + "), " + Name;
        }
    }
}

