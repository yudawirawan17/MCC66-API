using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Models;
using API.Repository.Data;
using API.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountRoleController : ControllerBase
    {
        private readonly AccountRoleRepository accountRoleRepository;
        public AccountRoleController(AccountRoleRepository accountRoleRepository)
        {
            this.accountRoleRepository = accountRoleRepository;
        }

        [Authorize(Roles = "Director")]
        [HttpPost]
        [Route("Assign")]
        public ActionResult AssignManajer(AccountRole accountRole)
        {
            var res = accountRoleRepository.AssignManajer(accountRole);
            if (res == 4041)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "NIK not found" });
            }
            /*else if (res == 4042)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Role not found" });
            }*/
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Manager role added to employee" });
            }
        }
    }
}