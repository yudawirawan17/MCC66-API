using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountRole
    {
        public string NIK { get; set; }
        public Account Account { get; set; }
        public string RoleId { get; set; }
        public Role Role { get; set; }
    }
}
