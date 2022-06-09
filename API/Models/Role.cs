using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class Role
    {
        [Key]
        public string RoleId { get; set; }
        public string Name { get; set; }
        public ICollection<AccountRole> AccountRoles { get; set; }

        //public const string RoleName = "Employee";
    }
}
