using API.Context;
using API.Models;
using API.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class EmployeeRepository : GeneralRepository<MyContext, Employee, string>
    {
        private readonly MyContext myContext;
        public EmployeeRepository(MyContext myContext) : base(myContext)
        {
            this.myContext = myContext;
        }
        
        public int Register(RegisterViewModel registerViewModel)
        {
            Employee em = new Employee();
            Account ac = new Account();
            Education ed = new Education();
            AccountRole acr = new AccountRole();
            Role ro = new Role(); 

            em.NIK = GenerateNIK();
            em.FirstName = registerViewModel.FirstName;
            em.LastName = registerViewModel.LastName;
            em.Phone = registerViewModel.Phone;
            em.Birthdate = registerViewModel.Birthdate;
            em.Salary = registerViewModel.Salary;
            em.Email = registerViewModel.Email;
            em.Gender = (Gender)Enum.Parse(typeof(Gender), registerViewModel.Gender);
            
            em.Account = ac;

            ac.Password = HashPassword(registerViewModel.Password);


            University un = myContext.University.Find(registerViewModel.University_Id);
            ed.Degree = (Degree)Enum.Parse(typeof(Degree), registerViewModel.Degree);
            ed.GPA = registerViewModel.GPA;
            ed.University = un;

            Profiling pr = new Profiling();
            pr.Education = ed;

            ac.Profiling = pr;

            myContext.Add(em);

            //acr.Account = ac;
            acr.NIK = ac.NIK;
            acr.RoleId = "1";
            myContext.Add(acr);

            var result = myContext.SaveChanges();
            return result;
        }


        public string GenerateNIK()
        {
            if (myContext.Employees.Count() > 0)
            {
                Employee emp = new Employee();
                var count = (from s in myContext.Employees orderby s.NIK select s.NIK).Last();
                var last_nik = Convert.ToInt32(count.Substring(count.Length - 4)) + 1;

                string new_count;

                if (last_nik < 10)
                {
                    new_count = "000" + last_nik;
                }
                else if (last_nik < 100)
                {
                    new_count = "00" + last_nik;
                }
                else if (last_nik < 1000)
                {
                    new_count = "0" + last_nik;
                }
                else
                {
                    new_count = last_nik.ToString();
                }

                var res = DateTime.Now.ToString("MMddyyyy") + new_count;
                return res;

            }
            else
            {
                var fr = DateTime.Now.ToString("MMddyyyy") + "0001";
                return fr;
            }
        }


        public bool validEmail(string Email)
        {
            var res = myContext.Employees.FirstOrDefault(e => e.Email == Email);
            return res !=null;
        }

        public bool validPhone(string Phone)
        {
            var res = myContext.Employees.FirstOrDefault(e => e.Phone == Phone);
            return res != null;
        }

        // hash password
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        public string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password, GetRandomSalt());
        }

        public IEnumerable<GetRegisterDataVM> GetRegisterData()
        {
            var regdat = (from em in myContext.Employees
                          join pr in myContext.Profilling on em.NIK equals pr.NIK
                          join ed in myContext.Educations on pr.Education_Id equals ed.Id
                          join un in myContext.University on ed.University_Id equals un.Id
                          //join ac in myContext.Accounts on em.NIK equals ac.NIK
                          join acr in myContext.AccountRoles on em.NIK equals acr.NIK
                          join ro in myContext.Roles on acr.RoleId equals ro.RoleId
                          select new GetRegisterDataVM()
                          {
                              NIK = em.NIK,
                              //FirstName = em.FirstName,
                              //LastName = em.LastName,
                              FullName = String.Format("{0} {1}", em.FirstName, em.LastName),
                              Phone = em.Phone,
                              Birthdate = em.Birthdate,
                              Salary = em.Salary,
                              Email = em.Email,
                              Gender = em.Gender.ToString(),
                              Degree = ed.Degree.ToString(),
                              GPA = ed.GPA,
                              //University_Id = un.Id,
                              UniversityName = un.Name,
                              Role = ro.Name
                          }).ToList();
            return regdat;
        }

        

    }
}
