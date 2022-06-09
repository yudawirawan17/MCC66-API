using API.Context;
using API.Models;
using API.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRoleRepository
    {
        private readonly MyContext myContext;
        public AccountRoleRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int AssignManajer(AccountRole accountRole)
        {
            var cek_nik = myContext.Employees.FirstOrDefault(ro => ro.NIK == accountRole.NIK);
            if (cek_nik == null)
            {
                return 4041;
            }
            else
            {
                accountRole.RoleId = "2";
                myContext.AccountRoles.Add(accountRole);
                myContext.SaveChanges();
                return 200;
            }

            /*AccountRole acro = new AccountRole();   
            acro.NIK = assignManagerVM.NIK;
            acro.RoleId = assignManagerVM.RoleId;*/


            /*var cek_email = (from em in myContext.Employees
                             join acr in myContext.AccountRoles on em.NIK equals acr.NIK
                             where em.Email == assignManagerVM.Email
                             select em).FirstOrDefault();
            if (cek_email.NIK == null)
            {
                return 4041;
            }
            else
            {
                myContext.Add(assignManagerVM);
                myContext.SaveChanges();
                return 200;
            }*/
            /*myContext.Add(assignManagerVM);
            var result = myContext.SaveChanges();
            return result;*/
            /*var cek_email = (from em in myContext.Employees
                            join acr in myContext.AccountRoles on em.NIK equals acr.NIK
                            join ro in myContext.Roles on acr.RoleId equals ro.Name
                            where em.Email == assignManagerVM.Email
                            select em.NIK).FirstOrDefault();
            AccountRole acr1 = new AccountRole();

            //var cek_role = myContext.Roles.FirstOrDefault(ro => ro.RoleId == assignManagerVM.Email);
            
            acr1.NIK = cek_email;
            acr1.RoleId = assignManagerVM.RoleId;
            myContext.AccountRoles.Add(acr1);
            myContext.SaveChanges();

            // success
            return 200;*/
            /*if (cek_email == null)
            {
                // email not found
                return 4041;
            }
            else
            {
                AccountRole acr1 = new AccountRole();

                var cek_role = myContext.Roles.FirstOrDefault(ro => ro.RoleId == assignManagerVM.Email);
                if (acr1.RoleId != assignManagerVM.RoleId)
                {

                }
                acr1.NIK = cek_email;
                acr1.RoleId = assignManagerVM.RoleId;
                myContext.AccountRoles.Add(acr1);
                myContext.SaveChanges();

                // success
                return 200;
            }*/
        }
    }
}
