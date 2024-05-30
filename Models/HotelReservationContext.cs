using Microsoft.EntityFrameworkCore;
using HotelReservationSystem.Models;

namespace HotelReservationSystem.Data
{
    public class HotelReservationContext : DbContext
    {
        public HotelReservationContext(DbContextOptions<HotelReservationContext> options) : base(options) { }

        public DbSet<HotelReservationSystem.Models.Room> Rooms { get; set; }  // Tam adı kullanarak güncellendi
        public DbSet<HotelReservationSystem.Models.Reservation> Reservations { get; set; }  // Tam adı kullanarak güncellendi
        public DbSet<HotelReservationSystem.Models.User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelReservationSystem.Models.Room>().ToTable("rooms");  // Tam adı kullanarak güncellendi
            modelBuilder.Entity<HotelReservationSystem.Models.Reservation>().ToTable("reservations");  // Tam adı kullanarak güncellendi
            modelBuilder.Entity<HotelReservationSystem.Models.User>().ToTable("users");  // Tam adı kullanarak güncellendi

            modelBuilder.Entity<HotelReservationSystem.Models.Room>(entity =>  // Tam adı kullanarak güncellendi
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Name).HasColumnName("name");
                entity.Property(e => e.Capacity).HasColumnName("capacity");
                entity.Property(e => e.View).HasColumnName("view");
            });

            modelBuilder.Entity<HotelReservationSystem.Models.Reservation>(entity =>  // Tam adı kullanarak güncellendi
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.RoomId).HasColumnName("roomid");
                entity.Property(e => e.UserName).HasColumnName("username");
                entity.Property(e => e.StartDate).HasColumnName("startdate");
                entity.Property(e => e.EndDate).HasColumnName("enddate");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Reservations)
                    .HasForeignKey(d => d.RoomId);
            });

            modelBuilder.Entity<HotelReservationSystem.Models.User>(entity =>  // Tam adı kullanarak güncellendi
            {
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserName).HasColumnName("username");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Gender).HasColumnName("gender");
                entity.Property(e => e.Age).HasColumnName("age");
            });
        }
    }
}
