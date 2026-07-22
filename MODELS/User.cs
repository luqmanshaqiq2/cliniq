using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cliniq.MODELS
{
    public class User
    {
        [Key]
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required, StringLength(255)]
        public string PasswordHash { get; set; } = string.Empty;
        public enum UserRole {Patient,Doctor,Admin}
        public UserRole Role { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // Navigation (One-to-One)
        public Patient? Patient { get; set; }
        public Doctor? Doctor { get; set; }


    }
}