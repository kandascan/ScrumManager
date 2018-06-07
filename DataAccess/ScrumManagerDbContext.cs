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
            : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<TeamEntity> Teams { get; set; }
        public DbSet<XrefUserTeamEntity> XregUserTeam { get; set; }
        public DbSet<RolesEntity> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            var connectionString = @"Data Source=DESKTOP-BVKHG2F;Initial Catalog=ScrumManager;Integrated Security=True";
            builder.UseSqlServer(connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(ConfigureUserEntity);
            modelBuilder.Entity<TeamEntity>(ConfigureTeamEntity);
            modelBuilder.Entity<XrefUserTeamEntity>(ConfigureXrefUserTeamEntity);
            modelBuilder.Entity<RolesEntity>(ConfigureRoleEntity);
        }

        private void ConfigureRoleEntity(EntityTypeBuilder<RolesEntity> entity)
        {
            entity.ToTable("Role");
            entity.HasKey(e => e.RoleId);
            entity.HasIndex(e => e.RoleName).IsUnique();
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureXrefUserTeamEntity(EntityTypeBuilder<XrefUserTeamEntity> entity)
        {
            entity.ToTable("XrefUserTeam");
            entity.HasKey(e => e.Id);
            entity.HasOne(p => p.Team)
                .WithMany(b => b.XrefUsersTeam)
                .HasForeignKey(p => p.TeamId);
            entity.HasOne(p => p.User)
                .WithMany(b => b.XrefUsersTeam)
                .HasForeignKey(p => p.UserId);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureTeamEntity(EntityTypeBuilder<TeamEntity> entity)
        {
            entity.ToTable("Team");
            entity.HasKey(e => e.TeamId);
            entity.HasIndex(p => p.TeamName).IsUnique();
            entity.Property(e => e.TeamName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
        }

        private void ConfigureUserEntity(EntityTypeBuilder<UserEntity> entity)
        {
            entity.ToTable("User");
            entity.HasKey(e => e.UserId);
            entity.HasIndex(p => p.Username).IsUnique();
            entity.HasIndex(p => p.Email).IsUnique();
            entity.Property(e => e.Username).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Password).IsRequired();
            entity.Property(e => e.FirstName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.LastName).IsRequired().HasMaxLength(50);
            entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
            entity.Property(e => e.CreatedDate).HasDefaultValueSql("GETDATE()");
            entity.Property(e => e.Active).IsRequired().HasDefaultValue(1);
            entity.HasOne(p => p.Role)
                .WithMany(b => b.Users)
                .HasForeignKey(p => p.RoleId);
        }
    }
}
