using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using S10266910_PRG2Assignment;

using System.IO;
Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();

Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();

Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// feature 3
void listAllFlights()
{
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (var airline in airlineDict.Values)
    {
        foreach (var flight in FlightDict.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"),-24}");
        }
    }
}

