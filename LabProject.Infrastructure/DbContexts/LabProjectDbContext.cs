using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Enums;
using LabProject.Domain.Entities;
using LabProject.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LabProject.Infrastructure.DbContexts
{
        public class LabProjectDbContext(DbContextOptions<LabProjectDbContext> options) : DbContext(options)
        {
        public DbSet<User> Users { get; set; } = null!;
            public DbSet<Role> Roles { get; set; } = null!;
            public DbSet<Appointment> Appointments { get; set; } = null!;
            public DbSet<Service> Services { get; set; } = null!;
            public DbSet<Location> Locations { get; set; } = null!;
            public DbSet<Discount> Discounts { get; set; } = null!;
            public DbSet<Review> Reviews { get; set; } = null!;
            public DbSet<PaymentDiscount> PaymentsDiscounts { get; set; } = null!;
            public DbSet<Payment> Payments { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AppointmentConfiguration());
        }
        }
}
