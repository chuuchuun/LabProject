﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Data;
using LabProject.Domain.Enums;

namespace LabProject.Domain.Entities
{
    public class User : BaseEntity
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(100)]
        required public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        required public string Username { get; set; }
        [Required]
        [MaxLength(20)]
        required public string Phone { get; set; }

        [Required]
        [MaxLength(255)]
        required public string Email { get; set; }

        [Required]
        [MaxLength(255)]
        required public string PasswordHash { get; set; }

        [Required]
        public int RoleId { get; set; }
        public Role? Role { get; set; }


        public virtual ICollection<Appointment>? AppointmentsAsProvider { get; set; }
        public virtual ICollection<User>? FavouritesAsProvider { get; set; }
        public virtual ICollection<Service>? ProviderServices { get; set; }
        public virtual ICollection<Location>? ProviderLocations { get; set; }
        public virtual ICollection<ProviderSpecialty>? ProviderSpecialties { get; set; }


        public virtual ICollection<Appointment>? AppointmentsAsClient { get; set; }
        public virtual ICollection<Review>? Reviews { get; set; }
        public virtual ICollection<Discount>? Discounts { get; set; }
        public virtual ICollection<User>? FavouritesAsClient { get; set; }
    }
}
