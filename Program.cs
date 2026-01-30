using System;
using BusTicketBookingSystem.Services;
using BusTicketBookingSystem.Enums;
using BusTicketBookingSystem.Utils;

namespace BusTicketBookingSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            BookingSystem system = new BookingSystem();
            bool running = true;

            while (running)
            {
                DisplayMenu();
                string? choice = Console.ReadLine();

                try
                {
                    running = ProcessChoice(system, choice);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
        }

        static void DisplayMenu()
        {
            Console.WriteLine("\n--- Bus Ticket Booking System ---");
            Console.WriteLine("1.  Create User");
            Console.WriteLine("2.  Show Users");
            Console.WriteLine("3.  Create Bus");
            Console.WriteLine("4.  Show Buses");
            Console.WriteLine("5.  Create Schedule");
            Console.WriteLine("6.  Show Schedules");
            Console.WriteLine("7.  Show Schedule Details");
            Console.WriteLine("8.  Book Ticket");
            Console.WriteLine("9.  Show Invoices Of A User");
            Console.WriteLine("10. Pay Invoice");
            Console.WriteLine("11. Show Tickets Of A User");
            Console.WriteLine("12. Exit");
            Console.Write("\nEnter Your Choice: ");
        }

        static bool ProcessChoice(BookingSystem system, string? choice)
        {
            switch (choice)
            {
                case "1":
                    CreateUser(system);
                    break;
                case "2":
                    system.ShowUsers();
                    break;
                case "3":
                    CreateBus(system);
                    break;
                case "4":
                    system.ShowBuses();
                    break;
                case "5":
                    CreateSchedule(system);
                    break;
                case "6":
                    system.ShowSchedules();
                    break;
                case "7":
                    ShowScheduleDetails(system);
                    break;
                case "8":
                    BookTicket(system);
                    break;
                case "9":
                    ShowInvoices(system);
                    break;
                case "10":
                    PayInvoice(system);
                    break;
                case "11":
                    ShowTickets(system);
                    break;
                case "12":
                    Console.WriteLine("\nThank You For Using Bus Ticket Booking System!");
                    return false;
                default:
                    Console.WriteLine("\nInvalid Choice. Please Try Again.");
                    break;
            }
            return true;
        }

        static void CreateUser(BookingSystem system)
        {
            Console.Write("Enter Name: ");
            string? name = Console.ReadLine();
            ValidationHelper.ValidateName(name);
            
            Console.Write("Enter Mobile Number: ");
            string? mobile = Console.ReadLine();
            string fullMobile = ValidationHelper.ValidateAndFormatMobileNumber(mobile!);
            
            if (system.IsMobileNumberExists(fullMobile))
                throw new Exception("User With This Mobile Number Already Exists.");
            
            Console.Write("Enter Email: ");
            string? email = Console.ReadLine();
            ValidationHelper.ValidateEmail(email!);
            
            if (system.IsEmailExists(email!))
                throw new Exception("User With This Email Already Exists.");
            
            system.CreateUser(name!, fullMobile, email!);
        }

        static void CreateBus(BookingSystem system)
        {
            Console.Write("Enter Coach Number: ");
            string? coachNumber = Console.ReadLine();
            ValidationHelper.ValidateNotEmpty(coachNumber, "Coach Number");
            
            Console.Write("Enter Bus Type (0=Business, 1=Economy): ");
            int busTypeInt = ValidationHelper.ValidateBusTypeInput(Console.ReadLine());
            
            BusType busType = (BusType)busTypeInt;
            system.CreateBus(coachNumber!, busType);
        }

        static void CreateSchedule(BookingSystem system)
        {
            Console.Write("Enter Bus ID: ");
            int busId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "Bus ID");
            
            Console.Write("Enter Departure City: ");
            string? depCity = Console.ReadLine();
            ValidationHelper.ValidateCityName(depCity!, "Departure City");
            
            Console.Write("Enter Arrival City: ");
            string? arrCity = Console.ReadLine();
            ValidationHelper.ValidateCityName(arrCity!, "Arrival City");
            
            Console.Write("Enter Departure Date And Time (yyyy-MM-dd HH:mm): ");
            string? dateTimeInput = Console.ReadLine();
            DateTime depDateTime = ValidationHelper.ValidateDateTime(dateTimeInput!);
            
            Console.Write("Enter Ticket Price: ");
            decimal price = ValidationHelper.ValidateDecimal(Console.ReadLine(), "Ticket Price");
            ValidationHelper.ValidateTicketPrice(price);
            
            system.CreateSchedule(busId, depCity!, arrCity!, depDateTime, price);
        }

        static void ShowScheduleDetails(BookingSystem system)
        {
            Console.Write("Enter Schedule ID To View Details: ");
            int scheduleId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "Schedule ID");
            system.ShowScheduleDetails(scheduleId);
        }

        static void BookTicket(BookingSystem system)
        {
            Console.Write("Enter User ID: ");
            int userId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "User ID");
            
            Console.Write("Enter Schedule ID: ");
            int schedId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "Schedule ID");
            
            Console.Write("Enter Seat Number (e.g., 1A, 3C): ");
            string? seatNumber = Console.ReadLine();
            ValidationHelper.ValidateNotEmpty(seatNumber, "Seat Number");
            
            system.BookTicket(userId, schedId, seatNumber!.ToUpper());
        }

        static void ShowInvoices(BookingSystem system)
        {
            Console.Write("Enter User ID: ");
            int userId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "User ID");
            system.ShowInvoicesOfUser(userId);
        }

        static void PayInvoice(BookingSystem system)
        {
            Console.Write("Enter Invoice ID: ");
            int invoiceId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "Invoice ID");
            system.PayInvoice(invoiceId);
        }

        static void ShowTickets(BookingSystem system)
        {
            Console.Write("Enter User ID: ");
            int userId = ValidationHelper.ValidateIntegerId(Console.ReadLine(), "User ID");
            system.ShowTicketsOfUser(userId);
        }
    }
}
