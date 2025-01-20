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
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

void ReadFlightDetails(string filepath)
{
    
    string[] lines = File.ReadAllLines(filepath);
    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i];
        string[] details = line.Split(',');

        string FlightNumber = details[0];
        string Origin = details[1];
        string Destination = details[2];
        DateTime ExpectedTime = details[3];

        Flight flight = new Flight(FlightNumber, Origin, Destination, ExpectedTime);
        flightDict.Add(flight);
    }
}
ReadFlightDetails("flight.csv");
