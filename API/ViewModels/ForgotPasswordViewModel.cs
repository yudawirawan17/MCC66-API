using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ViewModels
{
    public class ForgotPasswordViewModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string OTPCode { get; set; }
        public bool OTPStatus { get; set; }
        public DateTime OTPExpiredAt { get; set; }
    }
}
