using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HomeLibrary.Model;

namespace HomeLibrary
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() : base("DefaultConnection")
        {
        }
        public DbSet<ReadBook> ReadBooks { get; set; }
        public DbSet<CurrentBook> CurrentBooks { get; set; }
        public DbSet<PlannedBook> PlannedBooks { get; set; }
        public DbSet<PagesPerDay> PagesPerDays { get; set; }
    }
}
