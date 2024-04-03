using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[] Rooms { get; set; }
}

public class Room
{
    [JsonPropertyName("roomId")]
    public string roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string roomName { get; set; }

    [JsonPropertyName("capacity")]
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
    private RoomData roomData;
    private DateTime[] dateData;

    public ReservationHandler(RoomData rooms, DateTime[] dates)
    {
        roomData = rooms;
        dateData = dates;
        reservations = new Reservation[roomData.Rooms.Length][];
        for (int i = 0; i < roomData.Rooms.Length; i++)
        {
            reservations[i] = new Reservation[dates.Length];
        }
    }

    public bool AddReservation(Reservation reservation)
    {
        int roomIndex = Array.FindIndex(roomData.Rooms, room => room.roomName == reservation.room.roomName);
        int dateIndex = Array.FindIndex(dateData, date => date.Date == reservation.date.Date);

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

    // Rezervasyon silme metodu
    public bool RemoveReservation(string roomName, DateTime date, string reserverName)
    {
        int roomIndex = Array.FindIndex(roomData.Rooms, room => room.roomName == roomName);
        int dateIndex = Array.FindIndex(dateData, d => d.Date == date.Date);

        if (roomIndex == -1 || dateIndex == -1)
        {
            Console.WriteLine("Room or date not found.");
            return false;
        }

        var reservation = reservations[roomIndex][dateIndex];
        if (reservation != null && reservation.reserverName == reserverName && reservation.date.Date == date.Date)
        {
            reservations[roomIndex][dateIndex] = null; // Rezervasyonu sil
            Console.WriteLine("Reservation removed successfully.");
            return true;
        }
        else
        {
            Console.WriteLine("Reservation not found.");
            return false;
        }
    }

    


 public void displayWeeklySchedule()
{
    Console.WriteLine("Weekly Schedule");
    Console.Write("Room".PadRight(15) + " | " + "Capacity".PadRight(8) + " | ");
    for (int i = 0; i < dateData.Length; i++)
    {
        Console.Write($"{dateData[i].ToString("dd.MM.yyyy").PadRight(10)} | ");
    }
    Console.WriteLine();
    Console.WriteLine("-".PadRight(dateData.Length * 13 + 24, '-'));
    
    for (int j = 0; j < roomData.Rooms.Length; j++)
    {
        Console.Write($"{roomData.Rooms[j].roomName.PadRight(15)} | {roomData.Rooms[j].capacity.ToString().PadRight(8)} | ");
        for (int k = 0; k < reservations[j].Length; k++)
        {
            string displayText = "Free".PadRight(10);
            if (reservations[j][k] != null)
            {
                displayText = reservations[j][k].reserverName.PadRight(10);
            }
            Console.Write($"{displayText} | ");
        }
        Console.WriteLine();
    }
}

}

class Program
{
    static void Main(string[] args)
    {
        
        DateTime[] dateData = new DateTime[]
        {
            DateTime.Now.Date.AddDays(1),
            DateTime.Now.Date.AddDays(2),
            DateTime.Now.Date.AddDays(3),
            DateTime.Now.Date.AddDays(4),
            DateTime.Now.Date.AddDays(5),
            DateTime.Now.Date.AddDays(6),
        };

        RoomData roomData = new RoomData
        {
            Rooms = new Room[]
            {
                new Room { roomId = "001", roomName = "A-101", capacity = 30 },
                new Room { roomId = "002", roomName = "A-102", capacity = 24 },
                new Room { roomId = "003", roomName = "A-103", capacity = 26 },
                new Room { roomId = "004", roomName = "A-104", capacity = 28 },
                new Room { roomId = "005", roomName = "A-105", capacity = 30 },
                new Room { roomId = "006", roomName = "A-106", capacity = 32 },
                new Room { roomId = "007", roomName = "A-107", capacity = 34 },
                new Room { roomId = "008", roomName = "A-108", capacity = 36 },
                new Room { roomId = "009", roomName = "A-109", capacity = 38 },
                new Room { roomId = "010", roomName = "A-110", capacity = 40 },
                new Room { roomId = "011", roomName = "A-111", capacity = 42 },
                new Room { roomId = "012", roomName = "A-112", capacity = 44 },
                new Room { roomId = "013", roomName = "A-113", capacity = 46 },
                new Room { roomId = "014", roomName = "A-114", capacity = 48 },
                new Room { roomId = "015", roomName = "A-115", capacity = 50 },
                new Room { roomId = "016", roomName = "A-116", capacity = 52 },

                // Continue adding rooms as required
            }
        };

        ReservationHandler reservationHandler = new ReservationHandler(roomData, dateData);

        // Predefined reservations can be added here as shown:
       Reservation reservation1 = new Reservation
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            date = DateTime.Now.Date.AddDays(4),
            reserverName = "Okan",
            room = roomData.Rooms[0] 
        };

        Reservation reservation2 = new Reservation
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            date = DateTime.Now.Date.AddDays(3),
            reserverName = "Doğukan",
            room = roomData.Rooms[3] 
        };

        Reservation reservation3 = new Reservation
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            date = DateTime.Now.Date.AddDays(2),
            reserverName = "Aleyna",
            room = roomData.Rooms[0] 
        };

        Reservation reservation4 = new Reservation
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            date = DateTime.Now.Date.AddDays(5),
            reserverName = "Barış",
            room = roomData.Rooms[7] 
        };

        Reservation reservation5 = new Reservation
        {
            time = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 14, 0, 0),
            date = DateTime.Now.Date.AddDays(1),
            reserverName = "Ethem",
            room = roomData.Rooms[9] 
        };


        
        
        
        reservationHandler.AddReservation(reservation1);
        reservationHandler.AddReservation(reservation2);
        reservationHandler.AddReservation(reservation3);
        reservationHandler.AddReservation(reservation4);
        reservationHandler.AddReservation(reservation5);





        

        


        // Display the weekly schedule to see all reservations
reservationHandler.displayWeeklySchedule();

Console.WriteLine("Enter Your Infos for Delete Reservation");
        Console.Write("Room Name: ");
        string roomName = Console.ReadLine();

        Console.Write("Reservation Date (DD.MM.YYYY): ");
        DateTime date;
        while (!DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            Console.WriteLine("Invalid Date.");
        }

        Console.Write("Guest Name: ");
        string reserverName = Console.ReadLine();

        // Rezervasyonu silme denemesi
        bool result = reservationHandler.RemoveReservation(roomName, date, reserverName);
        if (result)
        {
            Console.WriteLine("Your Reservation is Succesfully Removed.");
        }
        else
        {
            Console.WriteLine("Invalid Infos.");
        }
reservationHandler.displayWeeklySchedule();

    }


    
}
