using Microsoft.EntityFrameworkCore;

namespace API_GOPR_SERVICE.Models
{
    public partial class GoprServiceContext : DbContext
    {
        public GoprServiceContext()
        {
        }

        public GoprServiceContext(DbContextOptions<GoprServiceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Client> Clients { get; set; } = null!;
        public virtual DbSet<ClientsDevice> ClientsDevices { get; set; } = null!;
        public virtual DbSet<Device> Devices { get; set; } = null!;
        public virtual DbSet<Tokens> Token { get; set; } = null!;
        public virtual DbSet<Notifications> Notifications { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(entity =>
            {
                entity.ToTable("clients");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email");

                entity.Property(e => e.Name)
                    .HasMaxLength(200)
                    .HasColumnName("name");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(50)
                    .HasColumnName("phoneNumber");
            });

            modelBuilder.Entity<ClientsDevice>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.ClientId).HasColumnName("clientID");

                entity.Property(e => e.DeviceId).HasColumnName("deviceID");

                entity.Property(e => e.NotificationId).HasColumnName("notificationID");

                entity.HasOne(d => d.Client)
                    .WithMany(p => p.ClientsDevices)
                    .HasForeignKey(d => d.ClientId)
                    .HasConstraintName("FK_ClientsDevices_clients");

                entity.HasOne(d => d.Device)
                    .WithMany(p => p.ClientsDevices)
                    .HasForeignKey(d => d.DeviceId)
                    .HasConstraintName("FK_ClientsDevices_devices");

                entity.HasOne(d => d.Notification)
                    .WithMany(p => p.ClientsDevices)
                    .HasForeignKey(d => d.NotificationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ClientsDevices_Notifications");
            });

            modelBuilder.Entity<Device>(entity =>
            {
                entity.ToTable("devices");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateToAdd)
                    .HasColumnType("datetime")
                    .HasColumnName("dateToAdd");
                entity.Property(e => e.DateToReturn)
                    .HasColumnType("datetime")
                    .HasColumnName("dateToReturn");

                entity.Property(e => e.DeviceName)
                    .HasMaxLength(500)
                    .HasColumnName("deviceName");

                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Tokens>(entity =>
            {
                entity.ToTable("tokens");
                entity.Property(e => e.id).HasColumnName("id");
                entity.Property(e => e.token)
                .HasMaxLength(1000)
                .HasColumnName("token");
                entity.Property(e => e.phoneNumber)
                .HasMaxLength(50)
                .HasColumnName("phoneNumber");

            });

            modelBuilder.Entity<Notifications>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Body)
                    .HasMaxLength(500)
                    .HasColumnName("body");

                entity.Property(e => e.DateToAdd)
                    .HasColumnType("datetime")
                    .HasColumnName("dateToAdd");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.Title)
                    .HasMaxLength(500)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<Admins> Admins { get; set; }
    }
}
