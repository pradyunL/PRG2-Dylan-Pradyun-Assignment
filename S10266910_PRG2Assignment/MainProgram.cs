using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Classes;


//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// FEATURE 1

// loads up airlines.csv into a dictionary

Terminal terminal = new Terminal("Terminal 5");
void ReadAirlineFile(string filepath, Terminal terminal)
{
    string[] lines = File.ReadAllLines(filepath);
    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i];
        string[] details = line.Split(',');

        string airlineName = details[0];
        string airlineCode = details[1];

        Airline airline = new Airline(airlineName, airlineCode);
        terminal.AddAirline(airline);
    }
}
ReadAirlineFile("airlines.csv", terminal);


// loads up boardinggates.csv into a dictionary
void ReadBoardingGateFile(string filepath, Terminal terminal)
{
    string[] lines = File.ReadAllLines(filepath);
    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i];
        string[] details = line.Split(',');

        string gateName = details[0];
        bool supportsCFFT = bool.Parse(details[1]);
        bool supportsDDJB = bool.Parse(details[2]);
        bool supportsLWTT = bool.Parse(details[3]);

        BoardingGate boardingGate = new BoardingGate(gateName, supportsCFFT, supportsDDJB, supportsLWTT);
        terminal.AddBoardingGate(boardingGate);
    }
}
ReadBoardingGateFile("boardinggates.csv",terminal);


//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// FEATURE 2
void ReadFlightFile(string filepath)
{
    string[] lines = File.ReadAllLines(filepath);
    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i];
        string[] data = line.Split(',');

        string flightNumber = data[0];
        string origin = data[1];
        string destination = data[2];
        DateTime expectedTime = DateTime.Parse(data[3]);
        string status = data[4];
        Flight flight;

        if (status == "DDJB")
        {
            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, status);
        }
        else if (status == "LWTT")
        {
            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, status);
        }
        else if (status == "CFFT")
        {
            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, status);
        }
        else
        {
            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, status);
        }
        Airline airline = terminal.GetAirlineFromFlight(flight);
        string airlineName = airline.Code;
        if (airline != null && terminal.airlines.ContainsKey(airline.Code))
        {
            terminal.airlines[airline.Code].AddFlight(flight); // add flights to AIRLINE class flight dictionary
            terminal.flights[flight.FlightNumber] = flight; // add flights to TERMINAL class flight dictionary
        }
        else
        {
            Console.WriteLine($"Warning: No airline found for flight {flightNumber}");
        }
    }
}
ReadFlightFile("flights.csv");

// FEATURE 3

void listAllFlights()
{
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (var airline in terminal.airlines.Values)
    {
        foreach (var flight in terminal.flights.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"),-24}");
        }
    }
}
listAllFlights();

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// FEATURE 4

void listAllBoardingGates()
{
    Console.WriteLine("Gate Name   Supports CFFT   Supports DDJB   Supports LWTT");
    foreach (var boardingGate in terminal.boardingGates.Values)
    {
        Console.WriteLine($"{boardingGate.gateName,-12} {boardingGate.supportsCFFT,-15} {boardingGate.supportsDDJB,-15} {boardingGate.supportsLWTT,-15}");
    }
}

// FEATURE 7

void listFullFlightDetails() 
{
    foreach (var airline in terminal.airlines.Values)
    {
        Console.WriteLine(airline);
    }
    Console.Write("Enter Two Letter Airline Code(e.g. SQ, etc.): ");
    string airlineCode = Console.ReadLine();


}
listFullFlightDetails();