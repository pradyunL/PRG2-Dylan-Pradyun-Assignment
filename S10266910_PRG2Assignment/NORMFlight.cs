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
    class NORMFlight : Flight
    {
        public NORMFlight() { }
        public NORMFlight(string fn, string o, string dest, DateTime et, string s)
            : base(fn, o, dest, et, s)
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
            return TotalFee;
        }
        public override string ToString()
        {
            return "NORMFlight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status;
        }
    }
}
