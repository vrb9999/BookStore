using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.User
{
    public class GetUserModel
    {
        public int UserId { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string Mobile { get; set; }
    }
}
