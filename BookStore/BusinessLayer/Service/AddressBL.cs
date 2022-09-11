using BusinessLayer.Interface;
using DatabaseLayer.Address;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class AddressBL : IAddressBL
    {
        IAddressRL addressRL;
        public AddressBL(IAddressRL addressRL)
        {
            this.addressRL = addressRL;
        }

        public bool AddAddress(int UserId, AddressModel addressModel)
        {
            try
            {
                return addressRL.AddAddress(UserId, addressModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetAddressModel> GetAllAddress(int UserId)
        {
            try
            {
                return addressRL.GetAllAddress(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public GetAddressModel GetAddressById(int AddressId, int UserId)
        {
            try
            {
                return addressRL.GetAddressById(AddressId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool UpdateAddress(int UserId, UpdateAddressModel updateAddressModel)
        {
            try
            {
                return addressRL.UpdateAddress(UserId, updateAddressModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteAddress(int AddressId, int UserId)
        {
            try
            {
                return addressRL.DeleteAddress(AddressId, UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
