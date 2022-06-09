using API.Context;
using API.Models;
using API.ViewModels;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Repository.Data
{
    public class AccountRepository : GeneralRepository<MyContext, Account, string>
    {
        private readonly MyContext myContext;
        public IConfiguration _configuration;
        public AccountRepository(MyContext myContext, IConfiguration configuration) : base(myContext)
        {
            this.myContext = myContext;
            this._configuration = configuration;
        }

        // Login
        public int Login(LoginViewModel loginViewModel)
        {
            var res = myContext.Employees.FirstOrDefault(e => e.Email == loginViewModel.Email);

            if (res == null)
            {
                return 4001;
            }
            else
            {
                var cek = (from e in myContext.Employees
                           join a in myContext.Accounts
                           on e.NIK equals a.NIK
                           where e.Email == loginViewModel.Email
                           select a).FirstOrDefault();
                var cek_pass = ValidatePassword(loginViewModel.Password, cek.Password);
                if (cek_pass == false)
                {
                    return 4002;
                }
                else
                {
                    return 200;
                }
            }

        }

        // Forgot Pass OTP sent to email
        public bool ForgotPassword(string Email)
        {
            var res = myContext.Employees.FirstOrDefault(e => e.Email == Email);

            if (res == null)
            {
                return false;
            }
            else
            {
                var new_otp = (from e in myContext.Employees
                               join a in myContext.Accounts
                               on e.NIK equals a.NIK
                               where e.Email == Email
                               select a).FirstOrDefault();


                new_otp.OTPCode = OTP();
                new_otp.OTPExpiredAt = DateTime.Now.AddMinutes(10);
                new_otp.OTPStatus = true;
                myContext.Update(new_otp);
                myContext.SaveChanges();
                //        from:                                  subject:    Body:
                SendMail("ebony.paucek46@ethereal.email", Email, "Kode OTP", "Kode OTP anda : " + new_otp.OTPCode);
                return true;
            }

        }

        // Generate OTP
        public string OTP()
        {
            // generate random OTP
            var chars1 = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890";
            var stringChars1 = new char[6];
            var random1 = new Random();

            for (int i = 0; i < stringChars1.Length; i++)
            {
                stringChars1[i] = chars1[random1.Next(chars1.Length)];
            }

            var rand_str = new String(stringChars1);
            // end of generate random OTP
            return rand_str;
        }

        // Change Password
        public int ChangePassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            // relation employee to account
            var cek = (from e in myContext.Employees
                       join a in myContext.Accounts
                       on e.NIK equals a.NIK
                       where e.Email == forgotPasswordViewModel.Email
                       select a).FirstOrDefault();

            // check password == cek
            var em = myContext.Employees.FirstOrDefault(e => e.Email == forgotPasswordViewModel.Email);
            if (em == null)
            {
                return 4040;
            }
            else if (cek.OTPCode != forgotPasswordViewModel.OTPCode)
            {
                // wrong OTP
                return 4041;
            }
            else if (cek.OTPStatus == false)
            {
                //OTP USED
                return 4042;
            }
            else if (DateTime.Now > cek.OTPExpiredAt)
            {
                // Expired OTP
                return 4043;
            }
            else
            {
                var new_upd = (from e in myContext.Employees
                               join a in myContext.Accounts
                               on e.NIK equals a.NIK
                               where e.Email == forgotPasswordViewModel.Email
                               select a).FirstOrDefault();

                new_upd.Password = HashPassword(forgotPasswordViewModel.Password);
                new_upd.OTPStatus = false;
                myContext.Update(new_upd);
                var result = myContext.SaveChanges();
                return result;
            }

        }
        // hash password - get random salt
        private static string GetRandomSalt()
        {
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }
        // hash password
        public string HashPassword(string Password)
        {
            return BCrypt.Net.BCrypt.HashPassword(Password, GetRandomSalt());
        }

        //Login check hash password
        public static bool ValidatePassword(String Password, string correctHash)
        {
            return BCrypt.Net.BCrypt.Verify(Password, correctHash);
        }

        // send OTP to Mail
        public void SendMail(string from, string to, string subject, string html)
        {
            // create message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(from));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html) { Text = html };

            // send email
            using var smtp = new SmtpClient();
            // ethereal
            smtp.Connect("smtp.ethereal.email", 587, SecureSocketOptions.StartTls);
            smtp.Authenticate("ebony.paucek46@ethereal.email", "Mf9kuj3wherRa4XJV4");
            smtp.Send(email);
            smtp.Disconnect(true);

            /* email contoh TO:
             ilene.sauer40@ethereal.email
             vkDQH5d26ReAf3SXSR
            */
        }


        // Jwt login
        public int LoginJwt(LoginViewModel loginViewModel, out string Token)
        {
            var res = myContext.Employees.FirstOrDefault(e => e.Email == loginViewModel.Email);

            if (res != null)
            {
                var password = (from e in myContext.Employees
                                join a in myContext.Accounts
                                on e.NIK equals a.NIK
                                where e.Email == loginViewModel.Email
                                select a.Password).FirstOrDefault();

                var checkPass = ValidatePassword(loginViewModel.Password, password);

                if (checkPass != false)
                {
                    var NIK = "";
                    var cekrole = (from em in myContext.Employees
                                   join acr in myContext.AccountRoles
                                   on em.NIK equals acr.NIK
                                   join ro in myContext.Roles
                                   on acr.RoleId equals ro.RoleId
                                   where em.Email == loginViewModel.Email
                                   select ro).ToList();

                    var claims = new List<Claim>();
                    claims.Add(new Claim("Email", loginViewModel.Email));
                    foreach (var role in cekrole)
                    {
                        claims.Add(new Claim("roles", role.Name));
                    }

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(10),
                        signingCredentials: signIn
                        );
                    var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                    claims.Add(new Claim("Token Security", idtoken.ToString()));
                    Token = idtoken;
                    return 200;
                }
                else
                {
                    // wrong password
                    Token = null;
                    return 4042;
                }
            }
            else
            {
                // wrong email
                Token = null;
                return 4041;
            }

        }
    }
}
