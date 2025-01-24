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
            RequestFee = 150;
        }

        public CFFTFlight(string fn, string o, string dest, DateTime et, string s) : base(fn, o, dest, et, s)
        {
        }

        public double CalculateFees()
        {
            double BaseFee = 300;
            double TotalFee = 0;
            if (Destination == "SIN")
            {
                TotalFee = BaseFee + 500;
            }
            if (Origin == "SIN")
            {
                TotalFee = TotalFee + 800;
            }
            return TotalFee + RequestFee;
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
