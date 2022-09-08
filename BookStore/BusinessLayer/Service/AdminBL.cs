using BusinessLayer.Interface;
using DatabaseLayer.Admin;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AdminBL : IAdminBL
    {
        IAdminRL adminRL;
        public AdminBL(IAdminRL adminRL)
        {
            this.adminRL = adminRL;
        }

        public string AdminLogin(AdminLoginModel adminLoginModel)
        {
            try
            {
                return this.adminRL.AdminLogin(adminLoginModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
