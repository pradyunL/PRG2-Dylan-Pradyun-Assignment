
//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

namespace Classes
{
    abstract class Flight
    {
        public string FlightNumber { get; set; }
        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime ExpectedTime { get; set; }
        public string Status { get; set; }
        public Flight() { }
        public Flight(string fn, string o, string dest, DateTime et, string s)
        {
            FlightNumber = fn;
            Origin = o;
            Destination = dest;
            ExpectedTime = et;
            Status = s;
        }
        public double CalculateFees()
        {
            double BaseFee = 300;
            double TotalFee = 0;
            if (Destination == "SIN")
            {
                TotalFee += 500;
            }
            if (Origin == "SIN")
            {
                TotalFee += 800;
            }
            return TotalFee;
        }
        public override string ToString()
        {
            return "Flight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status;
        }
    }
}
