using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

        public DbSet<UserEntity> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = @"Data Source=DESKTOP-BVKHG2F;Initial Catalog=ScrumManager;Integrated Security=True";
            builder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(ConfigureUserEntity);
        }

        private void ConfigureUserEntity(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserId);
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Active).IsRequired().HasDefaultValue(1);
        }
    }
}
