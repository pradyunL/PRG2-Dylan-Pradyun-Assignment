
//==========================================================
// Student Number	: S10267635J
// Student Name	: Dylan Loh
// Partner Name	: Pradyun
//==========================================================

namespace Classes
{
    class Flight
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
        public virtual double CalculateFees(int n)
        {
            if (n < 0)
                return -1;
            return n * 100;
        }
        public override string ToString()
        {
            return "Flight: " + FlightNumber + " (" + Origin + " to " + Destination + ")\n" +
                "Expected Time: " + ExpectedTime + "\n" +
                "Status: " + Status;
        }
    }
}
