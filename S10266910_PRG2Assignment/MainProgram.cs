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
        bool supportsDDJB = bool.Parse(details[1]);
        bool supportsCFFT = bool.Parse(details[2]);
        bool supportsLWTT = bool.Parse(details[3]);

        BoardingGate boardingGate = new BoardingGate(gateName,supportsDDJB, supportsCFFT, supportsLWTT);
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
        string specialRequestCode = data[4];
        string status = "Scheduled";
        Flight flight;

        if (specialRequestCode == "DDJB")
        {
            flight = new DDJBFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
        }
        else if (specialRequestCode == "LWTT")
        {
            flight = new LWTTFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
        }
        else if (specialRequestCode == "CFFT")
        {
            flight = new CFFTFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
        }
        else
        {
            flight = new NORMFlight(flightNumber, origin, destination, expectedTime, "Scheduled");
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
        Console.WriteLine($"{boardingGate.gateName,-11} {boardingGate.supportsCFFT,-15} {boardingGate.supportsDDJB,-15} {boardingGate.supportsLWTT,-14}");
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
        displayMenu();
    }
}

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// FEATURE 7

void displayAirlineFlights()
{
    airlineChoice();
    flightChoice();
}

void flightChoice()
{
    while (true)
    {
        Console.Write("\nEnter Flight Number(e.g. sq123, etc.): ");
        string flightNumber = formatFlightNumber(Console.ReadLine());
        if (terminal.flights.ContainsKey(flightNumber))
        {
            Flight chosenFlight = terminal.flights[flightNumber];
            Airline chosenAirline = terminal.GetAirlineFromFlight(chosenFlight);

            string assignedGateName = "Unassigned";
            foreach (var gate in terminal.boardingGates.Values)
            {
                if (gate.flight != null && gate.flight.FlightNumber == flightNumber)
                {
                    assignedGateName = gate.gateName;
                    break;
                }
            }
            string flightSPC = "NORM";
            if (chosenFlight is CFFTFlight)
            {
                flightSPC = "CFFT";
            }
            else if (chosenFlight is DDJBFlight)
            {
                flightSPC = "DDJB";
            }
            else if (chosenFlight is LWTTFlight)
            {
                flightSPC = "LWTT";
            }

            Console.WriteLine($"\nFlight Number: {chosenFlight.FlightNumber} \nAirline Name: {chosenAirline.Name} " +
                $"\nOrigin: {chosenFlight.Origin} \nDestination: {chosenFlight.Destination} " +
                $"\nExpected Time: {chosenFlight.ExpectedTime} \nSpecial Request Code: {flightSPC} \nBoarding Gate: {assignedGateName}");
            break;
        }
        else
        {
            Console.WriteLine("Flight not found. Make sure to enter in a valid flight number that was displayed earlier.");
            continue;
        }
    }
}
void airlineChoice() // made listing of all airlines and flights of it into a function to increase reusability for next feature 8
//listFullFlightDetails();

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
string formatFlightNumber(string input) // function to format flight number justt in case enters something like sq123, this makes it SQ 123
{
    string cleaned = input.Replace(" ", "").ToUpper();
    if (cleaned.Length >= 2)
    {
        return $"{cleaned.Substring(0, 2)} {cleaned.Substring(2)}";
    }
    return input;
}

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// FEATURE 8 

