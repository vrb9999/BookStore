using DatabaseLayer.Book;
using DatabaseLayer.Cart;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class CartRL : ICartRL
    {
        private readonly string connectionString;
        public CartRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore_db");
        }

        public CartModel AddCart(CartModel cart, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spAddToCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@BookId ", cart.BookId);
                    cmd.Parameters.AddWithValue("@Book_Quantity ", cart.BookQuantity);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);
                                        
                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return cart;
                    }
                    else
                    {
                        return null;
                    }
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

        public List<GetCartModel> GetAllCart(int UserId)
        {
            List<GetCartModel> list = new List<GetCartModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllBookFromCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GetCartModel getCartModel = new GetCartModel();
                        getCartModel.CartId = reader["CartId"] == DBNull.Value ? default : reader.GetInt32("CartId");
                        getCartModel.UserId = UserId;
                        getCartModel.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        getCartModel.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        getCartModel.AuthorName = reader["AuthorName"] == DBNull.Value ? default : reader.GetString("AuthorName");
                        getCartModel.Book_Quantity = reader["Book_Quantity"] == DBNull.Value ? default : reader.GetInt32("Book_Quantity");
                        getCartModel.OriginalPrice = reader["OriginalPrice"] == DBNull.Value ? default : reader.GetInt32("OriginalPrice");
                        getCartModel.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetInt32("DiscountPrice");
                        getCartModel.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        list.Add(getCartModel);
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

        public string DeleteCart(int CartId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId ", CartId);
                    
                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return "Remove Cart";
                    }
                    else
                    {
                        return "Failed";
                    }
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

        public CartModel UpdateCart(int CartId, CartModel cart, int UserId)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("SpUpdateCart", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@CartId ", CartId);
                    cmd.Parameters.AddWithValue("@BookQuantity ", cart.BookQuantity);
                    cmd.Parameters.AddWithValue("@BookId ", cart.BookId);
                    cmd.Parameters.AddWithValue("@UserId ", UserId);
                    
                    var result = cmd.ExecuteNonQuery();

                    if (result != 0)
                    {
                        return cart;
                    }
                    else
                    {
                        return null;
                    }
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