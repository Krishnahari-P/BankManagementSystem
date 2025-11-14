using BankManagementSystem.Entity.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;

namespace BankManagementSystem.Entity.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Customer> CustomerSet { get; set; }
        public DbSet<Employee> EmployeeSet { get; set; }
        public DbSet<AccountType> AccountTypeSet { get; set; }
        public DbSet<Account> AccountSet { get; set; }
        public DbSet<Transaction> TransactionSet { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(c => c.AadharNumber)
                .IsUnique();

                entity.HasIndex(c => c.PAN)
               .IsUnique()
               .HasFilter("[PAN] IS NOT NULL");

                entity.HasIndex(c => c.Phone)
                .IsUnique();

                entity.HasOne(c => c.ApplicationUserSet)
                .WithOne(u => u.CustomerSet)
                .HasForeignKey<Customer>(c => c.ApplicationUserID)
                .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(c => c.ApprovedByUserSet)
                .WithMany(u => u.ApprovedCustomers)
                .HasForeignKey(c => c.ApprovedByUserId)
                .OnDelete(DeleteBehavior.Restrict);

                entity.Property(c => c.ApprovalDate)
                .HasDefaultValueSql(null);

                modelBuilder.Entity<ApplicationUser>()
                .Property(u => u.Status)
                .HasDefaultValue("Pending");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasIndex(e => e.StaffCode)
                    .IsUnique();

                entity.HasOne(e => e.ApplicationUserSet)
                    .WithOne(u => u.EmployeeSet)
                    .HasForeignKey<Employee>(e => e.ApplicationUserID)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(e => e.HiredDate)
                    .HasDefaultValueSql("CAST(GETDATE() AS DATE)");
            });

            modelBuilder.Entity<Account>(entity =>
            {
                entity.Property(a => a.AccountNumber)
                .IsRequired(false);

                entity.HasIndex(a => a.AccountNumber)
                    .IsUnique();

                entity.HasOne(a => a.CustomerSet)
                    .WithMany(c => c.Accounts)
                    .HasForeignKey(a => a.CustomerId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(a => a.AccountTypeSet)
                    .WithMany(at => at.Accounts)
                    .HasForeignKey(a => a.AccountTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(a => a.CreatedDate)
                    .HasDefaultValueSql("CAST(GETDATE() AS DATE)");

                entity.Property(a => a.Status)
                    .HasDefaultValue("Pending");
            });

            modelBuilder.Entity<AccountType>(entity =>
            {
                entity.HasIndex(at => at.TypeName)
                    .IsUnique();

                entity.Property(at => at.InterestRate)
                    .HasDefaultValue(0);
            });
            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasOne(t => t.AccountSet)
                    .WithMany(a => a.InitiatedTransactions)
                    .HasForeignKey(t => t.AccountId)
                    .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(t => t.RecipientAccountSet)
                    .WithMany(a => a.ReceivedTransactions)
                    .HasForeignKey(t => t.RecipientAccountId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(t => t.ProcessedByUserSet)
                    .WithMany(u => u.ProcessedTransactions)
                    .HasForeignKey(t => t.ProcessedByUserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.Property(t => t.TransactionDate)
                    .HasDefaultValueSql("CAST(GETDATE() AS DATE)");

                entity.Property(a => a.Status)
                    .HasDefaultValue("Pending");
            });

            ApplicationUser applicationUser = new ApplicationUser
            {
                Id = "2b9d215a-65b4-44b7-872e-a5780fb66fd6",
                UserName = "admin@email.com",
                NormalizedUserName = "ADMIN@EMAIL.COM",
                Email = "admin@email.com",
                NormalizedEmail = "ADMIN@EMAIL.COM",
                IsActive = true,
                Status="Approved"
            };
            String password = "Admin@123";
            PasswordHasher<ApplicationUser> haser = new PasswordHasher<ApplicationUser>();
            applicationUser.PasswordHash = haser.HashPassword(applicationUser, password);
            modelBuilder.Entity<ApplicationUser>().HasData(applicationUser);

            IdentityRole identityRole = new IdentityRole
            {
                Id = "46b36bf0-15ee-4631-aa11-8a7007aca77c",
                Name = "Admin",
                NormalizedName = "ADMIN",
            };
            modelBuilder.Entity<IdentityRole>().HasData(identityRole);

            IdentityUserRole<String> identityUserRole = new IdentityUserRole<String>();
            identityUserRole.UserId = "2b9d215a-65b4-44b7-872e-a5780fb66fd6";
            identityUserRole.RoleId = "46b36bf0-15ee-4631-aa11-8a7007aca77c";
            modelBuilder.Entity<IdentityUserRole<String>>().HasData(identityUserRole);
            //Customer

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {

                Id = "2f76f0c3-4bfa-4e60-aca6-82c3cd7d22bb",

                Name = "Customer",

                NormalizedName = "CUSTOMER"

            });

            //Employee

            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole

            {

                Id = "68f74df6-eacf-4995-a00e-24238b575ab0",

                Name = "Manager",

                NormalizedName = "MANAGER"

            });

            base.OnModelCreating(modelBuilder);

        }
    }
}
