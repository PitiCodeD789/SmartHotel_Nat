﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using SmartHotel.Services.Hotels.Data;
using System;

namespace SmartHotel.Services.Hotels.Migrations
{
    [DbContext(typeof(HotelsDbContext))]
    partial class HotelsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("Relational:Sequence:.hotelseq", "'hotelseq', '', '1', '10', '', '', 'Int64', 'False'")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.City", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Country");

                    b.Property<double>("Latitude");

                    b.Property<double>("Longitude");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.ConferenceRoom", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<int>("HotelId");

                    b.Property<string>("Name");

                    b.Property<int>("NumPhotos");

                    b.Property<int>("PricePerHour");

                    b.Property<int>("Rating");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("ConferenceRoom");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.Hotel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:HiLoSequenceName", "hotelseq")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.SequenceHiLo);

                    b.Property<TimeSpan>("CheckinTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("CheckoutTime")
                        .HasColumnType("time");

                    b.Property<int?>("CityId");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<int>("NumPhotos");

                    b.Property<int>("Rating");

                    b.Property<int>("Visits");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.HotelService", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("HotelServices");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.Menu", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("CategoryId");

                    b.Property<string>("Description");

                    b.Property<int>("HotelId");

                    b.Property<bool>("IsAvailable");

                    b.Property<bool>("IsRecommended");

                    b.Property<string>("Item");

                    b.Property<decimal>("Price");

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.RoomService", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("Name")
                        .HasMaxLength(32);

                    b.HasKey("Id");

                    b.ToTable("RoomServices");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.ServicePerHotel", b =>
                {
                    b.Property<int>("HotelId");

                    b.Property<int>("ServiceId");

                    b.HasKey("HotelId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServicePerHotel");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.ServicePerRoom", b =>
                {
                    b.Property<int>("RoomTypeId");

                    b.Property<int>("ServiceId");

                    b.HasKey("RoomTypeId", "ServiceId");

                    b.HasIndex("ServiceId");

                    b.ToTable("ServicePerRoom");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Review.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Date");

                    b.Property<int>("HotelId");

                    b.Property<string>("Message");

                    b.Property<int>("Rating");

                    b.Property<string>("RoomType");

                    b.Property<int>("UserId");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.RoomService.Category", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("CategoryName");

                    b.Property<int>("HotelId");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.RoomService.OrderItem", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("OrderItemAmount");

                    b.Property<string>("OrderItemDescription");

                    b.Property<int>("OrderItemId");

                    b.Property<int>("ServiceTaskId");

                    b.HasKey("Id");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.RoomService.ServiceTask", b =>
                {
                    b.Property<int>("Id");

                    b.Property<int>("BookingId");

                    b.Property<int>("OrderItemId");

                    b.Property<int>("ServiceTaskType");

                    b.HasKey("Id");

                    b.ToTable("ServiceTasks");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.RoomService.ServiceTaskType", b =>
                {
                    b.Property<int>("Id");

                    b.Property<string>("TaskType");

                    b.HasKey("Id");

                    b.ToTable("ServiceTaskTypes");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Capacity");

                    b.Property<string>("Description");

                    b.Property<int>("DoubleBeds");

                    b.Property<int>("HotelId");

                    b.Property<string>("Name");

                    b.Property<int>("NumPhotos");

                    b.Property<int>("Price");

                    b.Property<int>("SingleBeds");

                    b.Property<int>("TwinBeds");

                    b.HasKey("Id");

                    b.HasIndex("HotelId");

                    b.ToTable("RoomType");
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.ConferenceRoom", b =>
                {
                    b.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.Hotel")
                        .WithMany("ConferenceRooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.Hotel", b =>
                {
                    b.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId");

                    b.OwnsOne("SmartHotel.Services.Hotels.Domain.Hotel.Address", "Address", b1 =>
                        {
                            b1.Property<int>("HotelId");

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.Property<string>("PostCode");

                            b1.Property<string>("Street");

                            b1.ToTable("Hotels");

                            b1.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.Hotel")
                                .WithOne("Address")
                                .HasForeignKey("SmartHotel.Services.Hotels.Domain.Hotel.Address", "HotelId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });

                    b.OwnsOne("SmartHotel.Services.Hotels.Domain.Hotel.Location", "Location", b1 =>
                        {
                            b1.Property<int>("HotelId");

                            b1.Property<double>("Latitude");

                            b1.Property<double>("Longitude");

                            b1.ToTable("Hotels");

                            b1.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.Hotel")
                                .WithOne("Location")
                                .HasForeignKey("SmartHotel.Services.Hotels.Domain.Hotel.Location", "HotelId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.ServicePerHotel", b =>
                {
                    b.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.Hotel", "Hotel")
                        .WithMany("Services")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.HotelService", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.Hotel.ServicePerRoom", b =>
                {
                    b.HasOne("SmartHotel.Services.Hotels.Domain.RoomType", "RoomType")
                        .WithMany("Services")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.RoomService", "Service")
                        .WithMany()
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SmartHotel.Services.Hotels.Domain.RoomType", b =>
                {
                    b.HasOne("SmartHotel.Services.Hotels.Domain.Hotel.Hotel")
                        .WithMany("RoomTypes")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
