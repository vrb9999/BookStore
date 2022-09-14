using BusinessLayer.Interface;
using DatabaseLayer;
using DatabaseLayer.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        public UserController(IUserBL userBL)
        {
            this.userBL = userBL;
        }

        [HttpPost("Register")]
        public IActionResult AddUser(UserModel userModel)
        {
            try
            {
                var result = this.userBL.AddUser(userModel);
                if (result)
                {
                    return this.Ok(new { Success = true, message = "Registration Successful..." });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Registration unsuccessful!!!" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("Login")]
        public IActionResult LoginUser(UserLoginModel userLogin)
        {
            try
            {
                var result = this.userBL.LoginUser(userLogin);
                if (result != null)
                {
                    return this.Ok(new { Success = true, message = "Login Successfull", data = result });
                }
                else
                {
                    return this.BadRequest(new { Success = false, message = "Login failed" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgetPasswordUser/{email}")]
        public IActionResult ForgetPasswordUser(string email)
        {
            try
            {
                bool result = this.userBL.ForgetPasswordUser(email);
                return Ok(new { success = true, Message = "Reset Password Link Sent successfully", data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        [HttpPut("ResetPassword")]
        public IActionResult ResetPassword(PasswordModel modelPassword)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                //var email = claims.Where(p => p.Type == @"http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress").FirstOrDefault()?.Value;
                var email = claims.Where(p => p.Type == @"Email").FirstOrDefault()?.Value;
                bool result = this.userBL.ResetPassoword(email, modelPassword);
                return Ok(new { success = true, Message = $"{email} your Password Updated successfully!" });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
