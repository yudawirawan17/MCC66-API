using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using API.Base;
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
    public class AccountController : BaseController<Account, AccountRepository, string>
    {
        private readonly AccountRepository accountRepository;
        public AccountController(AccountRepository accountRepository) : base(accountRepository)
        {
            this.accountRepository = accountRepository;
        }

        [HttpPost]
        [Route("login")]
        public ActionResult Login(LoginViewModel loginViewModel)
        {

            var res = accountRepository.Login(loginViewModel);
            if (res == 4001)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "Wrong Email" });
            }
            else if (res == 4002)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "Wrong Password" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Login Success" });
            }
        }

        [HttpPut]
        [Route("forgot/{Email}")]
        public ActionResult ForgotPassword(string Email)
        {
            if (accountRepository.ForgotPassword(Email))
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "OTP sent" });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Failed to send OTP" });
            }
        }

        [HttpPut]
        [Route("change")]
        public ActionResult ChangePassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            var res = accountRepository.ChangePassword(forgotPasswordViewModel);
            if (res == 4040)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "Email not found !!!" });
            }
            else if (res == 4041)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Wrong OTP !!!" });
            }
            else if (res == 4042)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "OTP was used !!!" });
            }
            else if (res == 4043)
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Expired OTP !!!" });
            }
            else
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Password changed" });
            }
        }

        //Login Jwt
        [HttpPost]
        [Route("Login/Jwt")]
        public ActionResult LoginJwt(LoginViewModel loginViewModel)
        {
            string Token;

            if (accountRepository.LoginJwt(loginViewModel, out Token) == 200)
            {
                return StatusCode(200, new { status = HttpStatusCode.OK, message = "Login Successfull", Token });
            }
            else if (accountRepository.LoginJwt(loginViewModel, out Token) == 4042)
            {
                return StatusCode(404, new { status = HttpStatusCode.BadRequest, message = "Wrong Password", Token });
            }
            else
            {
                return StatusCode(400, new { status = HttpStatusCode.BadRequest, message = "Wrong Email", Token });
            }
        }
        [Authorize]
        [HttpGet]
        [Route("Login/JwtTest")]
        public ActionResult JwtTest()
        {
            return StatusCode(200, new { status = HttpStatusCode.OK, message = "Jwt test was successfull" });
            //return Ok("Jwt test was successfull");
        }
    }
}