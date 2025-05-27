using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LabProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LabProject.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.HasKey(a => a.Id);

            // Properties
            builder.Property(a => a.DateTime).IsRequired();
            builder.Property(a => a.Status).IsRequired();

            // Client
            builder.HasOne(a => a.Client)
                .WithMany(c => c.AppointmentsAsClient)
                .HasForeignKey(a => a.ClientId);

            // Provider
            builder.HasOne(a => a.Provider)
                .WithMany(p => p.AppointmentsAsProvider)
                .HasForeignKey(a => a.ProviderId);

            // Service
            builder.HasOne(a => a.Service)
                .WithMany(s => s.Appointments)
                .HasForeignKey(a => a.ServiceId);

            // Location
            builder.HasOne(a => a.Location)
                .WithMany(l => l.Appointments)
                .HasForeignKey(a => a.LocationId);

            // Reviews
            builder.HasMany(a => a.Reviews)
                .WithOne(r => r.Appointment)
                .HasForeignKey(r => r.AppointmentId);
        }
    }
}
