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
    class DDJBFlight : Flight
    {
        public double RequestFee { get; set; }
        public DDJBFlight() { }
        public DDJBFlight(string fn, string o, string dest, DateTime et, string s, double rf)
            : base(fn, o, dest, et, s)
        {
            RequestFee = rf;
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
            return TotalFee + 300;
        }
        public override string ToString()
        {
            return "DDJBFlight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status + "\n" +
                "Request Fee: " + RequestFee;
        }
    }
}
