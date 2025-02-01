using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Project___ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project___ConsoleApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Book>Books { get; set; }
        public DbSet<Author>Authors { get; set; }
        public DbSet<Borrower>Borrowers { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<LoanItem> LoanItems { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-K4VIJ8L;Database=Project;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
