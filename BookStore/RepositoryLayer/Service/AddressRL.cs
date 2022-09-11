using DatabaseLayer.Address;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class AddressRL : IAddressRL
    {
        private readonly string connectionString;
        public AddressRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore_db");
        }

        public bool AddAddress(int UserId, AddressModel addressModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("spAddAddress", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@TypeId", addressModel.TypeId);
                    cmd.Parameters.AddWithValue("@Address", addressModel.Address);
                    cmd.Parameters.AddWithValue("@City", addressModel.City);
                    cmd.Parameters.AddWithValue("@State", addressModel.State);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    else 
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public List<GetAddressModel> GetAllAddress(int UserId)
        {
            List<GetAddressModel> list = new List<GetAddressModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllAddress", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GetAddressModel addressdetails = new GetAddressModel();
                        addressdetails.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        addressdetails.UserId = UserId;
                        addressdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        addressdetails.Mobile = reader["Mobile"] == DBNull.Value ? default : reader.GetInt64("Mobile");
                        addressdetails.TypeId = reader["TypeId"] == DBNull.Value ? default : reader.GetInt32("TypeId");
                        addressdetails.Address = reader["Address"] == DBNull.Value ? default : reader.GetString("Address");
                        addressdetails.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                        addressdetails.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                        list.Add(addressdetails);
                    }

                    return list;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public GetAddressModel GetAddressById(int AddressId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAddressById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", AddressId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetAddressModel addressdetails = new GetAddressModel();
                    if (reader.Read())
                    {
                        addressdetails.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        addressdetails.UserId = UserId;
                        addressdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        addressdetails.Mobile = reader["Mobile"] == DBNull.Value ? default : reader.GetInt64("Mobile");
                        addressdetails.TypeId = reader["TypeId"] == DBNull.Value ? default : reader.GetInt32("TypeId");
                        addressdetails.Address = reader["Address"] == DBNull.Value ? default : reader.GetString("Address");
                        addressdetails.City = reader["City"] == DBNull.Value ? default : reader.GetString("City");
                        addressdetails.State = reader["State"] == DBNull.Value ? default : reader.GetString("State");
                    }

                    if (addressdetails.AddressId == 0)
                    {
                        return null;
                    }

                    return addressdetails;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }

        public bool UpdateAddress(int UserId, UpdateAddressModel updateAddressModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand cmd = new SqlCommand("spUpdateAddress", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AddressId", updateAddressModel.AddressId);
                    cmd.Parameters.AddWithValue("@TypeId", updateAddressModel.TypeId);
                    cmd.Parameters.AddWithValue("@Address", updateAddressModel.Address);
                    cmd.Parameters.AddWithValue("@City", updateAddressModel.City);
                    cmd.Parameters.AddWithValue("@State", updateAddressModel.State);

                    var result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlconnection.Close();
            }
        }

        public bool DeleteAddress(int AddressId, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteAddress", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@AddressId ", AddressId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);
                    var result = cmd.ExecuteNonQuery();
                    if (result == 0)
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                sqlConnection.Close();
            }
        }
    }
}
