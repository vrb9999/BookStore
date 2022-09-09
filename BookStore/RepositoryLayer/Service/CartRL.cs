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

        public bool AddToCart(int UserId, CartPostModel cartPostModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand cmd = new SqlCommand("spAddToCart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", cartPostModel.BookId);
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@Quantity", cartPostModel.Quantity);
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
                connection.Close();
            }
        }

        public List<CartModel> GetCart(int UserId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            List<CartModel> cartList = new List<CartModel>();

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand cmd = new SqlCommand("spGetAllCart", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    CartModel cart = new CartModel();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cart.CartId = Convert.ToInt32(reader["CartId"]);
                        cart.BookId = Convert.ToInt32(reader["BookId"]);
                        cart.UserId = Convert.ToInt32(reader["UserId"]);
                        cart.Quantity = Convert.ToInt32(reader["Quantity"]);
                        cart.BookName = Convert.ToString(reader["BookName"]);
                        cart.AuthorName = Convert.ToString(reader["AuthorName"]);
                        cart.Description = Convert.ToString(reader["Description"]);
                        cart.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        cart.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        cart.BookImg = Convert.ToString(reader["BookImg"]);
                        

                        cartList.Add(cart);
                    }
                    return cartList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                connection.Close();
            }
        }
    }
}