using DatabaseLayer.Order;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class OrderRL : IOrderRL
    {
        private readonly string connectionString;
        public OrderRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore_db");
        }

        public bool AddOrder(OrderModel orderModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("spAddOrder", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@CartId", orderModel.CartId);
                    cmd.Parameters.AddWithValue("@AddressId", orderModel.AddressId);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0 && result != 1)
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

        public List<GetOrderModel> GetAllOrders(int UserId)
        {
            List<GetOrderModel> list = new List<GetOrderModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllOrders", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GetOrderModel order = new GetOrderModel();
                        order.OrderId = reader["OrderId"] == DBNull.Value ? default : reader.GetInt32("OrderId");
                        order.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        order.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        order.AddressId = reader["AddressId"] == DBNull.Value ? default : reader.GetInt32("AddressId");
                        order.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        order.AuthorName = reader["AuthorName"] == DBNull.Value ? default : reader.GetString("AuthorName");
                        order.OrderQuantity = reader["OrderQuantity"] == DBNull.Value ? default : reader.GetInt32("OrderQuantity");
                        order.TotalOrderPrice = reader["TotalOrderPrice"] == DBNull.Value ? default : reader.GetDecimal("TotalOrderPrice");
                        order.OrderDate = reader["OrderDate"] == DBNull.Value ? default : reader.GetDateTime("OrderDate");
                        order.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        list.Add(order);
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

        public bool DeleteOrder(int UserId, int OrderId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteOrder", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@OrderId", OrderId);
                    int result = cmd.ExecuteNonQuery();

                    if (result > 0)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
