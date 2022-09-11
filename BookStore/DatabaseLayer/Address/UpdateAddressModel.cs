using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Address
{
    public class UpdateAddressModel
    {
        [Required]
        public int AddressId { get; set; }

        [Required]
        [Range(1, 3)]
        [DefaultValue("1")]
        public int TypeId { get; set; }

        [Required]
        [DefaultValue("")]
        public string Address { get; set; }

        [Required]
        [DefaultValue("")]
        public string City { get; set; }

        [Required]
        [DefaultValue("")]
        public string State { get; set; }
    }
}
