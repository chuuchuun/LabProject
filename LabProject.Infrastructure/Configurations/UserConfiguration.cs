using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LabProject.Domain.Interfaces;

namespace LabProject.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.Name).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Username).HasMaxLength(100).IsRequired();
            builder.Property(u => u.Phone).HasMaxLength(20).IsRequired();
            builder.Property(u => u.Email).HasMaxLength(255).IsRequired();
            builder.Property(u => u.PasswordHash).HasMaxLength(255).IsRequired();

            // Role
            builder.HasOne(u => u.Role)
                .WithMany()
                .HasForeignKey(u => u.RoleId);

            // Favorites (many-to-many between Clients and Providers)
            builder.HasMany(u => u.FavouritesAsClient)
                .WithMany(p => p.FavouritesAsProvider)
                .UsingEntity<Dictionary<string, object>>(
                    "Favourites",
                    j => j.HasOne<User>().WithMany().HasForeignKey("ProviderId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("ClientId"),
                    j => j.HasKey("ClientId", "ProviderId"));

            // Appointments as Client
            builder.HasMany(u => u.AppointmentsAsClient)
                .WithOne(a => a.Client)
                .HasForeignKey(a => a.ClientId)
                .OnDelete(DeleteBehavior.Restrict);

            // Appointments as Provider
            builder.HasMany(u => u.AppointmentsAsProvider)
                .WithOne(a => a.Provider)
                .HasForeignKey(a => a.ProviderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Reviews
            builder.HasMany(u => u.Reviews)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId);

            //Discounts
            builder.HasMany(u => u.Discounts)
                .WithOne(d => d.Client)
                .HasForeignKey(d => d.ClientId);

            //Provider Services (many-to-many)
            builder.HasMany(u => u.ProviderServices)
                .WithMany(s => s.Providers)
                .UsingEntity<Dictionary<string, object>>(
                    "ProviderServices",
                    j => j.HasOne<Service>().WithMany().HasForeignKey("ServiceId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("ProviderId"));

            //Provider Locations (many-to-many)
            builder.HasMany(u => u.ProviderLocations)
                .WithMany(l => l.Providers)
                .UsingEntity<Dictionary<string, object>>(
                    "ProviderLocations",
                    j => j.HasOne<Location>().WithMany().HasForeignKey("LocationId"),
                    j => j.HasOne<User>().WithMany().HasForeignKey("ProviderId"));

        }
    }
}
