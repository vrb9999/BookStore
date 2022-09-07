using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer
{
    public class UserModel
    {
        [Required]
        [DefaultValue("FullName")]
        [RegularExpression("[A-Z]{1}[a-z]{3,}", ErrorMessage = "Please Enter for Fullname Atleast 3 character with first letter capital")]
        public string FullName { get; set; }

        [Required]
        [DefaultValue("sample@gmail.com")]
        [RegularExpression("^([A-Za-z0-9]{3,20})([.][A-Za-z0-9]{1,10})*([@][A-Za-z]{2,5})+[.][A-Za-z]{2,3}([.][A-Za-z]{2,3})?$", ErrorMessage = "Please Enter Valid Email")]
        public string Email { get; set; }

        [Required]
        [DefaultValue("Password@123")]
        [RegularExpression("(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$_])[a-zA-Z0-9@#$_]{8,}", ErrorMessage = "Please Enter Atleast 8 character with Alteast one numeric,special character")]
        public string Password { get; set; }

        [Required]
        [RegularExpression("(0|91)?[6-9][0-9]{9}", ErrorMessage = "Please Enter 10 digit Mobile Number")]
        public string Mobile { get; set; }
    }
}
