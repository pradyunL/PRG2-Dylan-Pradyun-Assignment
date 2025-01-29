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

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

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

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// FEATURE 5
void AssigningBoardingGateToFlight()
{
    Console.WriteLine("=============================================");
    Console.WriteLine("Assign a Boarding Gate to a Flight");
    Console.WriteLine("=============================================");

    while (true)
    {
        Console.WriteLine("Enter Flight Number:");
        string flightNumber = formatFlightNumber(Console.ReadLine());

        if (!terminal.flights.ContainsKey(flightNumber))
        {
            Console.WriteLine("Flight not found.");
            continue;
        }

        Flight flight = terminal.flights[flightNumber];
        Console.WriteLine("Enter Boarding Gate Name:");
        string? gateName = Console.ReadLine().ToUpper();

        if (!terminal.boardingGates.ContainsKey(gateName))
        {
            Console.WriteLine("Boarding Gate not found. Please enter a valid gate:");
            gateName = Console.ReadLine();
            continue;
        }

        BoardingGate gate = terminal.boardingGates[gateName];

        if (gate.flight != null)
        {
            Console.WriteLine("Boarding Gate is already assigned to another flight. Please enter a different gate:");
            gateName = Console.ReadLine();
            continue;
        }
        string SpecialRequestCode = flight.GetType().Name;
        if (SpecialRequestCode == "NORMFlight")
        {
            SpecialRequestCode = "None";
        }
        else if (SpecialRequestCode == "DDJBFlight")
        {
            SpecialRequestCode = "DDJB";
        }
        else if (SpecialRequestCode == "LWTTFlight")
        {
            SpecialRequestCode = "LWTT";
        }
        else if (SpecialRequestCode == "CFFTFlight")
        {
            SpecialRequestCode = "CFFT";
        }

        gate.flight = flight;

        Console.WriteLine($"Flight Number: {flight.FlightNumber}");
        Console.WriteLine($"Origin: {flight.Origin}");
        Console.WriteLine($"Destination: {flight.Destination}");
        Console.WriteLine($"Expected Time: {flight.ExpectedTime}");
        Console.WriteLine($"Special Request Code: {SpecialRequestCode}");
        Console.WriteLine($"Boarding Gate Name: {gate.gateName}");
        Console.WriteLine($"Supports DDJB: {gate.supportsDDJB}");
        Console.WriteLine($"Supports CFFT: {gate.supportsCFFT}");
        Console.WriteLine($"Supports LWTT: {gate.supportsLWTT}");

        Console.WriteLine("Would you like to update the status of the flight? (Y/N)");
        string updateStatus = Console.ReadLine();

        if (updateStatus.ToUpper() == "Y")
        {
            Console.WriteLine("1. Delayed");
            Console.WriteLine("2. Boarding");
            Console.WriteLine("3. On Time");
            Console.WriteLine("Please select the new status of the flight:");
            int statusOption = Convert.ToInt32(Console.ReadLine());

            if (statusOption == 1)
            {
                flight.Status = "Delayed";
            }
            else if (statusOption == 2)
            {
                flight.Status = "Boarding";
            }
            else if (statusOption == 3)
            {
                flight.Status = "On Time";
            }
        }
        else
        {
            flight.Status = "On Time";
        }

        Console.WriteLine($"{flight.Status} Flight {flight.FlightNumber} has been assigned to Boarding Gate {gate.gateName}!");
        break;
    }
}

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// FEATURE 6
void CreateNewFlight()
{
    Console.Write("Enter Flight Number: ");
    string? flightNumber = Console.ReadLine().ToUpper();
    Console.Write("Enter Origin: ");
    string? Origin = Console.ReadLine().ToUpper();
    Console.Write("Enter Destination: ");
    string? Destination = Console.ReadLine().ToUpper();
    Console.Write("Enter Expected Departure/Arrival Time (dd/mm/yyyy hh:mm): ");
    DateTime ExpectedTime = Convert.ToDateTime(Console.ReadLine());
    Console.Write("Enter Special Request Code (CFFT/DDJB/LWTT/None): ");
    string? SpecialRequestCode = Console.ReadLine();
    Flight flight;
    string Status = "Scheduled";
    if (SpecialRequestCode.ToUpper() == "DDJB")
    {
        flight = new DDJBFlight(flightNumber, Origin, Destination, ExpectedTime, Status);
    }
    else if (SpecialRequestCode.ToUpper() == "LWTT")
    {
        flight = new LWTTFlight(flightNumber, Origin, Destination, ExpectedTime, Status);
    }
    else if (SpecialRequestCode.ToUpper() == "CFFT")
    {
        flight = new CFFTFlight(flightNumber, Origin, Destination, ExpectedTime, Status);
    }
    else
    {
        flight = new NORMFlight(flightNumber, Origin, Destination, ExpectedTime, Status);
    }
    terminal.flights.Add(flightNumber, flight);


    using (StreamWriter sw = File.AppendText("flights.csv"))
    {
        sw.WriteLine($"{flightNumber},{Origin},{Destination},{ExpectedTime},{SpecialRequestCode}");
    }

    Console.WriteLine($"{flightNumber} has been added!");
    Console.WriteLine("Would you like to add another flight? (Y/N)");
    string? addAnotherFlight = Console.ReadLine();
    if (addAnotherFlight.ToUpper() == "Y")
    {
        CreateNewFlight();
    }
    else
    {
        //displayMenu();
    }
}

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// FEATURE 7

