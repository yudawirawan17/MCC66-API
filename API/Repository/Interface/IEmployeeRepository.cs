using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repository.Interface
{
    interface IEmployeeRepository
    {
        IEnumerable<Employee> Get();  //non void, return implement method / IEnumerable&IList / get all
        Employee Get(string FirstName);
        int Insert(Employee employee);
        int Update(Employee employee);
        //int UpdateId(string NIK, Employee employee);
        int Delete(string NIK);
        Employee GetFirst(string FirstName);
        Employee GetFirstOrDefault(string FirstName);
        Employee GetSingle(string LastName);
        Employee GetSingleOrDefault(string LastName);
        Employee GetFind(int Salary);
        Employee GetFindFN(string FirstName);
    }
}
