﻿using DatabaseLayer.Book;
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
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        BookModel getAllBook = new BookModel();
                        getAllBook.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        getAllBook.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                        getAllBook.AuthorName = reader["AuthorName"] == DBNull.Value ? default : reader.GetString("AuthorName");
                        getAllBook.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                        getAllBook.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                        getAllBook.OriginalPrice = reader["OriginalPrice"] == DBNull.Value ? default : reader.GetInt32("OriginalPrice");
                        getAllBook.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetInt32("DiscountPrice");
                        getAllBook.AvgRating = reader["AvgRating"] == DBNull.Value ? default : reader.GetDecimal("AvgRating");
                        getAllBook.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                        getAllBook.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");

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

        public BookModel GetBookById(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetBookById", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId ", BookId);
                    SqlDataReader reader = cmd.ExecuteReader();
                    BookModel getBookById = new BookModel();
                    while (reader.Read())
                    {
                        getBookById.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        getBookById.BookName = Convert.ToString(reader["BookName"]);
                        getBookById.AuthorName = Convert.ToString(reader["AuthorName"]);
                        getBookById.Description = Convert.ToString(reader["Description"]);
                        getBookById.Quantity = Convert.ToInt32(reader["Quantity"]);
                        getBookById.OriginalPrice = Convert.ToInt32(reader["OriginalPrice"]);
                        getBookById.DiscountPrice = Convert.ToInt32(reader["DiscountPrice"]);
                        getBookById.AvgRating = Convert.ToDecimal(reader["AvgRating"]);
                        getBookById.RatingCount = Convert.ToInt32(reader["RatingCount"]);
                        getBookById.BookImg = Convert.ToString(reader["BookImg"]);
                    }
                    if (getBookById.BookId > 0)
                    {
                        return getBookById;
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

        public BookModel UpdateBooks(int BookId, BookModel bookModel)
        {

            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                {
                    sqlconnection.Open();

                    SqlCommand com = new SqlCommand("spUpdateBook", sqlconnection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@BookId", BookId);
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
                    if (result != 0)
                    {
                        SqlDataReader reader = com.ExecuteReader();
                        BookModel response = new BookModel();
                        if (reader.Read())
                        {
                            response.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                            response.BookName = reader["BookName"] == DBNull.Value ? default : reader.GetString("BookName");
                            response.AuthorName = reader["AuthorName"] == DBNull.Value ? default : reader.GetString("AuthorName");
                            response.Description = reader["Description"] == DBNull.Value ? default : reader.GetString("Description");
                            response.Quantity = reader["Quantity"] == DBNull.Value ? default : reader.GetInt32("Quantity");
                            response.OriginalPrice = reader["OriginalPrice"] == DBNull.Value ? default : reader.GetInt32("OriginalPrice");
                            response.DiscountPrice = reader["DiscountPrice"] == DBNull.Value ? default : reader.GetInt32("DiscountPrice");
                            response.AvgRating = reader["AvgRating"] == DBNull.Value ? default : reader.GetDecimal("AvgRating");
                            response.RatingCount = reader["RatingCount"] == DBNull.Value ? default : reader.GetInt32("RatingCount");
                            response.BookImg = reader["BookImg"] == DBNull.Value ? default : reader.GetString("BookImg");
                        }

                        return response;
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
                sqlconnection.Close();
            }
        }

        public bool DeleteBook(int BookId)
        {
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spDeleteBook", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId ", BookId);
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
