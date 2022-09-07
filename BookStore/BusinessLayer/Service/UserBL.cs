using BusinessLayer.Interface;
using DatabaseLayer;
using DatabaseLayer.User;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }

        public bool AddUser(UserModel userModel)
        {
            try
            {
                return this.userRL.AddUser(userModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string LoginUser(UserLoginModel userLogin)
        {
            try
            {
                return this.userRL.LoginUser(userLogin);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPasswordUser(string email)
        {
            try
            {
                return this.userRL.ForgetPasswordUser(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassoword(string email, PasswordModel modelPassword)
        {
            try
            {
                return this.userRL.ResetPassoword(email, modelPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
