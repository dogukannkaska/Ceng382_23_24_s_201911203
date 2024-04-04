using System;
using System.IO;
using System.Text.Json;
using System.Globalization;

public class RoomData
{
    public Room[] Room { get; set; }
}

public class Room
{
    public string roomId { get; set; }
    public string roomName { get; set; }
    public int capacity { get; set; }
}

public class Reservation
{
    public DateTime time { get; set; }
    public DateTime date { get; set; }
    public string reserverName { get; set; }
    public Room room { get; set; }
}

public class ReservationHandler
{
    private Reservation[][] reservations;
    public RoomData roomData { get; set; }
    private DateTime[] dateData;

    public ReservationHandler(RoomData rooms, DateTime[] dates)
    {
        roomData = rooms;
        dateData = dates;
        reservations = new Reservation[roomData.Room.Length][];
        for (int i = 0; i < roomData.Room.Length; i++)
        {
            reservations[i] = new Reservation[dates.Length];
        }
    }

    public bool AddReservation(Reservation reservation)
    {
        int roomIndex = Array.FindIndex(roomData.Room, room => room.roomName == reservation.room.roomName);
        if (roomIndex == -1) 
        {
            Console.WriteLine("Room not found.");
            return false;
        }

        int dateIndex = Array.FindIndex(dateData, date => date.Date == reservation.date.Date);
        if (dateIndex == -1)
        {
            Console.WriteLine("Date not found.");
            return false;
        }

        if (reservations[roomIndex][dateIndex] == null)
        {
            reservations[roomIndex][dateIndex] = reservation;
            return true;
        }
        else
        {
            Console.WriteLine($"Cannot add reservation. Room {reservation.room.roomName} is already booked for {reservation.date:dd.MM.yyyy}.");
            return false;
        }
    }

    public bool RemoveReservation(string roomName, DateTime date, string reserverName)
    {
        int roomIndex = Array.FindIndex(roomData.Room, room => room.roomName == roomName);
        if (roomIndex == -1) 
        {
            Console.WriteLine("Room not found.");
            return false;
        }

        int dateIndex = Array.FindIndex(dateData, d => d.Date == date.Date);
        if (dateIndex == -1)
        {
            Console.WriteLine("Date not found.");
            return false;
        }

        var reservation = reservations[roomIndex][dateIndex];
        if (reservation != null && reservation.reserverName == reserverName)
        {
            reservations[roomIndex][dateIndex] = null;
            Console.WriteLine("Reservation removed successfully.");
            return true;
        }
        else
        {
            return false;
        }
    }

public void DisplayWeeklySchedule()
{
    // Başlık olarak tarihleri yazdır
    Console.Write("Room / Capacity".PadRight(15));
    foreach (var date in dateData)
    {
        Console.Write($" | {date:dd/MM/yy}".PadRight(15));
    }
    Console.WriteLine();

    // Oda bilgileri, kapasiteleri ve o tarihlerdeki rezervasyon durumlarını yazdır
    foreach (var room in roomData.Room)
    {
        // Oda adını ve kapasitesini yazdır
        Console.Write($"{room.roomName} / ({room.capacity})".PadRight(15)); 
        
        foreach (var date in dateData)
        {
            int roomIndex = Array.FindIndex(roomData.Room, r => r.roomName == room.roomName);
            int dateIndex = Array.FindIndex(dateData, d => d.Date == date.Date);
            var reservation = reservations[roomIndex][dateIndex];

            // Rezervasyon yapan kişinin adını veya "Boş" yazdır
            string status = reservation == null ? "Available" : reservation.reserverName;
            Console.Write($" | {status.PadRight(12)}");
        }
        Console.WriteLine(); // Her oda için yeni bir satıra geç
    }
}


}

class Program
{
    static void Main(string[] args)

    { 

        var reservation1 = new Reservation
        {
            room = new Room { roomName = "A-101" }, // Room sınıfınızın yapısı ve kullanımı önemli
            date = DateTime.Today.AddDays(1),
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            reserverName = "Ali"
        };
        
        var reservation2 = new Reservation
        {
            room = new Room { roomName = "A-102" },
            date = DateTime.Today.AddDays(2),
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            reserverName = "Veli"
        };
        
        var reservation3 = new Reservation
        {
            room = new Room { roomName = "A-103" },
            date = DateTime.Today.AddDays(3),
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            reserverName = "Ayşe"
        };


         var reservation4 = new Reservation
        {
            room = new Room { roomName = "A-104" },
            date = DateTime.Today.AddDays(4),
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            reserverName = "Barış"
        };
        

        var reservation5 = new Reservation
        {
            room = new Room { roomName = "A-105" },
            date = DateTime.Today.AddDays(5),
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            reserverName = "Doğukan"
        };


        var reservation6 = new Reservation
        {
            room = new Room { roomName = "A-106" },
            date = DateTime.Today.AddDays(6),
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            reserverName = "Hikmet"
        };
        
        string fileName = "./Data.json";
        string jsonString = File.ReadAllText(fileName);
        RoomData roomData = JsonSerializer.Deserialize<RoomData>(jsonString);

        DateTime[] dates = new DateTime[7];
        for (int i = 0; i < 7; i++)
        {
            dates[i] = DateTime.Today.AddDays(i);
        }

        ReservationHandler reservationHandler = new ReservationHandler(roomData, dates);

        // Add a new reservation
        

        // Attempt to remove a reservation
        reservationHandler.RemoveReservation("A-101", DateTime.Today.AddDays(1), "John Doe");

        // Display weekly schedule
                
                
                reservationHandler.AddReservation(reservation1);
                reservationHandler.AddReservation(reservation2);
                reservationHandler.AddReservation(reservation3);
                reservationHandler.AddReservation(reservation4);
                reservationHandler.AddReservation(reservation5);
                reservationHandler.AddReservation(reservation6);



        reservationHandler.DisplayWeeklySchedule();

         Console.WriteLine("Enter your infos for remove reservation.");
        Console.Write("Room Name: ");
        string roomName = Console.ReadLine();
        Console.Write("Date (gg.aa.yyyy): ");
        DateTime date;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            Console.WriteLine("Geçersiz tarih formatı, lütfen gg.aa.yyyy formatında bir tarih giriniz:");
        }
        Console.Write("Customer Name: ");
        string reserverName = Console.ReadLine();

        // Rezervasyonu sil
        bool isRemoved = reservationHandler.RemoveReservation(roomName, date, reserverName);
        if (isRemoved)
        {
            Console.WriteLine("Reservation is deleted.");
        }
        else
        {
            Console.WriteLine("Rezervasyon bulunamadı veya silinemedi.");
        }
        reservationHandler.DisplayWeeklySchedule();

    }

    
}
