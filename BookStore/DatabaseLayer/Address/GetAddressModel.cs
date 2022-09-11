using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Address
{
    public class GetAddressModel
    {
        public int AddressId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }

        public long Mobile { get; set; }

        public int TypeId { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string State { get; set; }
    }
}
