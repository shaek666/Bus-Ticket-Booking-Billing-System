using System;

namespace BusTicketBookingSystem.Models
{
    public class SeatReservation
    {
        public string SeatNumber { get; set; }
        public DateTime ReservationTime { get; set; }
        public bool IsPaid { get; set; }

        public SeatReservation(string seatNumber)
        {
            SeatNumber = seatNumber;
            ReservationTime = DateTime.Now;
            IsPaid = false;
        }

        public bool IsExpired()
        {
            return !IsPaid && (DateTime.Now - ReservationTime).TotalMinutes >= 10;
        }
    }
}