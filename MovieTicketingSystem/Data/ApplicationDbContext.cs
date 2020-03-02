using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTicketingSystem.Models;

namespace MovieTicketingSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<MovieTicketingSystem.Models.Movie> Movie { get; set; }
        public DbSet<MovieTicketingSystem.Models.Genre> Genre { get; set; }
        public DbSet<MovieTicketingSystem.Models.Rating> Rating { get; set; }
        public DbSet<MovieTicketingSystem.Models.Playing> Playing { get; set; }
        public DbSet<MovieTicketingSystem.Models.Room> Room { get; set; }
        public DbSet<MovieTicketingSystem.Models.Ticket> Ticket { get; set; }
        public DbSet<MovieTicketingSystem.Models.TimeSlot> TimeSlot { get; set; }
    }
}
