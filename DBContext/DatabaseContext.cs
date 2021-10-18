using System;
using System.Collections.Generic;
using System.Text;
using DBContext.Models;
using Microsoft.EntityFrameworkCore;

namespace DBContext
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Planet> Planets { get; set; }
        public DbSet<Connection> Connections { get; set; }
    }
}
