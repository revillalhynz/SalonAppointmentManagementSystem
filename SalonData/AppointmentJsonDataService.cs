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

        public void AddAppointment(string customerName, string service, int price, string date, string time)
        {
            Appointment newAppointment = new Appointment();
            newAppointment.CustomerName = customerName;
            newAppointment.Service = service;
            newAppointment.Price = price;
            newAppointment.Date = date;
            newAppointment.Time = time;

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
    }
}