void modifyFlightDetails()
{
    airlineChoice();
    while (true)
    {
        Console.WriteLine("\n[1] Modify Flight");
        Console.WriteLine("[2] Delete Flight");
        Console.WriteLine("[3] Exit");
        try
        {
            int modificationChoice = Convert.ToInt32(Console.ReadLine());
            if (modificationChoice == 1)
            {
                while (true)
                {
                    Console.Write("\nEnter Flight Number(e.g. sq123, etc.): ");
                    string flightNumber = formatFlightNumber(Console.ReadLine());
                    Flight chosenFlight = null;
                    
                    if (terminal.flights.ContainsKey(flightNumber))
                    {
                        chosenFlight = terminal.flights[flightNumber];
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
                        string flightSPC = "NORM";
                        if (chosenFlight is CFFTFlight)
                        {
                            flightSPC = "CFFT";
                        }
                        else if (chosenFlight is DDJBFlight)
                        {
                            flightSPC = "DDJB";
                        }
                        else if (chosenFlight is LWTTFlight)
                        {
                            flightSPC = "LWTT";
                        }

                        Console.WriteLine($"\nFlight Number: {chosenFlight.FlightNumber} \nAirline Name: {chosenAirline.Name} " +
                            $"\nOrigin: {chosenFlight.Origin} \nDestination: {chosenFlight.Destination} " +
                            $"\nExpected Time: {chosenFlight.ExpectedTime} \nSpecial Request Code: {flightSPC} \nBoarding Gate: {assignedGateName} \nStatus: {chosenFlight.Status}");
                    }
                    else
                    {
                        Console.WriteLine("Flight not found. Make sure to enter in a valid flight number that was displayed earlier.");
                        continue;
                    }

                    Console.WriteLine("\nWhat would you like to modify?");
                    Console.WriteLine("[1] Origin");
                    Console.WriteLine("[2] Destination");
                    Console.WriteLine("[3] Expected Time");
                    Console.WriteLine("[4] Status");
                    Console.WriteLine("[5] Special Request Code");
                    Console.WriteLine("[6] Boarding Gate");
                    Console.WriteLine("[7] Exit");
                    int modifyChoice = Convert.ToInt32(Console.ReadLine());

                    switch (modifyChoice)
                    {
                        case 1:
                            Console.Write("Enter new Origin: ");
                            chosenFlight.Origin = Console.ReadLine();
                            break;
                        case 2:
                            Console.Write("Enter new Destination: ");
                            chosenFlight.Destination = Console.ReadLine();
                            break;
                        case 3:
                            Console.Write("Enter new Expected Time (dd/mm/yyyy hh:mm): ");
                            chosenFlight.ExpectedTime = Convert.ToDateTime(Console.ReadLine());
                            break;
                        case 4:
                            Console.Write("Enter new Status(Delayed,Boarding,On Time): ");
                            chosenFlight.Status = Console.ReadLine();
                            break;
                        case 5:
                            Console.Write("Enter new Special Request Code (CFFT/DDJB/LWTT/NORM): ");
                            string newRequestCode = Console.ReadLine().ToUpper();
                            if (newRequestCode == "CFFT")
                            {
                                chosenFlight = new CFFTFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status);
                            }
                            else if (newRequestCode == "DDJB")
                            {
                                chosenFlight = new DDJBFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status);
                            }
                            else if (newRequestCode == "LWTT")
                            {
                                chosenFlight = new LWTTFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status);
                            }
                            else if (newRequestCode == "NORM")
                            {
                                chosenFlight = new NORMFlight(chosenFlight.FlightNumber, chosenFlight.Origin, chosenFlight.Destination, chosenFlight.ExpectedTime, chosenFlight.Status);
                            }
                            else
                            {
                                Console.WriteLine("Invalid Special Request Code.");
                            }
                            terminal.flights.Remove(flightNumber);
                            terminal.flights[flightNumber] = chosenFlight;
                            break;
                        case 6:
                            Console.Write("Enter new Boarding Gate: ");
                            string newGateName = Console.ReadLine().ToUpper();
                            if (terminal.boardingGates.ContainsKey(newGateName))
                            {
                                BoardingGate newGate = terminal.boardingGates[newGateName];

                                if (newGate.flight == null)
                                {
                                    foreach (var gate in terminal.boardingGates.Values)
                                    {
                                        if (gate.flight != null && gate.flight.FlightNumber == flightNumber)
                                        {
                                            gate.flight = null;
                                            break;
                                        }
                                    }
                                    newGate.flight = chosenFlight;
                                }
                                else
                                {
                                    Console.WriteLine("Boarding Gate is already assigned to another flight.");
                                    continue;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Boarding Gate not found.");
                                continue;
                            }
                            break;
                        case 7:
                            break;
                        default:
                            Console.WriteLine("Invalid choice.");
                            break;
                    }
                    Console.WriteLine("Flight details updated successfully!");
                    chosenFlight = terminal.flights[flightNumber];
                    Airline chosenAirline2 = terminal.GetAirlineFromFlight(chosenFlight);
                    string assignedGateName2 = "Unassigned";
                    foreach (var gate in terminal.boardingGates.Values)
                    {
                        if (gate.flight != null && gate.flight.FlightNumber == flightNumber)
                        {
                            assignedGateName2 = gate.gateName;
                            break;
                        }
                    }
                    string flightSPC2 = "NORM";
                    if (chosenFlight is CFFTFlight)
                    {
                        flightSPC2 = "CFFT";
                    }
                    else if (chosenFlight is DDJBFlight)
                    {
                        flightSPC2 = "DDJB";
                    }
                    else if (chosenFlight is LWTTFlight)
                    {
                        flightSPC2 = "LWTT";
                    }

                    Console.WriteLine($"\nFlight Number: {chosenFlight.FlightNumber} \nAirline Name: {chosenAirline2.Name} " +
                        $"\nOrigin: {chosenFlight.Origin} \nDestination: {chosenFlight.Destination} " +
                        $"\nExpected Time: {chosenFlight.ExpectedTime} \nSpecial Request Code: {flightSPC2} \nBoarding Gate: {assignedGateName2} \nStatus: {chosenFlight.Status}");
                    break;
                }
            }
            else if (modificationChoice == 2)
            {
                while (true)
                {
                    Console.Write("\nEnter Flight to Delete(e.g. sq123, etc.): ");
                    string flightNumber = formatFlightNumber(Console.ReadLine());
                    if (terminal.flights.ContainsKey(flightNumber))
                    {
                        Console.WriteLine($"Are you sure you want to delete flight {flightNumber}?(Y/N)");
                        string deleteChoice = Console.ReadLine().ToUpper();
                        if (deleteChoice == "Y")
                        {
                            terminal.flights.Remove(flightNumber);
                            Console.WriteLine($"Flight {flightNumber} has been deleted.");
                            break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Flight not found. Make sure to enter in a valid flight number that was displayed earlier.");
                        continue;
                    }
                }
            }
            else if (modificationChoice == 3)
            {
                break;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please enter 1 or 2.");
                continue;
            }
        }
        
        catch (FormatException)
        {
            Console.WriteLine("Invalid choice. Please enter a number.");
            continue;
        }
    }
}
modifyFlightDetails();

