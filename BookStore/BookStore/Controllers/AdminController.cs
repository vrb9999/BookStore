using BusinessLayer.Interface;
using DatabaseLayer.Admin;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        IAdminBL adminBL;
        public AdminController(IAdminBL adminBL)
        {
            this.adminBL = adminBL;
        }

        [HttpPost("Login")]
        public IActionResult AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                var result = this.adminBL.AdminLogin(adminLoginModel);
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
    }
}
