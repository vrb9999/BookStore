using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Order
{
    public class OrderModel
    {
        [Required]
        public int CartId { get; set; }

        [Required]
        public int AddressId { get; set; }
    }
}
