using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Account
    {
        [Key][ForeignKey("Employee")]
        public string NIK { get; set; }
        [Required]
        public string Password { get; set; }
        public string? OTPCode { get; set; }
        public bool? OTPStatus { get; set; }
        public DateTime? OTPExpiredAt { get; set; }
        public Employee Employee { get; set; }
        public Profiling Profiling { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }
    }
}
