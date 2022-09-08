using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Admin
{
    public class AdminLoginModel
    {
        [Required]
        [DefaultValue("sample@gmail.com")]
        public string Email { get; set; }

        [Required]
        [DefaultValue("Password@123")]
        public string Password { get; set; }
    }
}
