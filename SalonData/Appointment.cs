namespace SalonData
{
    public class Appointment
    {
        public string CustomerName { get; set; } = "";
        public string Service { get; set; } = "";
        public int Price { get; set; }
        public string Date { get; set; } = "";
        public string Time { get; set; } = "";
        [System.Text.Json.Serialization.JsonIgnore(Condition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull)]
        public string? BookingId { get; set; }
        public bool CancellationRequested { get; set; }
    }
}
