namespace HotelReservationSystem.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;  // Null atanamaz özellikler
        public int Capacity { get; set; }
        public string View { get; set; } = string.Empty;
        public List<Reservation> Reservations { get; set; } = new List<Reservation>();  // Null atanamaz özellikler
    }
}
