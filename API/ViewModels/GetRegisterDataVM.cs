using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class GetRegisterDataVM
    {
        public string NIK { get; set; }
        //public string FirstName { get; set; }
        //public string LastName { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public int? Salary { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Degree { get; set; }
        public string GPA { get; set; }
        //public int University_Id { get; set; }
        public string UniversityName { get; set; }
        //public string RoleId { get; set; }
        public string Role { get; set; }
    }

}
