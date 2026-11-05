using System;
using System.Collections.Generic;
using System.Globalization;
using SalonData;

namespace SalonBusinessLogic
{
    public class AppointmentLogic
    {
        AppointmentJsonDataService dataService = new AppointmentJsonDataService();
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
        public const int BookingsPerTimeSlot = 3;

        List<int> chosenServices = new List<int>();

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
            return GetAvailableSpots(date, timeNumber) == 0;
        }

        public int GetAvailableSpots(string date, int timeNumber)
        {
            string time = GetTimeSlot(timeNumber);
            List<Appointment> appointments = dataService.RetrieveAllAppointments();
            int availableSpots = BookingsPerTimeSlot;

            for (int i = 0; i < appointments.Count; i++)
            {
                if (appointments[i].Date == date && appointments[i].Time == time && IsFirstBookingRecord(i))
                {
                    availableSpots--;
                    if (availableSpots == 0)
                    {
                        break;
                    }
                }
            }

            return availableSpots;
        }
        public bool IsDateFull(string date)
        {
            for (int i = 1; i <= GetTimeSlotCount(); i++)
            {
                if (!IsSlotTaken(date, i))
                {
                    return false;
                }
            }

            return true;
        }

        public void ClearChosenServices()
        {
            chosenServices.Clear();
        }
        public bool AddChosenService(int serviceNumber)
        {
            if (chosenServices.Contains(serviceNumber))
            {
                return false;
            }

            chosenServices.Add(serviceNumber);
            return true;
        }

        public int GetChosenServiceCount()
        {
            return chosenServices.Count;
        }
        public int GetChosenService(int index)
        {
            return chosenServices[index];
        }

        public int GetChosenServicesTotal()
        {
            int total = 0;

            for (int i = 0; i < chosenServices.Count; i++)
            {
                total = total + servicePrices[chosenServices[i] - 1];
            }

            return total;
        }

        public void BookAppointment(string customerName, int serviceNumber, string date, int timeNumber,
            string? bookingId = null)
        {
            dataService.AddAppointment(
                customerName.Trim(),
                serviceNames[serviceNumber - 1],
                servicePrices[serviceNumber - 1],
                date,
                timeSlots[timeNumber - 1],
                bookingId);
        }

        public void BookChosenServices(string customerName, string date, int timeNumber)
        {
            string bookingId = Guid.NewGuid().ToString("N");
            for (int i = 0; i < chosenServices.Count; i++)
            {
                BookAppointment(customerName, chosenServices[i], date, timeNumber, bookingId);
            }

            chosenServices.Clear();
        }

        public bool IsFirstBookingRecord(int index)
        {
            for (int i = 0; i < index; i++)
            {
                if (dataService.IsSameBooking(index, i))
                {
                    return false;
                }
            }

            return true;
        }

        public bool CanRequestCancellation(int index)
        {
            return DateTime.TryParseExact(GetDate(index), "MM/dd/yyyy", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime date) && date >= DateTime.Today.AddDays(2);
        }

        public bool IsCancellationRequested(int index)
        {
            Appointment appointment = dataService.RetrieveAllAppointments()[index];
            return appointment.CancellationRequested && IsFirstBookingRecord(index);
        }

        public bool RequestCancellation(int index, string name)
        {
            if (!IsOwnedBy(index, name) || !CanRequestCancellation(index) || IsCancellationRequested(index))
            {
                return false;
            }

            dataService.RequestCancellation(index);
            return true;
        }

        public void ApproveCancellation(int index)
        {
            if (IsCancellationRequested(index))
            {
                dataService.RemoveRequestedBooking(index);
            }
        }
        public int GetAppointmentCount()
        {
            return dataService.RetrieveAllAppointments().Count;
        }

        public IEnumerable<int> GetAppointmentIndicesInDateOrder()
        {
            List<Appointment> appointments = dataService.RetrieveAllAppointments();
            return Enumerable.Range(0, appointments.Count)
                .OrderBy(index => DateTime.TryParseExact(appointments[index].Date, "MM/dd/yyyy",
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) ? date : DateTime.MaxValue)
                .ThenBy(index => Array.IndexOf(timeSlots, appointments[index].Time));
        }

        public string GetCustomerName(int index)
        {
            return dataService.RetrieveAllAppointments()[index].CustomerName;
        }

        public string GetService(int index)
        {
            return dataService.RetrieveAllAppointments()[index].Service;
        }

        public int GetPrice(int index)
        {
            return dataService.RetrieveAllAppointments()[index].Price;
        }

        public string GetDate(int index)
        {
            return dataService.RetrieveAllAppointments()[index].Date;
        }

        public string GetTime(int index)
        {
            return dataService.RetrieveAllAppointments()[index].Time;
        }

        public bool IsOwnedBy(int index, string name)
        {
            return string.Equals(GetCustomerName(index)?.Trim(), name.Trim(),
                StringComparison.CurrentCultureIgnoreCase);
        }

        public int CountAppointmentsOf(string name)
        {
            int count = 0;

            for (int i = 0; i < GetAppointmentCount(); i++)
            {
                if (IsOwnedBy(i, name))
                {
                    count++;
                }
            }

            return count;
        }

        public int GetTotalOf(string name)
        {
            int total = 0;

            for (int i = 0; i < GetAppointmentCount(); i++)
            {
                if (IsOwnedBy(i, name))
                {
                    total = total + GetPrice(i);
                }
            }

            return total;
        }

        public bool IsValidAppointmentNumber(int number)
        {
            return number >= 1 && number <= GetAppointmentCount();
        }
        public void ClearAppointment(int number)
        {
            dataService.RemoveAppointmentAt(number - 1);
        }

        public void ClearAllAppointments()
        {
            dataService.RemoveAllAppointments();
        }
    }
}
