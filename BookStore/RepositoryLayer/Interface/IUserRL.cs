using DatabaseLayer;
using DatabaseLayer.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        public bool AddUser(UserModel userModel);

        public string LoginUser(UserLoginModel userLogin);

        public bool ForgetPasswordUser(string email);

        public bool ResetPassoword(string email, PasswordModel modelPassword);
    }
}