//==========================================================
// Student Number	: S10266910B
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// FEATURE 9
void DisplayFlightSchedule()
{
    var sortedFlights = terminal.flights.Values.OrderBy(f => f.ExpectedTime).ToList();

    Console.WriteLine("=============================================");
    Console.WriteLine("Flight Schedule for Changi Airport Terminal 5");
    Console.WriteLine("=============================================");
    Console.WriteLine(
        "Flight Number   Airline Name           Origin                 Destination            Expected Departure/Arrival Time\n" +
        "Status          Boarding Gate");
    foreach (var flight in sortedFlights)
    {
        foreach (var airline in terminal.airlines.Values)
        {
            if (airline.Flights.ContainsKey(flight.FlightNumber))
            {
                string assignedGateName = "Not assigned";
                foreach (var gate in terminal.boardingGates.Values)
                {
                    if (gate.flight != null && gate.flight.FlightNumber == flight.FlightNumber)
                    {
                        assignedGateName = gate.gateName;
                        break;
                    }
                }
                Console.WriteLine($"{flight.FlightNumber,-15} {airline.Name,-22} {flight.Origin,-22} {flight.Destination,-22} {flight.ExpectedTime.ToString("dd/MM/yyyy hh:mm:ss tt"),-24}");
                Console.WriteLine($"{flight.Status,-15} {assignedGateName}");
            }
        }
    }
}
//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

// Advanced Feature (a)

