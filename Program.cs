using System;
using System.ComponentModel.Design;

namespace SalonAppointmentManagementSystem
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Salon Appointment Management System");

            Console.WriteLine("Enter 1 - for Admin Login");
            Console.WriteLine("Enter 2 - for Customer Login");
            Console.WriteLine("Enter x - to Exit the Application");

            Console.Write("User Input: ");

            string adminUsername = "Lhynz";
            string adminPassword = "1234";

            string Customer = "None";
            string bookedService = "None";
            string bookedPrice = "PHP 0";
            string appointmentTime = "None";
            string appointmentDay = "None";

            string checkLogin = Console.ReadLine();


            while (checkLogin != "x")
            {
                if (checkLogin == "1")
                {
                    Console.Write("Enter Admin Username: ");
                    string userInput = Console.ReadLine();

                    Console.Write("Enter Admin Password: ");
                    string inputPass = Console.ReadLine();

                    if (userInput == adminUsername && inputPass == adminPassword)
                    {
                        Console.WriteLine("Welcome Admin!");
                        Console.WriteLine("Enter 1 - to view current Appointment");
                        Console.WriteLine("Enter 2 - to view clear Appointment");

                        Console.Write("Enter your choice: ");
                        int adminChoice = Convert.ToInt16(Console.ReadLine());

                        switch (adminChoice)
                        {
                            case 1:
                                if (bookedService == "None")
                                {
                                    Console.WriteLine("No Appointment has been booked yet!");
                                }
                                else
                                {
                                    Console.WriteLine("Current Appointment: ");
                                    Console.WriteLine($"Customer Name: {Customer}");
                                    Console.WriteLine($"Service Booked: {bookedService}");
                                    Console.WriteLine($"Appointment Time: {appointmentTime}");
                                }
                                break;
                            case 2:
                                if (bookedService == "None")
                                {
                                    Console.WriteLine("No Appointment to Clear.");
                                }
                                else
                                {
                                    Console.Write($"Are you sure you want to clear the appointment for {Customer}? (y/n)");

                                    string clearChoice = Console.ReadLine();

                                    if (clearChoice == "y")
                                    {
                                        bookedService = "None";
                                        bookedPrice = "PHP 0";
                                        appointmentDay = "None";
                                        appointmentTime = "None";
                                        Console.WriteLine("The Appointment has been cleared successfully!");

                                    }
                                    else
                                    {
                                        Console.WriteLine("Clearing Appointment has been cancelled.");
                                    }
                                }
                                break;

                            default:
                                Console.WriteLine("Invalid Input....");
                                break;

                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid Information.");
                        break;
                    }
                }

                else if (checkLogin == "2")
                {
                    Console.Write("Enter your Name");
                    string inputName = Console.ReadLine();

                    if (inputName == "" || inputName == null)
                    {
                        Console.WriteLine("Name cannot be Empty.");
                    }
                    else
                        Customer = inputName; 
                        Console.WriteLine($"Welcome {Customer}! Please Choose:");

                        Console.WriteLine("Type 1 - to book an appoinment");
                        Console.WriteLine("Type 2 - to view appoinment details");
                        Console.WriteLine("Type 3 - to book exit");

                        Console.Write("Enter your Choice: ");
                        int customerChoice = Convert.ToInt16(Console.ReadLine());

                        switch (customerChoice)
                        {
                            case 1:
                                if (bookedService != "None")
                            {
                                Console.WriteLine("There's an Existing Appointment. The Admin must clear-out the details before you booked again.");
                                    //Ma'am since basic pa lang po tayo, nilagay ko po muna ito, since wala po ring example na array sa ginawa po natin.
                                break;                  
                            }


                        
                            






                    }
                            





                }







                }







            }
        }
    }
    
