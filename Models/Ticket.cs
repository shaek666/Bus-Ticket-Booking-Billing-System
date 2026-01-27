using System;

namespace BusTicketBookingSystem.Models
{
    public class Ticket
    {
        public int TicketId { get; private set; }
        public int UserId { get; set; }
        public int ScheduleId { get; set; }
        public string SeatNumber { get; set; }
        public DateTime BookingDate { get; private set; }
        public string CoachNumber { get; set; }
        public DateTime JourneyDate { get; set; }

        public Ticket(int ticketId, int userId, int scheduleId, string seatNumber, string coachNumber, DateTime journeyDate)
        {
            TicketId = ticketId;
            UserId = userId;
            ScheduleId = scheduleId;
            SeatNumber = seatNumber;
            BookingDate = DateTime.Now;
            CoachNumber = coachNumber;
            JourneyDate = journeyDate;
        }

        public override string ToString()
        {
            return $"Ticket ID: {TicketId} | Coach No: {CoachNumber} | Journey: {JourneyDate:yyyy-MM-dd} {JourneyDate:HH:mm} | Seat: {SeatNumber}";
        }
    }
}