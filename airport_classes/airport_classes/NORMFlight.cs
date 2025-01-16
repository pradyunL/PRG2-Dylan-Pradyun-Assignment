using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace airport_classes
{
    class NORMFlight : Flight
    {
        public NORMFlight() { }
        public NORMFlight(string fn, string o, string dest, DateTime et, string s)
            : base(fn, o, dest, et, s)
        {
        }
        public override string ToString()
        {
            return "NORMFlight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status;
        }
    }
}
