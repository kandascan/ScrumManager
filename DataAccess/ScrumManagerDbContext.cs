using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccess
{
    public class ApplicationContextDbFactory : IDesignTimeDbContextFactory<ScrumManagerDbContext>
    {
        ScrumManagerDbContext IDesignTimeDbContextFactory<ScrumManagerDbContext>.CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ScrumManagerDbContext>();
            var connectionString = @"Data Source=DESKTOP-BVKHG2F;Initial Catalog=ScrumManager;Integrated Security=True";
            optionsBuilder.UseSqlServer<ScrumManagerDbContext>(connectionString);
            return new ScrumManagerDbContext(optionsBuilder.Options);
        }
    }
    public class ScrumManagerDbContext : DbContext
    {
        public ScrumManagerDbContext(DbContextOptions<ScrumManagerDbContext> options)
            : base(options){}

        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = @"Data Source=DESKTOP-BVKHG2F;Initial Catalog=ScrumManager;Integrated Security=True";
            builder.UseSqlServer(connectionString);
        }
    }
}
