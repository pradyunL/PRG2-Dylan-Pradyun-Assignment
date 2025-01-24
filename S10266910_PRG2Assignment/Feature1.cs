using Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S10266910_PRG2Assignment
{
    class Feature1
    {
        //==========================================================
        // Student Number	: S10266910B
        // Student Name	: Pradyun
        // Partner Name	: Dylan Loh
        //==========================================================

        // Feature 1


        // loads up airlines.csv into a dictionary
        public Dictionary<string, Airline> airlineDict = new Dictionary<string, Airline>();
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
            ReadAirlineFile("airlines.csv");
        }
        


        // loads up boardinggates.csv into a dictionary
       public Dictionary<string, BoardingGate> boardingGateDict = new Dictionary<string, BoardingGate>();

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
            ReadBoardingGateFile("boardinggates.csv");
        }
    }
}
