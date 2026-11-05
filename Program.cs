using System;
using SalonBusinessLogic;

namespace SalonAppointmentManagementSystem
{
    internal class Program
    {

        static void Main(string[] args)
        {

            AuthenticationLogic authLogic = new AuthenticationLogic();
            AppointmentLogic appointmentLogic = new AppointmentLogic();

            Console.WriteLine("\nWelcome to Salon Appointment Management System");

            Console.WriteLine("\n===== MAIN MENU =====");
            Console.WriteLine("Enter 1 - for Admin Login");
            Console.WriteLine("Enter 2 - for Customer");
            Console.WriteLine("Enter x - to Exit the Application");

            Console.Write("\nEnter your choice: ");
            string? checkLogin = Console.ReadLine();

            while (checkLogin != null && checkLogin != "x")
            {
                if (checkLogin == "1")
                {
                    Console.Write("\nEnter Admin Username: ");
                    string userInput = Console.ReadLine() ?? "";

                    Console.Write("Enter Admin Password: ");
                    string inputPass = Console.ReadLine() ?? "";

                    if (authLogic.IsAdminLoginCorrect(userInput, inputPass))
                    {
                        Console.WriteLine("\nWelcome Admin!");
                        int adminChoice = -1;

                        while (adminChoice != 0)
                        {
                            Console.WriteLine("\n===== ADMIN MENU =====");
                            Console.WriteLine("Enter 1 - to view current appointments");
                            Console.WriteLine("Enter 2 - to clear one appointment");
                            Console.WriteLine("Enter 3 - to clear all appointments");
                            Console.WriteLine("Enter 4 - to review cancellation requests");
                            Console.WriteLine("Enter 0 - to log out");


                            Console.WriteLine();
                            if (!ReadNumber("Enter your choice: ", number => number >= 0 && number <= 4,
                                "Invalid choice. Enter a number from 0 to 4.", out adminChoice))
                            {
                                return;
                            }


                            switch (adminChoice)
                            {
                                case 1:
                                    Console.WriteLine("\n===== CURRENT APPOINTMENTS =====");
                                    if (appointmentLogic.GetAppointmentCount() == 0)
                                    {
                                        Console.WriteLine("No appointments have been booked yet.");
                                    }
                                    else
                                    {
                                        foreach (int i in appointmentLogic.GetAppointmentIndicesInDateOrder())
                                        {
                                            DisplayAppointment(appointmentLogic, i);
                                        }
                                    }
                                    break;

                                case 2:
                                    Console.WriteLine("\n===== CLEAR ONE APPOINTMENT =====");
                                    if (appointmentLogic.GetAppointmentCount() == 0)
                                    {
                                        Console.WriteLine("No appointments available to clear.");
                                    }
                                    else
                                    {
                                        foreach (int i in appointmentLogic.GetAppointmentIndicesInDateOrder())
                                        {
                                            DisplayAppointment(appointmentLogic, i);
                                        }

                                        if (!ReadNumber("Enter the appointment number to clear: ",
                                            appointmentLogic.IsValidAppointmentNumber,
                                            "Invalid appointment number. Enter a number from the list.", out int clearNumber))
                                        {
                                            return;
                                        }

                                        Console.Write($"Are you sure you want to clear the appointment of {appointmentLogic.GetCustomerName(clearNumber - 1)}? (y/n): ");
                                        string? clearChoice = Console.ReadLine()?.ToLowerInvariant();

                                        if (clearChoice == "y")
                                        {
                                            appointmentLogic.ClearAppointment(clearNumber);
                                            Console.WriteLine("The appointment has been cleared.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Appointment clearing cancelled.");
                                        }
                                    }
                                    break;

                                case 3:
                                    if (appointmentLogic.GetAppointmentCount() == 0)
                                    {
                                        Console.WriteLine("No appointments available to clear.");
                                    }
                                    else
                                    {
                                        string? clearAllChoice;
                                        while (true)
                                        {
                                            Console.Write("Are you sure you want to clear all appointments? (y/n): ");
                                            clearAllChoice = Console.ReadLine()?.ToLowerInvariant();
                                            if (clearAllChoice == null || clearAllChoice == "y" || clearAllChoice == "n")
                                            {
                                                break;
                                            }

                                            Console.WriteLine("Please enter y or n.");
                                        }

                                        if (clearAllChoice == "y")
                                        {
                                            appointmentLogic.ClearAllAppointments();
                                            Console.WriteLine("All appointments have been cleared.");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Appointment clearing cancelled.");
                                        }
                                    }
                                    break;
                                case 4:
                                    Console.WriteLine("\n===== CANCELLATION REQUESTS =====");
                                    bool hasRequests = false;
                                    foreach (int i in appointmentLogic.GetAppointmentIndicesInDateOrder())
                                    {
                                        if (appointmentLogic.IsCancellationRequested(i))
                                        {
                                            hasRequests = true;
                                            Console.WriteLine($"Appointment #{i + 1}: {appointmentLogic.GetCustomerName(i)} - {appointmentLogic.GetDate(i)} at {appointmentLogic.GetTime(i)}");
                                        }
                                    }

                                    if (!hasRequests)
                                    {
                                        Console.WriteLine("No pending cancellation requests.");
                                        break;
                                    }

                                    Console.WriteLine();
                                    if (!ReadNumber("Enter the appointment number to review (0 to go back): ",
                                        number => number == 0 || (appointmentLogic.IsValidAppointmentNumber(number)
                                            && appointmentLogic.IsCancellationRequested(number - 1)),
                                        "Enter a pending appointment number or 0.", out int requestNumber))
                                    {
                                        return;
                                    }

                                    if (requestNumber != 0 && ReadConfirmation("Approve this booking cancellation? (Y/N): "))
                                    {
                                        appointmentLogic.ApproveCancellation(requestNumber - 1);
                                        Console.WriteLine("Cancellation approved.");
                                    }
                                    break;
                                case 0:
                                    Console.WriteLine("You have been logged out.");
                                    break;
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Information.");
                    }
                }

                else if (checkLogin == "2")
                {
                    Console.Write("\nEnter your full name: ");
                    string inputName = Console.ReadLine()?.Trim() ?? "";
                    bool customerMenuOpen = authLogic.IsCustomerNameValid(inputName);

                    if (customerMenuOpen)
                    {
                        Console.WriteLine($"\nWelcome {inputName}!");
                    }
                    else
                    {
                        Console.WriteLine("Name cannot be empty.");
                    }

                    while (customerMenuOpen)
                    {
                        Console.WriteLine("\n===== CUSTOMER MENU =====");
                        Console.WriteLine("Type 1 - to book an appointment");
                        Console.WriteLine("Type 2 - to view appointment details");
                        Console.WriteLine("Type 3 - to request a booking cancellation");
                        Console.WriteLine("Type 4 - to return to the main menu");

                        Console.WriteLine();
                        if (!ReadNumber("Enter your choice: ", number => number >= 1 && number <= 4,
                            "Invalid choice. Enter a number from 1 to 4.", out int customerChoice))
                        {
                            return;
                        }

                        switch (customerChoice)
                        {
                            case 1:
                                appointmentLogic.ClearChosenServices();
                                string addMore = "y";

                                while (addMore == "y")
                                {
                                    Console.WriteLine("\nSelect Service:");
                                    Console.WriteLine("1 - Hair Service");
                                    Console.WriteLine("2 - Nail Service");
                                    if (!ReadNumber("Enter your choice: ", number => number == 1 || number == 2,
                                        "Invalid category. Enter 1 for hair or 2 for nails.", out int categoryChoice))
                                    {
                                        return;
                                    }

                                    Console.WriteLine("\nList of Salon Offerings");
                                    for (int i = appointmentLogic.GetFirstServiceNumber(categoryChoice); i <= appointmentLogic.GetLastServiceNumber(categoryChoice); i++)
                                    {
                                        Console.WriteLine($"{i} - {appointmentLogic.GetServiceName(i)} (PHP {appointmentLogic.GetServicePrice(i):N0})");
                                    }

                                    Console.WriteLine();
                                    if (!ReadNumber("Enter the service number: ",
                                        number => appointmentLogic.IsServiceInCategory(categoryChoice, number),
                                        "Invalid service number. Choose a number from this category.", out int serviceChoice))
                                    {
                                        return;
                                    }

                                    if (!appointmentLogic.AddChosenService(serviceChoice))
                                    {
                                        Console.WriteLine("You already added that service.");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Added: {appointmentLogic.GetServiceName(serviceChoice)}");
                                    }

                                    Console.Write("\nAdd another service? (y/n): ");
                                    addMore = Console.ReadLine()?.ToLowerInvariant() ?? "";
                                }

                                if (appointmentLogic.GetChosenServiceCount() == 0)
                                {
                                    Console.WriteLine("No service selected. Booking cancelled.");
                                    break;
                                }

                                string inputDate;
                                while (true)
                                {
                                    Console.Write("\nEnter appointment date (MM/DD/YYYY): ");
                                    string? date = Console.ReadLine();
                                    if (date == null)
                                    {
                                        return;
                                    }

                                    string dateMessage = appointmentLogic.CheckDate(date);
                                    if (dateMessage != "OK")
                                    {
                                        Console.WriteLine(dateMessage);
                                        continue;
                                    }

                                    if (appointmentLogic.IsDateFull(date))
                                    {
                                        Console.WriteLine("That date is fully booked. Please choose another date.");
                                        continue;
                                    }

                                    inputDate = date;
                                    break;
                                }

                                Console.WriteLine("\n===== TIME SLOTS =====");
                                for (int i = 1; i <= appointmentLogic.GetTimeSlotCount(); i++)
                                {
                                    int availableSpots = appointmentLogic.GetAvailableSpots(inputDate, i);
                                    if (availableSpots == 0)
                                    {
                                        Console.WriteLine($"{i} - {appointmentLogic.GetTimeSlot(i)} (Full)");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{i} - {appointmentLogic.GetTimeSlot(i)} ({availableSpots} of {AppointmentLogic.BookingsPerTimeSlot} available)");
                                    }
                                }
                                Console.WriteLine();
                                int timeChoice;
                                while (true)
                                {
                                    if (!ReadNumber("Enter the time slot number: ", appointmentLogic.IsValidTimeChoice,
                                        "Invalid time slot. Choose a number from the list.", out timeChoice))
                                    {
                                        return;
                                    }

                                    if (appointmentLogic.IsSlotTaken(inputDate, timeChoice))
                                    {
                                        Console.WriteLine("That time slot is full. Please choose another time.");
                                        continue;
                                    }

                                    break;
                                }

                                Console.WriteLine("\n===== BOOKING DETAILS =====");
                                Console.WriteLine($"Customer Name: {inputName}");
                                Console.WriteLine($"Date: {inputDate}");
                                Console.WriteLine($"Time: {appointmentLogic.GetTimeSlot(timeChoice)}");
                                Console.WriteLine("Services:");

                                for (int i = 0; i < appointmentLogic.GetChosenServiceCount(); i++)
                                {
                                    int service = appointmentLogic.GetChosenService(i);
                                    Console.WriteLine($"  - {appointmentLogic.GetServiceName(service)} (PHP {appointmentLogic.GetServicePrice(service):N0})");                                 
                                }
                                
                                Console.WriteLine($"Total: PHP {appointmentLogic.GetChosenServicesTotal():N0}");

                                Console.Write("\nConfirm booking? (y/n): ");
                                string? confirmBooking = Console.ReadLine()?.ToLowerInvariant();

                                if (confirmBooking == "y")
                                {
                                    appointmentLogic.BookChosenServices(inputName, inputDate, timeChoice);
                                    Console.WriteLine("Appointment successfully booked!");
                                }
                                else
                                {
                                    appointmentLogic.ClearChosenServices();
                                    Console.WriteLine("Booking cancelled.");
                                }
                                break;

                            case 2:
                                Console.WriteLine("\n===== YOUR APPOINTMENTS =====");
                                if (appointmentLogic.CountAppointmentsOf(inputName) == 0)
                                {
                                    Console.WriteLine("You don't have any appointments yet.");
                                }
                                else
                                {
                                    foreach (int i in appointmentLogic.GetAppointmentIndicesInDateOrder())
                                    {
                                        if (appointmentLogic.IsOwnedBy(i, inputName))
                                        {
                                            DisplayAppointment(appointmentLogic, i);                                      
                                        }
                                    }
                                    Console.WriteLine($"Total of all your appointments: PHP {appointmentLogic.GetTotalOf(inputName):N0}");
                                }
                                break;

                            case 3:
                                Console.WriteLine("\n===== REQUEST CANCELLATION =====");
                                if (appointmentLogic.CountAppointmentsOf(inputName) == 0)
                                {
                                    Console.WriteLine("You don't have any bookings to cancel.");
                                    break;
                                }

                                foreach (int i in appointmentLogic.GetAppointmentIndicesInDateOrder())
                                {
                                    if (appointmentLogic.IsOwnedBy(i, inputName) && appointmentLogic.IsFirstBookingRecord(i))
                                    {
                                        Console.WriteLine($"Booking #{i + 1}: {appointmentLogic.GetDate(i)} at {appointmentLogic.GetTime(i)}");
                                    }
                                }

                                Console.WriteLine();
                                if (!ReadNumber("Enter the booking number (0 to go back): ",
                                    number => number == 0 || (appointmentLogic.IsValidAppointmentNumber(number)
                                        && appointmentLogic.IsOwnedBy(number - 1, inputName)
                                        && appointmentLogic.IsFirstBookingRecord(number - 1)),
                                    "Enter a booking number from your list or 0.", out int bookingNumber))
                                {
                                    return;
                                }
                                if (bookingNumber == 0)
                                {
                                    break;
                                }

                                int appointmentIndex = bookingNumber - 1;
                                if (appointmentLogic.IsCancellationRequested(appointmentIndex))
                                {
                                    Console.WriteLine("This cancellation request is already waiting for admin approval.");
                                    break;
                                }

                                if (!appointmentLogic.CanRequestCancellation(appointmentIndex))
                                {
                                    Console.WriteLine("Request cancellation at least 2 calendar days before your booking.");
                                    break;
                                }

                                Console.WriteLine($"\nBooking on {appointmentLogic.GetDate(appointmentIndex)} at {appointmentLogic.GetTime(appointmentIndex)}.");
                                bool confirmed = ReadConfirmation("Are you sure you want to cancel your booking? (Y/N): ");
                                Console.WriteLine();
                                if (confirmed)
                                {
                                    if (appointmentLogic.RequestCancellation(appointmentIndex, inputName))
                                    {
                                        Console.WriteLine("Cancellation requested. Your booking remains active until an admin approves it.");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Request cancellation at least 2 calendar days before your booking.");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Booking kept.");
                                }
                                break;

                            case 4:
                                customerMenuOpen = false;
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2, or x.");
                }

                Console.WriteLine("\n===== MAIN MENU =====");
                Console.WriteLine("Enter 1 - for Admin Login");
                Console.WriteLine("Enter 2 - for Customer");
                Console.WriteLine("Enter x - to exit");
                Console.Write("\nEnter your choice: ");
                checkLogin = Console.ReadLine();
            }

            Console.WriteLine("Program ended. Goodbye!");
        }

        static bool ReadConfirmation(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string? answer = Console.ReadLine()?.Trim().ToUpperInvariant();
                if (answer == "Y")
                {
                    return true;
                }
                if (answer == "N" || answer == null)
                {
                    return false;
                }
                Console.WriteLine("Please enter Y or N.");
            }
        }

        static bool ReadNumber(string prompt, Func<int, bool> isValid, string errorMessage, out int number)
        {
            while (true)
            {
                Console.Write(prompt);
                string? input = Console.ReadLine();
                if (int.TryParse(input, out number) && isValid(number))
                {
                    return true;
                }

                if (input == null)
                {
                    return false;
                }

                Console.WriteLine(errorMessage);
            }
        }

        static void DisplayAppointment(AppointmentLogic appointmentLogic, int index)
        {
            Console.WriteLine($"Appointment #{index + 1}");
            Console.WriteLine($"  Customer Name: {appointmentLogic.GetCustomerName(index)}");
            Console.WriteLine($"  Service Booked: {appointmentLogic.GetService(index)}");
            Console.WriteLine($"  Price: PHP {appointmentLogic.GetPrice(index):N0}");
            Console.WriteLine($"  Date: {appointmentLogic.GetDate(index)}");
            Console.WriteLine($"  Time: {appointmentLogic.GetTime(index)}");
            Console.WriteLine();
        }
    }
}
