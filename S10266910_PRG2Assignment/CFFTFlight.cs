using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

namespace Classes
{
    class CFFTFlight : Flight
    {
        public double RequestFee { get; set; }
        public CFFTFlight() { }
        public CFFTFlight(string fn, string o, string dest, DateTime et, string s, double rf)
            : base(fn, o, dest, et, s)
        {
            RequestFee = rf;
        }
        public override double CalculateFees(int n)
        {
            if (n < 0)
                return -1;
            return n * RequestFee;
        }
        public override string ToString()
        {
            return "CFFTFlight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status + "\n" +
                "Request Fee: " + RequestFee;
        }


    }
}
