using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Classes;

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// Feature 2

class Feature2
{
    static void Main(string[] args)
    {
        string filePath = "flights.csv";
        Dictionary<string, Flight> flights = LoadFlights(filePath);

        ListAllFlights(flights);
    }
    static Dictionary<string, Flight> LoadFlights(string filePath)
    {
        var flights = new Dictionary<string, Flight>();
        var timeFormat = "h:mm tt";

        var lines = File.ReadAllLines(filePath);
        foreach (var line in lines.Skip(1)) // Skip header line
        {
            var values = line.Split(',');

            if (DateTime.TryParse(values[3], out DateTime expectedTime))
            {
                var flight = new Flight(
                    values[0], // FlightNumber
                    values[1], // Origin
                    values[2], // Destination
                    expectedTime, // ExpectedTime
                    values[4]  // Status
                );
                flights.Add(flight.FlightNumber, flight);
            }
        }

        return flights;
    }

    static void ListAllFlights(Dictionary<string, Flight> flights)
    {
        Console.WriteLine($"{"Flight Number",-20} {"Origin",-20} {"Destination",-20} {"Expected",-25}");

        foreach (var flight in flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-20} {flight.Origin,-20} {flight.Destination,-20} {flight.ExpectedTime:dd/MM/yyyy hh:mm tt}");
        }
    }
}
