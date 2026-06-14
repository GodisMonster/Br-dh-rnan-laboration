using Brädhörnan_laboration.Models;
using Microsoft.EntityFrameworkCore;

namespace Brädhörnan_laboration.Data
{
    public class AppDbContext: DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<GameMeeting> GameMeetings { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(
       @"Server=(localdb)\mssqllocaldb;Database=BrädhörnanDb;Trusted_Connection=True;TrustServerCertificate=True;",
       sqlOptions => sqlOptions.EnableRetryOnFailure());

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Game>()
                .HasKey(g => g.GameId);
            modelBuilder.Entity<Game>()
                .Property(g => g.GameId)
                .ValueGeneratedNever();

            modelBuilder.Entity<Member>()
                .HasKey(m => m.MemberNumber);
            modelBuilder.Entity<Member>()
                .Property(m => m.MemberNumber)
                .ValueGeneratedNever();

            modelBuilder.Entity<GameMeeting>()
                .HasKey(gm => gm.GameMeetingId);
            modelBuilder.Entity<GameMeeting>()
                .Property(gm => gm.GameMeetingId)
                .ValueGeneratedNever();
        }

    }
}
