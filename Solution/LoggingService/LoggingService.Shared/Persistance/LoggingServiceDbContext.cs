using LoggingService.Domain.Entities;
using LoggingService.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;

namespace LoggingService.Infrastructure.Persistance
{
    public class LoggingServiceDbContext : DbContext
    {
        public LoggingServiceDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<LoggedApplication> application { get; set; }
        public DbSet<LoggedMessage> logmessage { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoggedApplication>()
                        .HasKey(x => x.Id);

            modelBuilder.Entity<LoggedApplication>()
                        .HasIndex(x => x.Name)
                        .IsUnique();

            modelBuilder.Entity<LoggedMessage>()
                        .HasOne<LoggedApplication>(x => x.LoggedApplication)
                        .WithMany(x => x.LoggedMessages)
                        .HasForeignKey(x => x.ApplicationId);
            
            modelBuilder.Entity<LoggedMessage>()
                        .Property(e => e.LogLevel)
                        .HasConversion(
                            v => v.ToString(),
                            v => (LogLevel)Enum.Parse(typeof(LogLevel), v));
        }
    }
}
