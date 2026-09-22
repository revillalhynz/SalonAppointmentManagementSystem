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
                    Console.Write("Enter your Name": );
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
                            ////iniba ko po sa part na ito, mas ini-specify ko po yung mga choices na makikita po talaga sa Salon.
                            Console.WriteLine("Select Service:");
                            Console.WriteLine("1- Hair Service");
                            Console.WriteLine("2- Nail Service");
                            Console.Write("Enter your Choice: ");
                            int categoryChoice = Convert.ToInt16(Console.ReadLine());

                            int serviceChoice = 0;

                            if (categoryChoice == 1)
                            {
                                Console.WriteLine("""

                                            1 - Haircut / Eyebrow Shaving / Blow Dry / Flat Ironing / Shampoo (PHP 120)
                                            2 - Hair + Detox (PHP 1,200)
                                            3 - Haircolor + Detox (PHP 1,200)
                                            4 - Haircolor + BK (PHP 1,490)
                                            5 - Highlights + Detox (PHP 1,200)
                                            6 - Highlights + BK (PHP 1,490)
                                            7 - Nano Rebond and BK (PHP 1,600)
                                            8 - Brazilian Keratin (solo) (PHP 500)
                                            9  - Nano Rebond (solo) (PHP 1,200)
                                            10 - Hair Color (solo) (PHP 990)
                                            11 - Highlights (solo) (PHP 990)
                                            12 - Hair Perming / Cold Wave Solution (PHP 990)
                                        """);
                                Console.Write("Enter your Choice: ");
                                serviceChoice = Convert.ToInt16(Console.ReadLine());
                            }

                            else if (categoryChoice == 2)
                            {
                                Console.WriteLine("""
 
                                            13 - Manicure with Hand Massage (PHP 250)
                                            14 - Pedicure with Foot Massage (PHP 250)
                                            15 - Mani-Pedi with Massage (PHP 500)
                                            16 - Foot Spa with Foot Massage (PHP 500)
 
                                        """);
                                Console.Write("Enter your Choice: ");
                                serviceChoice = Convert.ToInt16(Console.ReadLine());

                            }
                            else
                            {
                                Console.WriteLine("Invalid category. Booking cancelled.");
                                break;
                            }

                            switch (serviceChoice)
                            {
                                case 1:
                                    bookedService = "Haircut / Eyebrow Shaving / Blow Dry / Flat Ironing / Shampoo ";
                                    bookedPrice = "Php 120;
                                    break;
                                case 2:
                                    bookedService = "Hair Detox";
                                    bookedPrice = "PHP 1,200";
                                    break;
                                case 3:
                                    bookedService = "Haircolor + Hair Detox";
                                    bookedPrice = "PHP 1,200";
                                    break;
                                case 4:
                                    bookedService = "Haircolor + BK";
                                    bookedPrice = "PHP 1,490";
                                    break;
                                case 5:
                                    bookedService = "Highlights + Detox";
                                    bookedPrice = "PHP 1,200";
                                    break;
                                case 6:
                                    bookedService = "Highlights + BK";
                                    bookedPrice = "PHP 1,490";
                                    break;
                                case 7:
                                    bookedService = "Nano Rebond and BK";
                                    bookedPrice = "PHP 1,600";
                                    break;
                                case 8:
                                    bookedService = "Brazilian Keratin (solo)";
                                    bookedPrice = "PHP 500";
                                    break;
                                case 9:
                                    bookedService = "Nano Rebond (solo)";
                                    bookedPrice = "PHP 1,200";
                                    break;
                                case 10:
                                    bookedService = "Hair Color (solo)";
                                    bookedPrice = "PHP 990";
                                    break;
                                case 11:
                                    bookedService = "Highlights (solo)";
                                    bookedPrice = "PHP 990";
                                    break;
                                case 12:
                                    bookedService = "Hair Perming (cold wave solution)";
                                    bookedPrice = "PHP 990";
                                    break;
                                case 13:
                                    bookedService = "Manicure with Hand Massage";
                                    bookedPrice = "PHP 250";
                                    break;
                                case 14:
                                    bookedService = "Pedicure with Foot Massage";
                                    bookedPrice = "PHP 250";
                                    break;
                                case 15:
                                    bookedService = "Mani-Pedi with Massage";
                                    bookedPrice = "PHP 500";
                                    break;
                                case 16:
                                    bookedService = "Foot Spa with Foot Massage";
                                    bookedPrice = "PHP 500";
                                    break;
                                default:
                                    Console.WriteLine("Invalid service. Booking cancelled.");
                                    bookedService = "None";
                                    bookedPrice = "PHP 0)";
                                    break;
                            }

                            if (bookedService == "None")
                            {
                                break;
                            }

                            Console.Write("Enter Appointment Day(Monday to Saturday): ");
                            string inputDay = Console.ReadLine();
                            string inputDayFixed = inputDay.ToLower();

                            if (inputDayFixed == "monday" || inputDayFixed == "tuesday" || inputDayFixed == "wednesday" || inputDayFixed == "thursday" || inputDayFixed == "friday" || inputDayFixed == "saturday")
                            {
                                appointmentDay = inputDay;
                            }
                            else
                            {
                                Console.WriteLine("Invalid DAY. Our Salon is closed every Sunday.");
                                bookedService = "None";
                                bookedPrice = "None";
                                break;
                            }

                            Console.WriteLine("Select Time Slots");
                            Console.WriteLine("1 - 9:00 AM");
                            Console.WriteLine("2 - 1:00 PM");
                            Console.WriteLine("3 - 2:OO PM");
                            Console.WriteLine("4 - 4:OO PM");
                            Console.Write("Enter choice: ");
                            int itemChoice = Convert.ToInt16(Console.ReadLine());

                            switch (itemChoice)
                            {
                                case 1:
                                    appointmentTime = "9:00 AM";
                                    break;
                                case 2:
                                    appointmentTime = "1:00 PM";
                                    break;
                                case 3:
                                    appointmentTime = "2:00 PM";
                                    break;
                                case 4:
                                    appointmentTime = "4:00 PM";
                                    break;
                                default:
                                    appointmentTime = "9:00 AM";
                                    break;
                            }

                            Console.WriteLine("Appointment sucessfully booked!");
                            break;

                        case 2:
                            if (bookedService == "None")
                            {
                                Console.WriteLine("You don't have any apointment yet.");
                            }
                            else
                            {
                                Console.WriteLine($"Service: {bookedService}");
                                Console.WriteLine($"Price: {bookedPrice}");
                                Console.WriteLine($"Day: {appointmentDay}");
                                Console.WriteLine($"Time: {appointmentTime}");
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
    }
}

 