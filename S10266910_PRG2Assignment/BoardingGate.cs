using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10266910B
// Student Name	: Pradyun
// Partner Name	: Dylan Loh
//==========================================================

namespace Classes
{
    class BoardingGate
    {
        public string gateName { get; set; }
        public bool supportsCFFT { get; set; }
        public bool supportsDDJB { get; set; }
        public bool supportsLWTT { get; set; }
        public Flight flight { get; set; }
        public BoardingGate() { }
        public BoardingGate(string gn, bool cfft, bool ddjb, bool lwtt)
        {
            gateName = gn;
            supportsCFFT = cfft;
            supportsDDJB = ddjb;
            supportsLWTT = lwtt;
        }
        public double CalculateFees()
        {
            return 300;
        }
        public override string ToString()
        {
            return "BoardingGate: " + gateName + "\n" +
                "Supports CFFT: " + supportsCFFT + "\n" +
                "Supports DDJB: " + supportsDDJB + "\n" +
                "Supports LWTT: " + supportsLWTT;
        }
    }
}

