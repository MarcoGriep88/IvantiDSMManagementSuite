using Microsoft.EntityFrameworkCore;
using Packless.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Packless.Database
{
    public class PacklessDbContext : DbContext
    {
        public PacklessDbContext(DbContextOptions<PacklessDbContext> options):base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<PatchData> PatchDataCollection { get; set; }
    }
}
