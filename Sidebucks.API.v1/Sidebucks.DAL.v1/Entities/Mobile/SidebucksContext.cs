using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Sidebucks.DAL.v1.Entities.Mobile
{
    public partial class SidebucksContext : DbContext
    {
        public SidebucksContext()
        {
        }

        public SidebucksContext(DbContextOptions<SidebucksContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserSessions> AspNetUserSessions { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Files> Files { get; set; }
        public virtual DbSet<Location> Location { get; set; }
        public virtual DbSet<Skills> Skills { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=sidebucks.cjy4coyz5nhi.ap-southeast-1.rds.amazonaws.com;Initial Catalog=sidebucks;Persist Security Info=True;User ID=admin;Password=NIterbARmica");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });

                entity.HasIndex(e => e.RoleId);

                entity.HasIndex(e => e.UserId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserSessions>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AccessToken).HasMaxLength(450);

                entity.Property(e => e.ApiVersion).HasMaxLength(50);

                entity.Property(e => e.AppVersion).HasMaxLength(50);

                entity.Property(e => e.DeviceId).HasMaxLength(450);

                entity.Property(e => e.DeviceName).HasMaxLength(450);

                entity.Property(e => e.UserId).HasMaxLength(450);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Fullname).HasMaxLength(256);

                entity.Property(e => e.MobileNumber).HasMaxLength(50);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Files>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.CodeName).HasMaxLength(250);

                entity.Property(e => e.Extension).HasMaxLength(250);

                entity.Property(e => e.FeedId).HasMaxLength(250);

                entity.Property(e => e.FilePath).HasMaxLength(250);

                entity.Property(e => e.Name).HasMaxLength(250);

                entity.Property(e => e.UserId).HasMaxLength(250);
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(250)
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CityName).HasMaxLength(250);

                entity.Property(e => e.CountryName).HasMaxLength(250);

                entity.Property(e => e.IpAddress).HasMaxLength(250);

                entity.Property(e => e.Latitude).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Longitude).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.MacAddress).HasMaxLength(250);

                entity.Property(e => e.PostalCode).HasMaxLength(250);

                entity.Property(e => e.State).HasMaxLength(250);

                entity.Property(e => e.UserId).HasMaxLength(250);
            });

            modelBuilder.Entity<Skills>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Skill).HasMaxLength(450);
            });
        }
    }
}
