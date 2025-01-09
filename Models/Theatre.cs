using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public List<Reservation>? Reservations { get; set; }
    }

    public class Reservation
    {
        public int ReservationId { get; set; }

        public int AmountOfTickets { get; set; }

        public bool Used { get; set; }

        public Customer? Customer { get; set; }

        public TheatreShowDate? TheatreShowDate { get; set; }
    }

    public class TheatreShowDate
    {
        public int TheatreShowDateId { get; set; }

        public DateTime DateAndTime { get; set; } //"MM-dd-yyyy HH:mm"

        public List<Reservation>? Reservations { get; set; }

        public TheatreShow? TheatreShow { get; set; }

    }

    public class TheatreShow
    {
        public int TheatreShowId { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; }

        public double Price { get; set; }

        public List<TheatreShowDate>? theatreShowDates { get; set; }

        public Venue? Venue { get; set; }

    }

    public class Venue
    {
        public int VenueId { get; set; }

        public string? Name { get; set; }

        public int Capacity { get; set; }

        public List<TheatreShow>? TheatreShows { get; set; }
    }
}


//     public class DatabaseContext : DbContext
//     {
//         // The admin table will be used in both cases
//         public DbSet<Admin> Admin { get; set; }

//         // You can comment out or remove the case you are not going to use.

//         // Tables for the Theatre ticket case

//         // public DbSet<Customer> Customer { get; set; }
//         // public DbSet<Reservation> Reservation { get; set; }
//         // public DbSet<TheatreShowDate> TheatreShowDate { get; set; }
//         // public DbSet<TheatreShow> TheatreShow { get; set; }
//         // public DbSet<Venue> Venue { get; set; }
//         public DbSet<Customer> Customer { get; set; }
//         public DbSet<Reservation> Reservation { get; set; }
//         public DbSet<TheatreShowDate> TheatreShowDate { get; set; }
//         public DbSet<TheatreShow> TheatreShow { get; set; }
//         public DbSet<Venue> Venue { get; set; }

//         // Tables for the event calendar case

//         // public DbSet<User> User { get; set; }
//         // public DbSet<Attendance> Attendance { get; set; }
//         // public DbSet<Event_Attendance> Event_Attendance { get; set; }
//         // public DbSet<Event> Event { get; set; }



//         public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
//         {

//         }

//         protected override void OnModelCreating(ModelBuilder modelBuilder)
//         {
//             modelBuilder.Entity<Admin>()
//                 .HasIndex(p => p.UserName).IsUnique();

//             modelBuilder.Entity<Admin>()
//                 .HasData(new Admin { AdminId = 1, Email = "admin1@example.com", UserName = "admin1", Password = ("password") });
//             modelBuilder.Entity<Admin>()
//                 .HasData(new Admin { AdminId = 2, Email = "admin2@example.com", UserName = "admin2", Password = ("tooeasytooguess") });
//             modelBuilder.Entity<Admin>()
//                 .HasData(new Admin { AdminId = 3, Email = "admin3@example.com", UserName = "admin3", Password = ("helloworld") });
//             modelBuilder.Entity<Admin>()
//                 .HasData(new Admin { AdminId = 4, Email = "admin4@example.com", UserName = "admin4", Password = ("Welcome123") });
//             modelBuilder.Entity<Admin>()
//                 .HasData(new Admin { AdminId = 5, Email = "admin5@example.com", UserName = "admin5", Password = ("Whatisapassword?") });
//         }

//     }


// }