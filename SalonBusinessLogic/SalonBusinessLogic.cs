using System;
using System.Collections.Generic;
using System.Text;

namespace SalonBusinessLogic
{
    public class AppointmentLogic
    {
        string[] serviceNames = new string[]
        {
            "Haircut / Eyebrow Shaving / Blow Dry / Flat Ironing / Shampoo",
            "Haircut + Detox",
            "Haircolor + Hair Detox",
            "Haircolor + BK",
            "Highlights + Detox",
            "Highlights + BK",
            "Nano Rebond and BK",
            "Brazilian Keratin (solo)",
            "Nano Rebond (solo)",
            "Hair Color (solo)",
            "Highlights (solo)",
            "Hair Perming (cold wave solution)",
            "Manicure with Hand Massage",
            "Pedicure with Foot Massage",
            "Mani-Pedi with Massage",
            "Foot Spa with Foot Massage"
        };

        int[] servicePrices = new int[]
        {
            120, 990, 1200, 1490,
            1200, 1490, 1600, 500,
            1200, 990, 990, 990,
            250, 250, 500, 500
        };

        string[] timeSlots = new string[] { "9:00 AM", "1:00 PM", "2:00 PM", "4:00 PM" };

        List<string> customerNames = new List<string>();
        List<string> bookedServices = new List<string>();
        List<int> bookedPrices = new List<int>();
        List<string> appointmentDates = new List<string>();
        List<string> appointmentTimes = new List<string>();

        public void PopulateDummyAppointments()
        {
            customerNames.Add("Lhynz Revilla");
            bookedServices.Add(serviceNames[1]);
            bookedPrices.Add(servicePrices[1]);
            appointmentDates.Add("11/03/2026");
            appointmentTimes.Add(timeSlots[0]);

            customerNames.Add("Charel Guillermo");
            bookedServices.Add(serviceNames[14]);
            bookedPrices.Add(servicePrices[14]);
            appointmentDates.Add("11/05/2026");
            appointmentTimes.Add(timeSlots[1]);

            customerNames.Add("Miles Claudia");
            bookedServices.Add(serviceNames[9]);
            bookedPrices.Add(servicePrices[9]);
            appointmentDates.Add("11/07/2026");
            appointmentTimes.Add(timeSlots[3]);
        }

        public int GetFirstServiceNumber(int category)
        {
            if (category == 1)
            {
                return 1;
            }
            else
            {
                return 13;
            }
        }

        public int GetLastServiceNumber(int category)
        {
            if (category == 1)
            {
                return 12;
            }
            else
            {
                return serviceNames.Length;
            }
        }

        public bool IsServiceInCategory(int category, int service)
        {
            if (category != 1 && category != 2)
            {
                return false;
            }

            return service >= GetFirstServiceNumber(category) && service <= GetLastServiceNumber(category);
        }

        public string GetServiceName(int number)
        {
            return serviceNames[number - 1];
        }

        public int GetServicePrice(int number)
        {
            return servicePrices[number - 1];
        }

        public string CheckDate(string date)
        {
            if (date == null)
            {
                return "Please type the date as MM/DD/YYYY.";
            }

            string[] parts = date.Split('/');

            if (parts.Length != 3 || parts[0].Length != 2 || parts[1].Length != 2 || parts[2].Length != 4)
            {
                return "Wrong format. Please use MM/DD/YYYY (example: 11/05/2026).";
            }

            foreach (string part in parts)
            {
                if (!IsAllDigits(part))
                {
                    return "Wrong format. Please use numbers only (example: 11/05/2026).";
                }
            }

            int month = Convert.ToInt32(parts[0]);
            int day = Convert.ToInt32(parts[1]);
            int year = Convert.ToInt32(parts[2]);

            if (month < 1 || month > 12)
            {
                return "Invalid month. Please choose 01 to 12.";
            }

            if (year < 1)
            {
                return "Invalid year.";
            }

            if (day < 1 || day > DateTime.DaysInMonth(year, month))
            {
                return "Invalid day for that month.";
            }

            DateTime chosenDate = new DateTime(year, month, day);

            if (chosenDate < DateTime.Today)
            {
                return "That date is already past. Please choose a future date.";
            }

            if (chosenDate.DayOfWeek == DayOfWeek.Sunday)
            {
                return "Our Salon is closed every Sunday.";
            }

            return "OK";
        }

        bool IsAllDigits(string text)
        {
            foreach (char letter in text)
            {
                if (letter < '0' || letter > '9')
                {
                    return false;
                }
            }

            return true;
        }

        public int GetTimeSlotCount()
        {
            return timeSlots.Length;
        }

        public string GetTimeSlot(int number)
        {
            return timeSlots[number - 1];
        }

        public bool IsValidTimeChoice(int number)
        {
            return number >= 1 && number <= timeSlots.Length;
        }
        public bool IsSlotTaken(string date, int timeNumber)
        {
            string time = timeSlots[timeNumber - 1];

            for (int i = 0; i < appointmentDates.Count; i++)
            {
                if (appointmentDates[i] == date && appointmentTimes[i] == time)
                {
                    return true;
                }
            }

            return false;
        }

        public void BookAppointment(string customerName, int serviceNumber, string date, int timeNumber)
        {
            customerNames.Add(customerName);
            bookedServices.Add(serviceNames[serviceNumber - 1]);
            bookedPrices.Add(servicePrices[serviceNumber - 1]);
            appointmentDates.Add(date);
            appointmentTimes.Add(timeSlots[timeNumber - 1]);
        }
        public int GetAppointmentCount()
        {
            return customerNames.Count;
        }

        public string GetCustomerName(int index)
        {
            return customerNames[index];
        }

        public string GetService(int index)
        {
            return bookedServices[index];
        }

        public int GetPrice(int index)
        {
            return bookedPrices[index];
        }

        public string GetDate(int index)
        {
            return appointmentDates[index];
        }

        public string GetTime(int index)
        {
            return appointmentTimes[index];
        }

        public bool IsOwnedBy(int index, string name)
        {
            return customerNames[index].ToLower() == name.ToLower();
        }

        public int CountAppointmentsOf(string name)
        {
            int count = 0;

            for (int i = 0; i < customerNames.Count; i++)
            {
                if (IsOwnedBy(i, name))
                {
                    count++;
                }
            }

            return count;
        }
        public bool IsValidAppointmentNumber(int number)
        {
            return number >= 1 && number <= customerNames.Count;
        }
        public void ClearAppointment(int number)
        {
            int index = number - 1;

            customerNames.RemoveAt(index);
            bookedServices.RemoveAt(index);
            bookedPrices.RemoveAt(index);
            appointmentDates.RemoveAt(index);
            appointmentTimes.RemoveAt(index);
        }

        public void ClearAllAppointments()
        {
            customerNames.Clear();
            bookedServices.Clear();
            bookedPrices.Clear();
            appointmentDates.Clear();
            appointmentTimes.Clear();
        }
    }
}