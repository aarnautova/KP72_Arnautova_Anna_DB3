using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace KP72_ArnautovaAnna_DB3
{
    public partial class HotelContext : DbContext
    {
        public HotelContext()
        {
        }

        public HotelContext(DbContextOptions<HotelContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Booking> Booking { get; set; }
        public virtual DbSet<Guest> Guest { get; set; }
        public virtual DbSet<GuestStatus> GuestStatus { get; set; }
        public virtual DbSet<Room> Room { get; set; }
        public virtual DbSet<RoomType> RoomType { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseNpgsql("Server=127.0.0.1; Port=5432; User Id=postgres; Password=123; Database=Hotel;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Arrival)
                    .HasColumnName("arrival")
                    .HasColumnType("date");

                entity.Property(e => e.Departure)
                    .HasColumnName("departure")
                    .HasColumnType("date");

                entity.Property(e => e.GuestId)
                    .HasColumnName("guest_id")
                    .HasDefaultValueSql("nextval('\"Booking_GuestId_seq\"'::regclass)");

                entity.Property(e => e.RoomId)
                    .HasColumnName("room_id")
                    .HasDefaultValueSql("nextval('\"Booking_RoomId_seq\"'::regclass)");

                entity.HasOne(d => d.Guest)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.GuestId)
                    .HasConstraintName("booking_guest_id_fkey");

                entity.HasOne(d => d.Room)
                    .WithMany(p => p.Booking)
                    .HasForeignKey(d => d.RoomId)
                    .HasConstraintName("booking_room_id_fkey");
            });

            modelBuilder.Entity<Guest>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Guests_id_seq\"'::regclass)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name");

                entity.Property(e => e.StatusId)
                    .HasColumnName("status_id")
                    .HasDefaultValueSql("nextval('\"Guests_StatusId_seq\"'::regclass)");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Guest)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("guest_status_id_fkey");
            });

            modelBuilder.Entity<GuestStatus>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.RequiredVisits).HasColumnName("required_visits");

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasColumnName("status_name");
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasDefaultValueSql("nextval('\"Rooms_id_seq\"'::regclass)");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number");

                entity.Property(e => e.OceanView).HasColumnName("ocean_view");

                entity.Property(e => e.TypeId)
                    .HasColumnName("type_id")
                    .HasDefaultValueSql("nextval('\"Rooms_TypeId_seq\"'::regclass)");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Room)
                    .HasForeignKey(d => d.TypeId)
                    .HasConstraintName("room_type_id_fkey");
            });

            modelBuilder.Entity<RoomType>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BedCount).HasColumnName("bed_count");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasColumnType("money");

                entity.Property(e => e.TypeName)
                    .IsRequired()
                    .HasColumnName("type_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
