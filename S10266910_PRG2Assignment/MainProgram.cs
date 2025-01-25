using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Classes;
using System.IO;

Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();
Dictionary<string, Flight> flightDict = new Dictionary<string, Flight>();

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// feature 1
// loads up airlines.csv into a dictionary
void ReadAirlineFile(string filepath)
{
    string[] lines = File.ReadAllLines(filepath);
    for (int i = 1; i < lines.Length; i++)
    {
        string line = lines[i];
        string[] details = line.Split(',');

        string airlineName = details[0];
        string airlineCode = details[1];

        Airline airline = new Airline(airlineName, airlineCode);
        airlineDict.Add(airlineName, airline);
    }
}
ReadAirlineFile("airlines.csv");


// loads up boardinggates.csv into a dictionary
void ReadBoardingGateFile(string filepath)
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
        boardingGateDict.Add(gateName, boardingGate);
    }
}
ReadBoardingGateFile("boardinggates.csv");


//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// feature 2
void readFlight(string filepath)
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
        flightDict.Add(flightNumber, flight);

        readFlight("flights.csv");
    }
}

// feature 3
void listAllFlights()
{
    Console.WriteLine("Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time");
    foreach (var airline in airlineDict.Values)
    {
        foreach (var flight in flightDict.Values)
        {
            Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"),-24}");
        }
    }
}

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// feature 4

