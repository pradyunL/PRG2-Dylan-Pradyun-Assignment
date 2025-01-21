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

// Feature 3

void listAllFlights()
{
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected");
    foreach (var airline in airlines)
    {
        foreach (var flight in airline.Flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-20} {flight.Origin,-20} {flight.Destination,-20} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"),-30}");
        }
    }
}


