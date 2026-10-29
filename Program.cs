using System;
using SalonBusinessLogic;
using System.Collections.Generic;

namespace SalonAppointmentManagementSystem
{
    internal class Program
    {

        static void Main(string[] args)
        {

            AuthenticationLogic authLogic = new AuthenticationLogic();
            AppointmentLogic appointmentLogic = new AppointmentLogic();

            appointmentLogic.PopulateDummyAppointments();

            Console.WriteLine("\nWelcome to Salon Appointment Management System");

            Console.WriteLine("Enter 1 - for Admin Login");
            Console.WriteLine("Enter 2 - for Customer Login");
            Console.WriteLine("Enter x - to Exit the Application");

            Console.Write("User Input: ");
            string checkLogin = Console.ReadLine();

            while (checkLogin != "x")
            {
                if (checkLogin == "1")
                {
                    Console.Write("\nEnter Admin Username: ");
                    string userInput = Console.ReadLine();

                    Console.Write("Enter Admin Password: ");
                    string inputPass = Console.ReadLine();

                    if (authLogic.IsAdminLoginCorrect(userInput, inputPass))
                    {
                        Console.WriteLine("\nWelcome Admin!");
                        int adminChoice = -1;

                        while (adminChoice != 0)
                        {
                            Console.WriteLine("Enter 1 - to view current Appointment");
                            Console.WriteLine("Enter 2 - to clear one Appointment");
                            Console.WriteLine("Enter 3 - to clear Appointment");
                            Console.WriteLine("Enter 0 - to Logout");


                            Console.Write("Enter your choice: ");
                            adminChoice = Convert.ToInt16(Console.ReadLine());


                            switch (adminChoice)
                            {
                                case 1:
                                    if (appointmentLogic.GetAppointmentCount() == 0)
                                    {
                                        Console.WriteLine("No Appointment has been booked yet!");
                                    }
                                    else
                                    {
                                        for (int i = 0; i < appointmentLogic.GetAppointmentCount(); i++)
                                        {
                                            DisplayAppointment(appointmentLogic, i);
                                        }
                                    }
                                    break;

                                case 2:
                                    if (appointmentLogic.GetAppointmentCount() == 0)
                                    {
                                        Console.WriteLine("No Appointment to Clear.");
                                    }
                                    else
                                    {
                                        for (int i = 0; i < appointmentLogic.GetAppointmentCount(); i++)
                                        {
                                            DisplayAppointment(appointmentLogic, i);
                                        }

                                        Console.Write("Enter the Appointment number to clear: ");
                                        int clearNumber = Convert.ToInt16(Console.ReadLine());

                                        if (!appointmentLogic.IsValidAppointmentNumber(clearNumber))
                                        {
                                            Console.WriteLine("Invalid Appointment number.");
                                        }
                                        else
                                        {
                                            Console.Write($"Are you sure you want to clear the appointment of {appointmentLogic.GetCustomerName(clearNumber - 1)}? (y/n): ");
                                            string clearChoice = Console.ReadLine();

                                            if (clearChoice == "y")
                                            {
                                                appointmentLogic.ClearAppointment(clearNumber);
                                                Console.WriteLine("The Appointment has been cleared successfully!");
                                            }
                                            else
                                            {
                                                Console.WriteLine("Clearing Appointment has been cancelled.");
                                            }
                                        }
                                    }
                                    break;

                                case 3:
                                    if (appointmentLogic.GetAppointmentCount() == 0)
                                    {
                                        Console.WriteLine("No Appointment to Clear.");
                                    }
                                    else
                                    {
                                        Console.Write("Are you sure you want to clear ALL appointments? (y/n): ");
                                        string clearAllChoice = Console.ReadLine();

                                        if (clearAllChoice == "y")
                                        {
                                            appointmentLogic.ClearAllAppointments();
                                            Console.WriteLine("All Appointments have been cleared successfully!");
                                        }
                                        else
                                        {
                                            Console.WriteLine("Clearing Appointments has been cancelled.");
                                        }
                                    }
                                    break;
                                case 0:
                                    Console.WriteLine("You have been logged out.");
                                    break;

                                default:
                                    Console.WriteLine("Invalid Input....");
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
                    Console.Write("Enter your Name: ");
                    string inputName = Console.ReadLine();

                    if (!authLogic.IsCustomerNameValid(inputName))
                    {
                        Console.WriteLine("Name cannot be Empty.");
                    }
                    else
                    {
                        Console.WriteLine($"\nWelcome {inputName}! Please Choose:");

                        Console.WriteLine("Type 1 - to book an appoinment");
                        Console.WriteLine("Type 2 - to view appoinment details");
                        Console.WriteLine("Type 3 - to book exit");

                        Console.Write("Enter your Choice: ");
                        int customerChoice = Convert.ToInt16(Console.ReadLine());

                        switch (customerChoice)
                        {
                            case 1:
                                List<int> chosenServices = new List<int>();
                                string addMore = "y";

                                while (addMore == "y")
                                {
                                    Console.WriteLine("\nSelect Service:");
                                    Console.WriteLine("1- Hair Service");
                                    Console.WriteLine("2- Nail Service");
                                    Console.Write("Enter your Choice: ");
                                    int categoryChoice = Convert.ToInt16(Console.ReadLine());

                                if (categoryChoice != 1 && categoryChoice != 2)
                                {
                                    Console.WriteLine("Invalid Service.");       
                                }
                                else
                                {
                                        Console.WriteLine("\nList of Salon Offerings");
                                        for (int i = appointmentLogic.GetFirstServiceNumber(categoryChoice); i <= appointmentLogic.GetLastServiceNumber(categoryChoice); i++)
                                        {
                                            Console.WriteLine($"{i} - {appointmentLogic.GetServiceName(i)} (PHP {appointmentLogic.GetServicePrice(i):N0})");
                                        }
                                        Console.Write("Enter your Choice: ");
                                        int serviceChoice = Convert.ToInt16(Console.ReadLine());

                                        if (!appointmentLogic.IsServiceInCategory(categoryChoice, serviceChoice))
                                        {
                                            Console.WriteLine("Invalid Service.");                                     
                                        }
                                        else if (chosenServices.Contains(serviceChoice))
                                        {
                                            Console.WriteLine("You already added that service.");
                                        }
                                        else
                                        {
                                            chosenServices.Add(serviceChoice);
                                            Console.WriteLine($"Added: {appointmentLogic.GetServiceName(serviceChoice)}");
                                        }
                                    }

                                    Console.Write("Add another service? (y/n): ");
                                    addMore = Console.ReadLine();
                                }

                                if (chosenServices.Count == 0)
                                {
                                    Console.WriteLine("No service selected. Booking cancelled.");
                                    break;
                                }

                                Console.Write("\nEnter Appointment Date (MM/DD/YYYY): ");
                                string inputDate = Console.ReadLine();

                                string dateMessage = appointmentLogic.CheckDate(inputDate);
                                if (dateMessage != "OK")
                                {
                                    Console.WriteLine(dateMessage);
                                    Console.WriteLine("Booking cancelled.");
                                    break;
                                }

                                Console.WriteLine("Select Time Slots");
                                for (int i = 1; i <= appointmentLogic.GetTimeSlotCount(); i++)
                                {
                                    if (appointmentLogic.IsSlotTaken(inputDate, i))
                                    {
                                        Console.WriteLine($"{i} - {appointmentLogic.GetTimeSlot(i)} (Taken)");
                                    }
                                    else
                                    {
                                        Console.WriteLine($"{i} - {appointmentLogic.GetTimeSlot(i)}");
                                    }
                                }
                                Console.Write("Enter choice: ");
                                int timeChoice = Convert.ToInt16(Console.ReadLine());

                                if (!appointmentLogic.IsValidTimeChoice(timeChoice))
                                {
                                    Console.WriteLine("Invalid time slot. Booking cancelled.");
                                    break;
                                }

                                if (appointmentLogic.IsSlotTaken(inputDate, timeChoice))
                                {
                                    Console.WriteLine("Sorry, that time slot is already taken. Booking cancelled.");
                                    break;
                                }

                                int bookingTotal = 0;
                                Console.WriteLine("\n===== BOOKING DETAILS =====");
                                Console.WriteLine($"Customer Name: {inputName}");
                                Console.WriteLine($"Date: {inputDate}");
                                Console.WriteLine($"Time: {appointmentLogic.GetTimeSlot(timeChoice)}");
                                Console.WriteLine("Services:");

                                foreach (int service in chosenServices)
                                {
                                    Console.WriteLine($"  - {appointmentLogic.GetServiceName(service)} (PHP {appointmentLogic.GetServicePrice(service):N0})");
                                    bookingTotal += appointmentLogic.GetServicePrice(service);
                                }
                                
                                Console.WriteLine($"Total: PHP {bookingTotal:N0}");

                                Console.Write("\nConfirm booking? (y/n): ");
                                string confirmBooking = Console.ReadLine();

                                if (confirmBooking == "y")
                                {
                                    foreach (int service in chosenServices)
                                    {
                                        appointmentLogic.BookAppointment(inputName, service, inputDate, timeChoice);
                                    }

                                    Console.WriteLine("Appointment successfully booked!");
                                }
                                else
                                {
                                    Console.WriteLine("Booking cancelled.");
                                }
                                break;

                            case 2:
                                if (appointmentLogic.CountAppointmentsOf(inputName) == 0)
                                {
                                    Console.WriteLine("You don't have any appointment yet.");
                                }
                                else
                                {
                                    int myTotal = 0;
                                    for (int i = 0; i < appointmentLogic.GetAppointmentCount(); i++)
                                    {
                                        if (appointmentLogic.IsOwnedBy(i, inputName))
                                        {
                                            DisplayAppointment(appointmentLogic, i);
                                            myTotal += appointmentLogic.GetPrice(i);
                                        }
                                    }
                                    Console.WriteLine($"Total of all your appointments: PHP {myTotal:N0}");
                                }
                                break;

                            case 3:
                                Console.WriteLine("Thank you for using Salon Appointment Management System");
                                break;

                            default:
                                Console.WriteLine("Incorrect input...");
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter 1, 2, or x.");
                }

                Console.WriteLine();
                Console.WriteLine("Enter 1 - for Admin Login");
                Console.WriteLine("Enter 2 - for Customer Login");
                Console.WriteLine("Enter x - to exit");
                Console.Write("User Input: ");
                checkLogin = Console.ReadLine();
            }

            Console.WriteLine("Program ended. Goodbye!");
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


 