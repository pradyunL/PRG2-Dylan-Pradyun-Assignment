using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airport_classes
{
    class LWTTFlight : Flight
    {
        public double RequestFee { get; set; }
        public LWTTFlight() { }
        public LWTTFlight(string fn, string o, string dest, DateTime et, string s, double rf)
            : base(fn, o, dest, et, s)
        {
            RequestFee = rf;
        }
        public override double CalculateFee(int n)
        {
            if (n < 0)
                return -1;
            return n * RequestFee;
        }
        public override string ToString()
        {
            return "LWTTFlight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status + "\n" +
                "Request Fee: " + RequestFee;
        }
    }
