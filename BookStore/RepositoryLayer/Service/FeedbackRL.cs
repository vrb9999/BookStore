using DatabaseLayer.Feedback;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Service
{
    public class FeedbackRL : IFeedbackRL
    {
        private readonly string connectionString;
        public FeedbackRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore_db");
        }

        public bool AddFeedback(int UserId, FeedbackModel feedbackModel)
        {
            SqlConnection sqlconnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlconnection)
                {
                    sqlconnection.Open();
                    SqlCommand cmd = new SqlCommand("spAddFeedback", sqlconnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@BookId", feedbackModel.BookId);
                    cmd.Parameters.AddWithValue("@AvgRating", feedbackModel.AvgRating);
                    cmd.Parameters.AddWithValue("@Comments", feedbackModel.Comments);

                    int result = cmd.ExecuteNonQuery();

                    if (result > 1)
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

        public List<GetFeedbackModel> GetAllFeedback(int BookId)
        {
            List<GetFeedbackModel> list = new List<GetFeedbackModel>();
            SqlConnection sqlConnection = new SqlConnection(this.connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spGetFeedback", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@BookId", BookId);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        GetFeedbackModel Feedbackdetails = new GetFeedbackModel();
                        Feedbackdetails.FeedbackId = reader["FeedbackId"] == DBNull.Value ? default : reader.GetInt32("FeedbackId");
                        Feedbackdetails.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        Feedbackdetails.BookId = reader["BookId"] == DBNull.Value ? default : reader.GetInt32("BookId");
                        Feedbackdetails.AvgRating = reader["AvgRating"] == DBNull.Value ? default : reader.GetInt32("AvgRating");
                        Feedbackdetails.Comments = reader["Comments"] == DBNull.Value ? default : reader.GetString("Comments");
                        Feedbackdetails.FullName = reader["FullName"] == DBNull.Value ? default : reader.GetString("FullName");
                        list.Add(Feedbackdetails);
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
    }
}
