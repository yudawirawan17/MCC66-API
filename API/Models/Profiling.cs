using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Profiling
    {
        [Key][ForeignKey("Account")]
        public string NIK { get; set; }

        [ForeignKey("Education")][Required]
        public int Education_Id { get; set; }

        public Account Account { get; set; }
        public Education Education { get; set; }
    }
}
