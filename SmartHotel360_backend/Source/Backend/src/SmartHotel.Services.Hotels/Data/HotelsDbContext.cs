using Microsoft.EntityFrameworkCore;
using SmartHotel.Services.Hotels.Domain;
using SmartHotel.Services.Hotels.Domain.Hotel;
using SmartHotel.Services.Hotels.Domain.RoomService;
using SmartHotel.Services.Hotels.Domain.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHotel.Services.Hotels.Data
{
    public class HotelsDbContext : DbContext
    {
        public HotelsDbContext(DbContextOptions options) : base(options)
        {
        }


        public DbSet<HotelService> HotelServices { get; set; }
        public DbSet<RoomService> RoomServices { get; set; }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Menu> Menus { get; set; } 
        public DbSet<Category> Categories { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ServiceTask> ServiceTasks { get; set; }
        public DbSet<ServiceTaskType> ServiceTaskTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Hotel>().Property(h => h.Id)
                .ForSqlServerUseSequenceHiLo("hotelseq");
            modelBuilder.Entity<Hotel>().OwnsOne(h => h.Address);
            modelBuilder.Entity<Hotel>().OwnsOne(h => h.Location);
            modelBuilder.Entity<Hotel>().Property(h => h.CheckinTime).HasColumnType("time");
            modelBuilder.Entity<Hotel>().Property(h => h.CheckoutTime).HasColumnType("time");

            modelBuilder.Entity<HotelService>().Property(s => s.Id).ValueGeneratedNever();
            modelBuilder.Entity<HotelService>().Property(s => s.Name).HasMaxLength(32);

            modelBuilder.Entity<ServicePerHotel>().HasKey(sph => new { sph.HotelId, sph.ServiceId });

            modelBuilder.Entity<RoomService>().Property(s => s.Id).ValueGeneratedNever();
            modelBuilder.Entity<RoomService>().Property(s => s.Name).HasMaxLength(32);

            modelBuilder.Entity<ServicePerRoom>().HasKey(sph => new { sph.RoomTypeId, sph.ServiceId });

            modelBuilder.Entity<City>().Property(c => c.Id).ValueGeneratedNever();

            modelBuilder.Entity<Menu>().Property(m => m.Id).ValueGeneratedNever();
            modelBuilder.Entity<Menu>().HasKey(m => m.Id);

            modelBuilder.Entity<Category>().Property(c => c.Id).ValueGeneratedNever();
            modelBuilder.Entity<Category>().HasKey(c => c.Id);

            modelBuilder.Entity<OrderItem>().Property(o => o.Id).ValueGeneratedNever();
            modelBuilder.Entity<OrderItem>().HasKey(o => o.Id);

            modelBuilder.Entity<ServiceTask>().Property(s => s.Id).ValueGeneratedNever();
            modelBuilder.Entity<ServiceTask>().HasKey(s => s.Id);

            modelBuilder.Entity<ServiceTaskType>().Property(s => s.Id).ValueGeneratedNever();
            modelBuilder.Entity<ServiceTaskType>().HasKey(s => s.Id);
        }
    }
}
