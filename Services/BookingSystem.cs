// Services/BookingSystem.cs
using System;
using System.Collections.Generic;
using System.Linq;
using BusTicketBookingSystem.Models;
using BusTicketBookingSystem.Enums;

namespace BusTicketBookingSystem.Services
{
    public class BookingSystem
    {
        private List<User> users;
        private List<Bus> buses;
        private List<Schedule> schedules;
        private List<Ticket> tickets;
        private List<Invoice> invoices;
        private int userCounter;
        private int busCounter;
        private int scheduleCounter;
        private int ticketCounter;
        private int invoiceCounter;

        public BookingSystem()
        {
            users = new List<User>();
            buses = new List<Bus>();
            schedules = new List<Schedule>();
            tickets = new List<Ticket>();
            invoices = new List<Invoice>();
            userCounter = 1;
            busCounter = 1;
            scheduleCounter = 1;
            ticketCounter = 1;
            invoiceCounter = 1;
        }

        // User Management
        public bool IsMobileNumberExists(string mobileNumber)
        {
            return users.Any(u => u.MobileNumber == mobileNumber);
        }

        public bool IsEmailExists(string email)
        {
            return users.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public void CreateUser(string name, string mobileNumber, string email)
        {
            User user = new User(userCounter++, name, mobileNumber, email);
            users.Add(user);
            Console.WriteLine($"\nUser Created Successfully With ID: {user.UserId}");
        }

        public void ShowUsers()
        {
            Console.WriteLine("\n--- Users ---");
            if (users.Count == 0)
            {
                Console.WriteLine("No Users Found.");
                return;
            }
            foreach (var user in users)
            {
                Console.WriteLine($"{user.UserId}. {user}");
            }
        }

        // Bus Management
        public void CreateBus(string coachNumber, BusType busType)
        {
            Bus bus = new Bus(busCounter++, coachNumber, busType);
            buses.Add(bus);
            Console.WriteLine($"\nBus Created Successfully With ID: {bus.BusId}");
        }

        public void ShowBuses()
        {
            Console.WriteLine("\n--- Buses ---");
            if (buses.Count == 0)
            {
                Console.WriteLine("No Buses Found.");
                return;
            }
            foreach (var bus in buses)
            {
                Console.WriteLine($"{bus.BusId}. {bus}");
            }
        }

        // Schedule Management
        public void CreateSchedule(int busId, string departureCity, string arrivalCity, 
                                   DateTime departureDateTime, decimal ticketPrice)
        {
            Bus? bus = buses.FirstOrDefault(b => b.BusId == busId);
            if (bus == null)
            {
                throw new Exception("Bus Not Found.");
            }

            if (departureCity.Trim().Equals(arrivalCity.Trim(), StringComparison.OrdinalIgnoreCase))
            {
                throw new Exception("Departure City And Arrival City Cannot Be The Same.");
            }

            if (ticketPrice < 0)
            {
                throw new Exception("Ticket Price Cannot Be Negative.");
            }

            if (ticketPrice > 9999.99m)
            {
                throw new Exception("Ticket Price Cannot Exceed 9999.99.");
            }

            Schedule schedule = new Schedule(scheduleCounter++, busId, departureCity, arrivalCity, 
                                            departureDateTime, ticketPrice);
            schedules.Add(schedule);
            Console.WriteLine($"\nSchedule Created Successfully With ID: {schedule.ScheduleId}");
        }

        public void ShowSchedules()
        {
            Console.WriteLine("\n--- Schedules ---");
            if (schedules.Count == 0)
            {
                Console.WriteLine("No Schedules Found.");
                return;
            }
            foreach (var schedule in schedules)
            {
                Console.WriteLine($"{schedule.ScheduleId}. {schedule}");
            }
        }

        public void ShowScheduleDetails(int scheduleId)
        {
            Schedule? schedule = schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
            {
                Console.WriteLine("\nError: Schedule Not Found.");
                return;
            }

            Bus? bus = buses.FirstOrDefault(b => b.BusId == schedule.BusId);
            if (bus == null)
            {
                Console.WriteLine("\nError: Bus Not Found.");
                return;
            }

            Console.WriteLine("\n--- Schedule Details ---");
            Console.WriteLine($"Schedule ID: {schedule.ScheduleId}");
            Console.WriteLine($"Bus ID: {bus.BusId} | Coach Number: {bus.CoachNumber} | Type: {bus.BusType}");
            Console.WriteLine($"From: {schedule.DepartureCity} To: {schedule.ArrivalCity}");
            Console.WriteLine($"Departure Time: {schedule.DepartureDateTime:HH:mm} | Taka: {schedule.TicketPrice}");
            Console.WriteLine($"Total Seats: {bus.TotalSeats}");
            Console.WriteLine();
            
            var seatLayout = schedule.GetSeatLayout(bus);
            Console.WriteLine("Seat Layout (X = Booked, [ ] = Available):");
            
            if (bus.BusType == BusType.Business)
            {
                // Business: Left column (A), Right column (B, C)
                for (int row = 1; row <= 9; row++)
                {
                    string leftSeat = $"[{seatLayout[$"{row}A"]}:{row}A]";
                    string rightSeats = $"[{seatLayout[$"{row}B"]}:{row}B]" + 
                                       $"[{seatLayout[$"{row}C"]}:{row}C]";
                    
                    Console.WriteLine($"{leftSeat.PadRight(14)}{rightSeats} Row {row}");
                }
            }
            else
            {
                // Economy: Left column (A, B), Right column (C, D)
                for (int row = 1; row <= 9; row++)
                {
                    string leftSeats = $"[{seatLayout[$"{row}A"]}:{row}A]" + 
                                      $"[{seatLayout[$"{row}B"]}:{row}B]";
                    string rightSeats = $"[{seatLayout[$"{row}C"]}:{row}C]" + 
                                       $"[{seatLayout[$"{row}D"]}:{row}D]";
                    
                    Console.WriteLine($"{leftSeats.PadRight(22)}{rightSeats} Row {row}");
                }
            }
        }

        // Ticket Booking
        public void BookTicket(int userId, int scheduleId, string seatNumber)
        {
            User? user = users.FirstOrDefault(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("User Not Found.");
            }

            Schedule? schedule = schedules.FirstOrDefault(s => s.ScheduleId == scheduleId);
            if (schedule == null)
            {
                throw new Exception("Schedule Not Found.");
            }

            Bus? bus = buses.FirstOrDefault(b => b.BusId == schedule.BusId);
            if (bus == null)
            {
                throw new Exception("Bus Not Found.");
            }

            // Validate seat format
            if (!IsValidSeatNumber(seatNumber, bus))
            {
                string maxSeat = bus.BusType == BusType.Business ? "9C" : "9D";
                throw new Exception($"Invalid Seat Number. Valid Range: 1A-{maxSeat}");
            }

            if (!schedule.IsSeatAvailable(seatNumber))
            {
                throw new Exception($"Seat {seatNumber} Is Already Booked. Please Choose Another Seat!");
            }

            schedule.ReserveSeat(seatNumber);

            Ticket ticket = new Ticket(ticketCounter++, userId, scheduleId, seatNumber, bus.CoachNumber, schedule.DepartureDateTime);
            tickets.Add(ticket);

            Invoice invoice = new Invoice(invoiceCounter++, ticket.TicketId, userId, schedule.TicketPrice);
            invoices.Add(invoice);

            Console.WriteLine($"\nTicket Booked Successfully With ID: {ticket.TicketId}");
            Console.WriteLine($"Invoice Generated With ID: {invoice.InvoiceId}");
            Console.WriteLine("Note: Please Pay Within 10 Minutes To Confirm Your Booking.");
        }

        private bool IsValidSeatNumber(string seatNumber, Bus bus)
        {
            if (string.IsNullOrEmpty(seatNumber) || seatNumber.Length < 2)
                return false;

            string rowPart = seatNumber.Substring(0, seatNumber.Length - 1);
            char seatLetter = seatNumber[seatNumber.Length - 1];

            if (!int.TryParse(rowPart, out int row))
                return false;

            if (row < 1 || row > 9)
                return false;

            if (bus.BusType == BusType.Business)
            {
                return seatLetter >= 'A' && seatLetter <= 'C';
            }
            else
            {
                return seatLetter >= 'A' && seatLetter <= 'D';
            }
        }

        // Invoice Management
        public void ShowInvoicesOfUser(int userId)
        {
            Console.WriteLine("\n--- Invoices ---");
            var userInvoices = invoices.Where(i => i.UserId == userId).ToList();
            if (userInvoices.Count == 0)
            {
                Console.WriteLine("No Invoices Found For This User.");
                return;
            }
            foreach (var invoice in userInvoices)
            {
                Console.WriteLine(invoice);
            }
        }

        public void PayInvoice(int invoiceId)
        {
            Invoice? invoice = invoices.FirstOrDefault(i => i.InvoiceId == invoiceId);
            if (invoice == null)
            {
                throw new Exception("Invoice Not Found.");
            }

            if (invoice.PaymentStatus == PaymentStatus.Paid)
            {
                throw new Exception("Invoice Already Paid.");
            }

            invoice.MarkAsPaid();

            Ticket? ticket = tickets.FirstOrDefault(t => t.TicketId == invoice.TicketId);
            if (ticket != null)
            {
                Schedule? schedule = schedules.FirstOrDefault(s => s.ScheduleId == ticket.ScheduleId);
                if (schedule != null)
                {
                    schedule.ConfirmSeat(ticket.SeatNumber);
                }
            }

            Console.WriteLine($"\nPayment Successful For Invoice ID: {invoiceId}");
            Console.WriteLine("Booking Confirmed!");
        }

        // Ticket Management
        public void ShowTicketsOfUser(int userId)
        {
            Console.WriteLine("\n--- Tickets ---");
            var userTickets = tickets.Where(t => t.UserId == userId).ToList();
            if (userTickets.Count == 0)
            {
                Console.WriteLine("No Paid Tickets Found For This User.");
                return;
            }
            
            bool hasPaidTickets = false;
            foreach (var ticket in userTickets)
            {
                Invoice? invoice = invoices.FirstOrDefault(i => i.TicketId == ticket.TicketId);
                if (invoice != null && invoice.PaymentStatus == PaymentStatus.Paid)
                {
                    Console.WriteLine(ticket);
                    hasPaidTickets = true;
                }
            }
            
            if (!hasPaidTickets)
            {
                Console.WriteLine("No Paid Tickets Found For This User.");
            }
        }
    }
}