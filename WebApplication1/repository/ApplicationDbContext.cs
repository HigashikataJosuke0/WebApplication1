using Microsoft.EntityFrameworkCore;
using WebApplication1.model;

namespace WebApplication1.repository
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public ApplicationDbContext()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public DbSet<User> User { get; set; }
        public DbSet<Habits> Habits { get; set; }
        public DbSet<RecordOfExecution> Record { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<RecordOfExecution>().ToTable("Record");

            modelBuilder.Entity<Habits>(entity =>
            {
                entity.ToTable("Habits");

                entity.HasDiscriminator<string>("Discriminator")
                    .HasValue<GoodHabits>("GoodHabits")
                    .HasValue<BadHabits>("BadHabits");

                entity.HasKey(h => h.Id);
            });
            
            modelBuilder.Entity<RecordOfExecution>()
                .HasOne(e => e.Habits) 
                .WithMany() 
                .HasForeignKey(e => e.HabitsId) 
                .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}