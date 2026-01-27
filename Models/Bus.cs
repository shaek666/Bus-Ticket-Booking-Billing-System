using BusTicketBookingSystem.Enums;

namespace BusTicketBookingSystem.Models
{
    public class Bus
    {
        public int BusId { get; private set; }
        public string CoachNumber { get; set; }
        public BusType BusType { get; set; }
        public int TotalSeats { get; private set; }
        public int Rows { get; private set; }

        public Bus(int busId, string coachNumber, BusType busType)
        {
            BusId = busId;
            CoachNumber = coachNumber;
            BusType = busType;
            
            if (busType == BusType.Business)
            {
                Rows = 9;
                TotalSeats = 27;
            }
            else
            {
                Rows = 9;
                TotalSeats = 36;
            }
        }

        public override string ToString()
        {
            return $"{CoachNumber} | {BusType} | Seats: {TotalSeats}";
        }
    }
}