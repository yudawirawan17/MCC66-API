using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Base;
using API.Models;
using API.Repository;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository employeeRepository) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
        }

        [HttpPost("{register}")]
        public ActionResult Register(RegisterViewModel registerViewModel)
        {
            if (employeeRepository.validEmail(registerViewModel.Email))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Email Used, Try another email" });
            }
            else if (employeeRepository.validPhone(registerViewModel.Phone))
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Phone Number Used, Try another number" });
            }

            var result = employeeRepository.Register(registerViewModel);
            if (result == 0)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Insert Failed" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Data inserted" });
            }
        }

        [Authorize(Roles = "Director, Manager")]
        [HttpGet]
        [Route("GetRegisterData")]
        public IEnumerable<GetRegisterDataVM> GetRegisterData()
        {
            return employeeRepository.GetRegisterData().ToList();
        }

        [HttpGet]
        [Route("TestCORS")] 
        public ActionResult TestCors()
        {
            return Ok("Test Cors berhasil");
        }

    }
}