using DatabaseLayer.Book;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class BookRL : IBookRL
    {
        private readonly string connectionString;
        public BookRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore_db");
        }

        public BookModel AddBook(BookModel bookModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spAddBook", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@BookName", bookModel.BookName);
                    com.Parameters.AddWithValue("@AuthorName", bookModel.AuthorName);
                    com.Parameters.AddWithValue("@Description", bookModel.Description);
                    com.Parameters.AddWithValue("@Quantity", bookModel.Quantity);
                    com.Parameters.AddWithValue("@OriginalPrice", bookModel.OriginalPrice);
                    com.Parameters.AddWithValue("@DiscountPrice", bookModel.DiscountPrice);
                    com.Parameters.AddWithValue("@AvgRating", bookModel.AvgRating);
                    com.Parameters.AddWithValue("@RatingCount", bookModel.RatingCount);
                    com.Parameters.AddWithValue("@BookImg", bookModel.BookImg);
                    var result = com.ExecuteNonQuery();
                    if (result > 0)
                    {
                        return bookModel;
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
                connection.Close();
            }
        }

        public List<BookModel> GetAllBooks()
        {
            List<BookModel> listOfBooks = new List<BookModel>();
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetAllBooks", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    BookModel getAllBook = new BookModel();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        getAllBook.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        getAllBook.BookName = Convert.ToString(reader["BookName"]);
                        getAllBook.AuthorName = Convert.ToString(reader["AuthorName"]);
                        getAllBook.Description = Convert.ToString(reader["Description"]);
                        getAllBook.Quantity = Convert.ToInt32(reader["Quantity"]);
                        getAllBook.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        getAllBook.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        getAllBook.AvgRating = Convert.ToInt32(reader["AvgRating"]);
                        getAllBook.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        getAllBook.BookImg = Convert.ToString(reader["BookImg"]);

                        listOfBooks.Add(getAllBook);
                    }
                    return listOfBooks;
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
