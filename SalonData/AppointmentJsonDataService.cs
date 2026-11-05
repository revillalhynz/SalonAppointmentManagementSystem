using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SalonData
{
 
    public class AppointmentJsonDataService
    {
        private List<Appointment> appointments = new List<Appointment>();
        private string _jsonAppointmentFileName;

        public AppointmentJsonDataService()
        {
            _jsonAppointmentFileName =
                $"{AppDomain.CurrentDomain.BaseDirectory}/Appointments.json";

            RetrieveDataFromJsonFile();
        }

        private void SaveDataToJsonFile()
        {
            string json = JsonSerializer.Serialize(appointments,
                new JsonSerializerOptions { WriteIndented = true });

            File.WriteAllText(_jsonAppointmentFileName, json);
        }

        private void RetrieveDataFromJsonFile()
        {
            if (!File.Exists(_jsonAppointmentFileName))
            {
                return;
            }

            using (var jsonFileReader = File.OpenText(_jsonAppointmentFileName))
            {
                List<Appointment>? loaded = JsonSerializer.Deserialize<List<Appointment>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions
                    { PropertyNameCaseInsensitive = true });

                if (loaded != null)
                {
                    appointments = loaded;
                }
            }
        }

        public List<Appointment> RetrieveAllAppointments()
        {
            return appointments;
        }

        public void AddAppointment(string customerName, string service, int price, string date, string time,
            string? bookingId = null)
        {
            Appointment newAppointment = new Appointment();
            newAppointment.CustomerName = customerName;
            newAppointment.Service = service;
            newAppointment.Price = price;
            newAppointment.Date = date;
            newAppointment.Time = time;
            newAppointment.BookingId = bookingId;

            appointments.Add(newAppointment);
            SaveDataToJsonFile();
        }

        public void RemoveAppointmentAt(int index)
        {
            appointments.RemoveAt(index);
            SaveDataToJsonFile();
        }

        public void RemoveAllAppointments()
        {
            appointments.Clear();
            SaveDataToJsonFile();
        }

        public bool IsSameBooking(int firstIndex, int secondIndex)
        {
            Appointment first = appointments[firstIndex];
            Appointment second = appointments[secondIndex];

            if (first.BookingId != null || second.BookingId != null)
            {
                return first.BookingId == second.BookingId;
            }

            // Older records used one time slot for all services in the same booking.
            return first.Date == second.Date && first.Time == second.Time
                && string.Equals(first.CustomerName?.Trim(), second.CustomerName?.Trim(),
                    StringComparison.CurrentCultureIgnoreCase);
        }

        public void RequestCancellation(int index)
        {
            for (int i = 0; i < appointments.Count; i++)
            {
                if (IsSameBooking(index, i))
                {
                    appointments[i].CancellationRequested = true;
                }
            }
            SaveDataToJsonFile();
        }

        public void RemoveRequestedBooking(int index)
        {
            for (int i = appointments.Count - 1; i >= 0; i--)
            {
                if (i != index && IsSameBooking(index, i))
                {
                    appointments.RemoveAt(i);
                    if (i < index)
                    {
                        index--;
                    }
                }
            }
            appointments.RemoveAt(index);
            SaveDataToJsonFile();
        }
    }
}
