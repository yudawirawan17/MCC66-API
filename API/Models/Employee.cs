using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Employee
    {
        [Key][Required]
        public string NIK { get; set; }
        [Required]
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public int? Salary { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public bool? IsDeleted { get; set; }
        public Account Account { get; set; }
        
    }

    public enum Gender
    {
        Male,
        Female
    }

}
