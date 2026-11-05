using System;
using System.Collections.Generic;
using SalonData;

namespace SalonData
{
    public class AppointmentDataService
    {
        private List<Appointment> appointments = new List<Appointment>();

        public AppointmentDataService()
        {
            PopulateDummyAppointments();
        }

        private void PopulateDummyAppointments()
        {
            AddAppointment("Lhynz Revilla", "Haircut + Detox", 990, "11/03/2026", "9:00 AM");
            AddAppointment("Charel Guillermo", "Mani-Pedi with Massage", 500, "11/05/2026", "1:00 PM");
            AddAppointment("Miles Claudia", "Hair Color (solo)", 990, "11/07/2026", "4:00 PM");
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
        }

        public void RemoveAppointmentAt(int index)
        {
            appointments.RemoveAt(index);
        }

        public void RemoveAllAppointments()
        {
            appointments.Clear();
        }
    }
}
