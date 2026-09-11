namespace SalonAppointmentManagementSystem
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Salon Appoinment Management System");

            Console.WriteLine("Enter 1 - for Admin Login");
            Console.WriteLine("Enter 2 - for Customer Login");
            Console.WriteLine("Enter x - to Exit the Application");

            Console.Write("User Input: ");

            string adminUsername = "Lhynz";
            string adminPassword = "1234";

            string Customer = "ChaCha";
            string bookService = "None";
            string appointmentTime = "None";

            string checkLogin = Console.ReadLine();


            while (checkLogin != "x")
            {
                if (checkLogin == "1") ;
                {
                    Console.Write("Enter Admin Username: ");
                    string userInput = Console.ReadLine();

                    Console.Write("Enter Admin Password: ");
                    string inputPass= Console.ReadLine();

                    if (adminUsername == userInput && adminPassword == inputPass)
                    {
                        Console.Write("Welcome Admin!");
                        Console.WriteLine("Enter 1 - to view current Appointment");
                        Console.WriteLine("Enter 2 - to view clear Appointment");
                        
                        Console.Write("Enter your choice: ");
                        int adminChoice = Convert.ToInt16(Console.ReadLine());
                    }

                    switch (adminChoice)
                    {
                        case 1:
                            Console.WriteLine("Current Appointment: ");
                            Console.WriteLine($"Customer Name: {Customer}");
                            Console.WriteLine($"Service Booked: {bookService}");
                            Console.WriteLine($"Appointment Time: {appointmentTime}");
                            break;

                        case 2:
                            bookService = "None";
                            appointmentTime = "None";
                            Console.WriteLine("The Appointment has been cleared successfully!");
                            break;

                        default:
                            Console.WriteLine("Invalid Input....");
                            break;
                    }

                }








            }







        }
    }
}
