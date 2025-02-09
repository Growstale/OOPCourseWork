using CouseWork.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CouseWork.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Organizers> Organizers { get; set; }
        public DbSet<Locations> Locations { get; set; }
        public DbSet<Events> Events { get; set; }
        public DbSet<EventsSchedule> EventsSchedule { get; set; }
        public DbSet<SectorRows> SectorRows { get; set; }
        public DbSet<Tickets> Tickets { get; set; }
        public DbSet<Managers> Managers { get; set; }
        public DbSet<OrganizerQuestions> OrganizerQuestions { get; set; }
        public DbSet<Sales> Sales { get; set; }
        public DbSet<ShoppingCart> ShoppingCart { get; set; }
        public DbSet<TicketRefund> TicketRefund { get; set; }
        public DbSet<UserQuestions> UserQuestions { get; set; }
        public DbSet<Comments> Comments { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categories>()
                .HasMany(c => c.Events)
                .WithOne(e => e.Category)
                .HasForeignKey(e => e.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);


            modelBuilder.Entity<Comments>()
                .HasOne(c => c.User)
                .WithMany(u => u.Comments)
                .HasForeignKey(c => c.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Event)
                .WithMany(e => e.Comments)
                .HasForeignKey(c => c.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Events>()
                .HasMany(e => e.EventSchedules)
                .WithOne(es => es.Event)
                .HasForeignKey(es => es.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Events>()
                .HasMany(e => e.Comments)
                .WithOne(c => c.Event)
                .HasForeignKey(c => c.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Events>()
                .HasOne(e => e.Category)
                .WithMany(c => c.Events)
                .HasForeignKey(e => e.CategoryID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Events>()
                .HasOne(e => e.Organizer)
                .WithMany(o => o.Events)
                .HasForeignKey(e => e.OrganizerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventsSchedule>()
                .HasMany(es => es.Tickets)
                .WithOne(t => t.EventsSchedule)
                .HasForeignKey(t => t.EventScheduleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventsSchedule>()
                .HasOne(es => es.Location)
                .WithMany(l => l.EventSchedules)
                .HasForeignKey(es => es.LocationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<EventsSchedule>()
                .HasOne(es => es.Event)
                .WithMany(e => e.EventSchedules)
                .HasForeignKey(es => es.EventID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Locations>()
                .HasMany(l => l.EventSchedules)
                .WithOne(es => es.Location)
                .HasForeignKey(es => es.LocationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Locations>()
                .HasMany(l => l.SectorRows)
                .WithOne(sr => sr.Location)
                .HasForeignKey(sr => sr.LocationID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Managers>()
                .HasMany(m => m.UserQuestions)
                .WithOne(uq => uq.Manager)
                .HasForeignKey(uq => uq.ManagerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Managers>()
                .HasMany(m => m.OrganizerQuestions)
                .WithOne(oq => oq.Manager)
                .HasForeignKey(oq => oq.ManagerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Organizers>()
                .HasMany(o => o.Events)
                .WithOne(e => e.Organizer)
                .HasForeignKey(e => e.OrganizerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Organizers>()
                .HasMany(o => o.OrganizerQuestions)
                .WithOne(oq => oq.Organizer)
                .HasForeignKey(oq => oq.OrganizerID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Roles>()
                .HasMany(r => r.Users)
                .WithOne(u => u.Role)
                .HasForeignKey(u => u.RoleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Roles>()
                .HasMany(r => r.Organizers)
                .WithOne(o => o.Role)
                .HasForeignKey(o => o.RoleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Roles>()
                .HasMany(r => r.Managers)
                .WithOne(m => m.Role)
                .HasForeignKey(m => m.RoleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sales>()
                .HasOne(s => s.User)
                .WithMany(u => u.Sales)
                .HasForeignKey(s => s.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Ticket)
                .WithMany(t => t.Sales)
                .HasForeignKey(s => s.TicketID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Sales>()
                .HasMany(s => s.TicketRefunds)
                .WithOne(tr => tr.Sale)
                .HasForeignKey(tr => tr.SaleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SectorRows>()
                .HasMany(sr => sr.Tickets)
                .WithOne(t => t.SectorRows)
                .HasForeignKey(t => t.SectorRowID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.User)
                .WithMany(u => u.ShoppingCarts)
                .HasForeignKey(sc => sc.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ShoppingCart>()
                .HasOne(sc => sc.Ticket)
                .WithMany(t => t.ShoppingCarts)
                .HasForeignKey(sc => sc.TicketID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketRefund>()
                .HasOne(tr => tr.Sale)
                .WithMany(s => s.TicketRefunds)
                .HasForeignKey(tr => tr.SaleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TicketRefund>()
                .HasOne(tr => tr.User)
                .WithMany(u => u.TicketRefunds)
                .HasForeignKey(tr => tr.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.EventsSchedule)
                .WithMany(es => es.Tickets)
                .HasForeignKey(t => t.EventScheduleID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Tickets>()
                .HasOne(t => t.SectorRows)
                .WithMany(sr => sr.Tickets)
                .HasForeignKey(t => t.SectorRowID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserQuestions>()
                .HasOne(uq => uq.User)
                .WithMany(u => u.UserQuestions)
                .HasForeignKey(uq => uq.UserID)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserQuestions>()
                .HasOne(uq => uq.Manager)
                .WithMany(m => m.UserQuestions)
                .HasForeignKey(uq => uq.ManagerID)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
