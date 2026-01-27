using System;
using BusTicketBookingSystem.Enums;

namespace BusTicketBookingSystem.Models
{
    public class Invoice
    {
        public int InvoiceId { get; private set; }
        public int TicketId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public DateTime InvoiceDate { get; private set; }
        public PaymentStatus PaymentStatus { get; set; }

        public Invoice(int invoiceId, int ticketId, int userId, decimal amount)
        {
            InvoiceId = invoiceId;
            TicketId = ticketId;
            UserId = userId;
            Amount = amount;
            InvoiceDate = DateTime.Now;
            PaymentStatus = PaymentStatus.Unpaid;
        }

        public void MarkAsPaid()
        {
            PaymentStatus = PaymentStatus.Paid;
        }

        public override string ToString()
        {
            string status = PaymentStatus == PaymentStatus.Paid ? "Yes" : "No";
            return $"Invoice ID: {InvoiceId} | Ticket ID: {TicketId} | Amount: {Amount} | Date: {InvoiceDate:yyyy-MM-dd} | Paid: {status}";
        }
    }
}