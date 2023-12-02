using ConsoleAppDemo.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppDemo
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=EFCoreConsoleAppDemoDB;" +
                "User=sa;Password=sa;Trusted_Connection=True;TrustServerCertificate=True");
        }

        public DbSet<Person> People { get; set; }
    }
}
