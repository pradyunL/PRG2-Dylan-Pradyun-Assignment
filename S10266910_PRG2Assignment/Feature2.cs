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
namespace S10266910_PRG2Assignment
{
    class Feature2
    {
        public Dictionary<string, Flight> FlightDict = new Dictionary<string, Flight>();
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
                FlightDict.Add(flightNumber, flight);

                readFlight("flights.csv");
            }
        }
    }
}
