using Example_EF_1.EasyIrriot.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Example_EF_1.Shared.Infraestructure.Persistence.Configuration
{
    public class EasyIrriotContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Thing> Things { get; set; }
        public DbSet<ThingState> ThingStates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Thing>(entity =>
            {
                entity.ToTable("Things");
                entity.HasKey(t => t.Id);

                entity.Property(t => t.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(t => t.SerialNumber)
                    .IsRequired()
                    .ValueGeneratedOnAdd();
                
                entity.Property(t => t.Model)
                    .IsRequired();
                
                entity.Property(t => t.OperationMode)
                    .IsRequired();
                
                entity.Property(t => t.MaximumTemperatureThreshold)
                    .IsRequired();
                
                entity.Property(t => t.MinimumHumidityThreshold)
                    .IsRequired();
                
                entity.Property(t => t.CreatedAt)
                    .IsRequired();
                
                entity.Property(t => t.UpdateAt)
                    .IsRequired();
            });

            builder.Entity<ThingState>(entity =>
            {
                entity.ToTable("ThingStates");
                entity.HasKey(t => t.Id);
                
                entity.Property(t => t.Id)
                    .IsRequired()
                    .ValueGeneratedOnAdd();

                entity.Property(t => t.ThingSerialNumber)
                    .IsRequired();
                
                entity.Property(t => t.CurrentOperationMode)
                    .IsRequired();
                
                entity.Property(t => t.CurrentTemperature)
                    .IsRequired();
                
                entity.Property(t => t.CurrentHumidity)
                    .IsRequired();
                
                entity.Property(t => t.CollectedAt)
                    .IsRequired();
                
                entity.Property(t => t.CreatedAt)
                    .IsRequired();
                
                entity.Property(t => t.UpdateAt)
                    .IsRequired();
                
                entity.HasOne(s => s.Thing)
                    .WithMany(t => t.ThingStates)
                    .HasForeignKey(s => s.ThingId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}