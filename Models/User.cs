using System;

namespace BusTicketBookingSystem.Models
{
    public class User
    {
        public int UserId { get; private set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }

        public User(int userId, string name, string mobileNumber, string email)
        {
            UserId = userId;
            Name = name;
            MobileNumber = mobileNumber;
            Email = email;
        }

        public override string ToString()
        {
            return $"{Name} | {MobileNumber} | {Email}";
        }
    }
}