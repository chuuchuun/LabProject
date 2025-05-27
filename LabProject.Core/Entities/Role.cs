using System;
using System.ComponentModel.DataAnnotations;

namespace LabProject.Domain.Entities
{
    public class Role : BaseEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        required public string Name { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