void listFullFlightDetails()
{
    airlineListing();
    while (true)
    {
        Console.Write("Enter Flight Number(e.g. sq123, etc.): ");
        string flightNumber = formatFlightNumber(Console.ReadLine());
        if (terminal.flights.ContainsKey(flightNumber))
        {
            Flight chosenFlight = terminal.flights[flightNumber];
            Airline chosenAirline = terminal.GetAirlineFromFlight(chosenFlight);

            string assignedGateName = "Not assigned";
            foreach (var gate in terminal.boardingGates.Values)
            {
                if (gate.flight != null && gate.flight.FlightNumber == flightNumber)
                {
                    assignedGateName = gate.gateName;
                    break;
                }
            }

            Console.WriteLine($"\nFlight Number: {chosenFlight.FlightNumber} \nAirline Name: {chosenAirline.Name} " +
                $"\nOrigin: {chosenFlight.Origin} \nDestination: {chosenFlight.Destination} " +
                $"\nExpected Time: {chosenFlight.ExpectedTime} \nSpecial Request Code: {chosenFlight.Status} \nBoarding Gate: {assignedGateName}");
            break;
        }
        else
        {
            Console.WriteLine("Flight not found. Make sure to enter in a valid flight number that was displayed earlier.");
            continue;
        }
    }
}
listFullFlightDetails();

string formatFlightNumber(string input) // function to format flight number justt in case enters something like sq123, this makes it SQ 123
{
    string cleaned = input.Replace(" ", "").ToUpper();
    if (cleaned.Length >= 2)
    {
        return $"{cleaned.Substring(0, 2)} {cleaned.Substring(2)}";
    }
    return input;
}

void airlineListing() // made listing of all airlines and flights of it into a function to increase reusability for next feature 8
{
    while (true){
        foreach (var airline in terminal.airlines.Values)
        {
            Console.WriteLine(airline);
        }
        Console.Write("Enter Two Letter Airline Code(e.g. SQ, etc.): ");
        string airlineCode = Console.ReadLine().ToUpper();
        if (terminal.airlines.ContainsKey(airlineCode))
        {
            Airline chosenAirline = terminal.airlines[airlineCode];
            foreach (var flight in chosenAirline.Flights.Values)
            {
                Console.WriteLine($"\nFlight Number: {flight.FlightNumber} \nOrigin: {flight.Origin} \nDestination: {flight.Destination}");
            }
            break;
        }
        else
        {
            Console.WriteLine("Airline not found.");
            continue;
        }
    }
}

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// FEATURE 8

void modifyFlightDetails()
{
    airlineListing();

}