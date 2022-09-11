﻿using DatabaseLayer.Address;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IAddressRL
    {
        public bool AddAddress(int UserId, AddressModel addressModel);

        public List<GetAddressModel> GetAllAddress(int UserId);

        public GetAddressModel GetAddressById(int AddressId, int UserId);

        public bool UpdateAddress(int UserId, UpdateAddressModel updateAddressModel);

        public bool DeleteAddress(int AddressId, int UserId);
    }
}