void BulkAssignFlightsToGates()
{
    Queue<Flight> unassignedFlights = new Queue<Flight>();
    int totalUnassignedFlights = 0;
    int totalUnassignedGates = 0;

    foreach (var flight in terminal.flights.Values)
    {
        bool isAssigned = false;
        foreach (var gate in terminal.boardingGates.Values)
        {
            if (gate.flight?.FlightNumber == flight.FlightNumber)
            {
                isAssigned = true;
                break;
            }
        }
        if (!isAssigned)
        {
            unassignedFlights.Enqueue(flight);
            totalUnassignedFlights++;
        }
    }

    foreach (var gate in terminal.boardingGates.Values)
    {
        if (gate.flight == null)
        {
            totalUnassignedGates++;
        }
    }

    Console.WriteLine($"Unassigned Flights: {totalUnassignedFlights}");
    Console.WriteLine($"Unassigned Gates: {totalUnassignedGates}");

    int autoAssignedFlights = 0;
    int autoAssignedGates = 0;

    while (unassignedFlights.Count > 0)
    {
        Flight currentFlight = unassignedFlights.Dequeue();
        BoardingGate suitableGate = null;

        bool hasSpecialRequest = currentFlight is CFFTFlight || currentFlight is DDJBFlight || currentFlight is LWTTFlight;

        foreach (var gate in terminal.boardingGates.Values)
        {
            if (gate.flight != null) //this checks if gate is unassigned and won't progress to the rest of the code until it finds unassigned
            {
                continue;
            }
            if (hasSpecialRequest)
            {
                if (currentFlight is CFFTFlight && gate.supportsCFFT)
                {
                    suitableGate = gate;
                    break;
                }
                else if (currentFlight is DDJBFlight && gate.supportsDDJB)
                {
                    suitableGate = gate;
                    break;
                }
                else if (currentFlight is LWTTFlight && gate.supportsLWTT)
                {
                    suitableGate = gate;
                    break;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                if (!gate.supportsCFFT && !gate.supportsDDJB && !gate.supportsLWTT)
                {
                    suitableGate = gate;
                    break;
                }
                else
                {
                    continue;
                }
            }
        }

        if (suitableGate != null)
        {
            suitableGate.flight = currentFlight;
            autoAssignedFlights++;
            autoAssignedGates++;

            string specialCode = "None";
            if (currentFlight is CFFTFlight) { specialCode = "CFFT"; }
            else if (currentFlight is DDJBFlight) { specialCode = "DDJB"; }
            else if (currentFlight is LWTTFlight) { specialCode = "LWTT"; }

            while (true)
            {
                Airline chosenAirline = terminal.GetAirlineFromFlight(currentFlight);
                string flightNumber = currentFlight.FlightNumber;
                string assignedGateName = "Unassigned";
                foreach (var gate in terminal.boardingGates.Values)
                {
                    if (gate.flight != null && gate.flight.FlightNumber == flightNumber)
                    {
                        assignedGateName = gate.gateName;
                        break;
                    }
                }

                Console.WriteLine($"\nFlight Number: {currentFlight.FlightNumber} \nAirline Name: {chosenAirline.Name} " +
                    $"\nOrigin: {currentFlight.Origin} \nDestination: {currentFlight.Destination} " +
                    $"\nExpected Time: {currentFlight.ExpectedTime} \nSpecial Request Code: {specialCode} \nBoarding Gate: {assignedGateName}");
                break;
            }
        }
    }

    int totalFlights = terminal.flights.Count;
    int totalGates = terminal.boardingGates.Count;

    Console.WriteLine($"\nProcessed Results:");
    Console.WriteLine($"Assigned Flights: {autoAssignedFlights}/{totalUnassignedFlights}");
    Console.WriteLine($"Assigned Gates: {autoAssignedGates}/{totalUnassignedGates}");

    double flightPercentage = totalUnassignedFlights > 0 ?
        (autoAssignedFlights * 100.0) / totalUnassignedFlights : 0;
    double gatePercentage = totalUnassignedGates > 0 ?
        (autoAssignedGates * 100.0) / totalUnassignedGates : 0;

    Console.WriteLine($"Automatically Assigned: {flightPercentage:F2}% of flights, " + $"{gatePercentage:F2}% of gates");
}


//==========================================================
// Student Number	: S10266910B
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

// ADVANCED FEATURE (b)
void DisplayTotalFeePerAirline()
{
    // Check if all flights have been assigned boarding gates
    foreach (var flight in terminal.flights.Values)
    {
        bool gateAssigned = terminal.boardingGates.Values.Any(g => g.flight?.FlightNumber == flight.FlightNumber);
        if (!gateAssigned)
        {
            Console.WriteLine($"Flight {flight.FlightNumber} has not been assigned a boarding gate. Please assign all flights before running this feature.");
            return;
        }
    }

    double totalFees = 0;
    double totalDiscounts = 0;

    Console.WriteLine("Airline Fees for the Day:");
    foreach (var airline in terminal.airlines.Values)
    {
        double airlineFees = 0;
        double airlineDiscounts = 0;
        int flightCount = airline.Flights.Count;
        int earlyLateFlightCount = 0;
        int specificOriginCount = 0;
        int noSpecialRequestCount = 0;

        foreach (var flight in airline.Flights.Values)
        {
            // Calculate base fees
            if (flight.Origin == "SIN")
            {
                airlineFees += 800;
            }
            if (flight.Destination == "SIN")
            {
                airlineFees += 500;
            }

            // Add boarding gate base fee
            airlineFees += 300;

            // Add special request fees
            if (flight is DDJBFlight)
            {
                airlineFees += 300;
            }
            else if (flight is CFFTFlight)
            {
                airlineFees += 150;
            }
            else if (flight is LWTTFlight)
            {
                airlineFees += 500;
            }

            // Check for early or late flights
            if (flight.ExpectedTime.ToString("hh tt") == "11 AM" || flight.ExpectedTime.ToString("hh tt") == "09 PM")
            {
                earlyLateFlightCount++;
            }

            // Check for specific origins
            if (flight.Origin == "DXB" || flight.Origin == "BKK" || flight.Origin == "NRT")
            {
                specificOriginCount++;
            }

            // Check for special request codes
            if (!(flight is DDJBFlight) && !(flight is CFFTFlight) && !(flight is LWTTFlight))
            {
                noSpecialRequestCount++;
            }
        }

        // Apply discounts
        airlineDiscounts += (flightCount / 3) * 350;
        airlineDiscounts += earlyLateFlightCount * 110;
        airlineDiscounts += specificOriginCount * 25;
        airlineDiscounts += noSpecialRequestCount * 50;

        if (flightCount > 5)
        {
            airlineDiscounts += airlineFees * 0.03;
        }

        double finalFees = airlineFees - airlineDiscounts;
        totalFees += airlineFees;
        totalDiscounts += airlineDiscounts;

        Console.WriteLine($"Airline: {airline.Name}");
        Console.WriteLine($"Original Subtotal: {airlineFees:C}");
        Console.WriteLine($"Discounts: {airlineDiscounts:C}");
        Console.WriteLine($"Final Total: {finalFees:C}");
        Console.WriteLine();
    }

    double finalTotalFees = totalFees - totalDiscounts;
    double discountPercentage = (totalDiscounts / totalFees) * 100;

    Console.WriteLine("Summary:");
    Console.WriteLine($"Subtotal of All Airline Fees: {totalFees:C}");
    Console.WriteLine($"Subtotal of All Airline Discounts: {totalDiscounts:C}");
    Console.WriteLine($"Final Total of Airline Fees Collected: {finalTotalFees:C}");
    Console.WriteLine($"Percentage of Discounts Over Final Total: {discountPercentage:F2}%");
}



//==========================================================
//DISPLAY MENU
//==========================================================
void displayMenu()
{

    DisplayTotalFeePerAirline();

    Console.WriteLine("\nLoading Airlines...\r\n" +
    "8 Airlines Loaded!\r\nLoading Boarding Gates...\r\n" +
    "66 Boarding Gates Loaded!\r\n" +
    "Loading Flights...\r\n" +
    "30 Flights Loaded!\r\n\n\n\n");

    Console.WriteLine(
        "=============================================\r\n" +
        "Welcome to Changi Airport Terminal 5\r\n" +
        "=============================================\r\n" +
        "1. List All Flights\r\n" +
        "2. List Boarding Gates\r\n" +
        "3. Assign a Boarding Gate to a Flight\r\n" +
        "4. Create Flight\r\n" +
        "5. Display Airline Flights\r\n" +
        "6. Modify Flight Details\r\n" +
        "7. Display Flight Schedule\r\n" +
        "0. Exit\r\n");

    Options();
}
displayMenu();
void Options()
{
    try
    {
        Console.WriteLine("Please select your option:");
        int option = Convert.ToInt32(Console.ReadLine());

        if (option == 1)
        {
            listAllFlights();
        }
        else if (option == 2)
        {
            listAllBoardingGates();
        }
        else if (option == 3)
        {
            AssigningBoardingGateToFlight();
        }
        else if (option == 4)
        {
            CreateNewFlight();
        }
        else if (option == 5)
        {
            displayAirlineFlights();
        }
        else if (option == 6)
        {
            modifyFlightDetails();
        }
        else if (option == 7)
        {
            DisplayFlightSchedule();
        }
        else if (option == 0)
        {
            Console.WriteLine("Goodbye!");
        }
        else
        {
            Console.WriteLine("Invalid option. Please try again.");
            Options();
        }
    }
    catch (FormatException ex)
    {
        Console.WriteLine("Invalid input format. Please enter a valid number.");
        Options();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
        Options();
    }
}