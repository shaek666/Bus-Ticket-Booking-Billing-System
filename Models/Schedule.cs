using System;
using System.Collections.Generic;
using System.Linq;

namespace BusTicketBookingSystem.Models
{
    public class Schedule
    {
        public int ScheduleId { get; private set; }
        public int BusId { get; set; }
        public string DepartureCity { get; set; }
        public string ArrivalCity { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public decimal TicketPrice { get; set; }
        private Dictionary<string, SeatReservation> ReservedSeats { get; set; }

        public Schedule(int scheduleId, int busId, string departureCity, string arrivalCity, DateTime departureDateTime, decimal ticketPrice)
        {
            ScheduleId = scheduleId;
            BusId = busId;
            DepartureCity = departureCity;
            ArrivalCity = arrivalCity;
            DepartureDateTime = departureDateTime;
            TicketPrice = ticketPrice;
            ReservedSeats = new Dictionary<string, SeatReservation>();
        }

        public void CleanExpiredReservations()
        {
            var expiredSeats = ReservedSeats.Where(kvp => kvp.Value.IsExpired()).Select(kvp => kvp.Key).ToList();
            foreach (var seat in expiredSeats)
            {
                ReservedSeats.Remove(seat);
            }
        }

        public bool IsSeatAvailable(string seatNumber)
        {
            CleanExpiredReservations();
            return !ReservedSeats.ContainsKey(seatNumber);
        }

        public void ReserveSeat(string seatNumber)
        {
            ReservedSeats[seatNumber] = new SeatReservation(seatNumber);
        }

        public void ConfirmSeat(string seatNumber)
        {
            if (ReservedSeats.ContainsKey(seatNumber))
            {
                ReservedSeats[seatNumber].IsPaid = true;
            }
        }

        public int GetAvailableSeatsCount(int totalSeats)
        {
            CleanExpiredReservations();
            return totalSeats - ReservedSeats.Count;
        }

        public Dictionary<string, string> GetSeatLayout(Bus bus)
        {
            CleanExpiredReservations();
            var layout = new Dictionary<string, string>();
            
            if (bus.BusType == Enums.BusType.Business)
            {
                // Business: 9 rows, 3 seats per row (A, B, C)
                for (int row = 1; row <= 9; row++)
                {
                    layout[$"{row}A"] = GetSeatMarker($"{row}A");
                    layout[$"{row}B"] = GetSeatMarker($"{row}B");
                    layout[$"{row}C"] = GetSeatMarker($"{row}C");
                }
            }
            else
            {
                // Economy: 9 rows, 4 seats per row (A, B, C, D)
                for (int row = 1; row <= 9; row++)
                {
                    layout[$"{row}A"] = GetSeatMarker($"{row}A");
                    layout[$"{row}B"] = GetSeatMarker($"{row}B");
                    layout[$"{row}C"] = GetSeatMarker($"{row}C");
                    layout[$"{row}D"] = GetSeatMarker($"{row}D");
                }
            }
            
            return layout;
        }

        private string GetSeatMarker(string seatNumber)
        {
            if (ReservedSeats.ContainsKey(seatNumber) && ReservedSeats[seatNumber].IsPaid)
            {
                return "X";
            }
            return " ";
        }

        public override string ToString()
        {
            return $"Bus ID: {BusId} | {DepartureCity} -> {ArrivalCity} | Date: {DepartureDateTime:yyyy-MM-dd} | Time: {DepartureDateTime:HH:mm} | Taka: {TicketPrice}";
        }
    }
}