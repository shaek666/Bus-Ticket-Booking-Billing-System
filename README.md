**Assignment 1**  
Total Marks: 50  
---

## **Problem Statement**

Design and implement a **Bus Ticket Booking & Billing System** for a transportation company. The system must allow users to book bus tickets, select seats, receive invoices, and make payments. The project must be implemented using **Object-Oriented Programming (OOP)** principles and must demonstrate proper use of **SOLID**, **KISS**, and **DRY** principles.

---

## **System Requirements**

### **1\. User Management**

* The system must allow creation of users.  
* Each user should have:  
  * A unique ID  
  * Name  
  * Mobile number  
  * Email address  
* A user can book multiple tickets.

---

### **2\. Bus Management**

* The system must support multiple buses.  
* Each bus must have:  
  * Unique ID  
  * Coach number  
  * Bus type (e.g., Business, Economy)  
  * Total number of seats based on bus type  
* The system must track booked and available seats.

---

### **3\. Schedule Management**

* Each bus can have multiple schedules.  
* A schedule must include:  
  * Departure city  
  * Arrival city  
  * Departure date and time  
  * Ticket price  
* Each schedule must be linked to a bus.

---

### **4\. Ticket Booking**

* A user must be able to:  
  * Select a schedule  
  * Choose a seat  
  * Book a ticket  
* The system must:  
  * Validate seat numbers based on bus type  
  * Prevent double booking of seats

* On successful booking (after payment):  
  * A ticket must be generated  
  * The seat must be marked as booked

---

### **5\. Invoice & Payment**

* Every ticket booking must generate an invoice.  
* An invoice must include  
  * Invoice ID  
  * Ticket ID  
  * User ID  
  * Amount  
  * Invoice date  
  * Payment status (Paid / Unpaid)

* Users must be able to:  
  * View their invoices  
  * Pay unpaid invoices

---

### **6\. Viewing Information**

The system must allow:

* Create User  
* Show Users  
* Create Bus  
* Show Buses  
* Create Schedule  
* Show Schedules  
* Show Schedule Details  
* Book Ticket  
* Show Invoices of a user  
* Pay Invoice  
* Show Tickets of a User
