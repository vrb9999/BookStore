using DatabaseLayer;
using DatabaseLayer.User;
using Experimental.System.Messaging;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace RepositoryLayer.Service
{
    public class UserRL : IUserRL
    {
        private readonly string connectionString;
        public UserRL(IConfiguration configuartion)
        {
            connectionString = configuartion.GetConnectionString("BookStore_db");
        }

        public bool AddUser(UserModel userModel)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    //Creating a stored Procedure for adding Users into database
                    SqlCommand com = new SqlCommand("spAddUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@FullName", userModel.FullName);
                    com.Parameters.AddWithValue("@Email", userModel.Email);
                    com.Parameters.AddWithValue("@Password", userModel.Password);
                    com.Parameters.AddWithValue("@Mobile", userModel.Mobile);
                    var result = com.ExecuteNonQuery();
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
            finally
            {
                connection.Close();
            }
        }

        public string LoginUser(UserLoginModel userLogin)
        {
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            try
            {
                using (sqlConnection)
                {
                    sqlConnection.Open();
                    SqlCommand cmd = new SqlCommand("spLoginUser", sqlConnection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Email", userLogin.Email);
                    cmd.Parameters.AddWithValue("@Password", userLogin.Password);
                    cmd.ExecuteNonQuery();

                    SqlDataReader reader = cmd.ExecuteReader();
                    GetUserModel response = new GetUserModel();
                    if (reader.Read())
                    {
                        response.UserId = reader["UserId"] == DBNull.Value ? default : reader.GetInt32("UserId");
                        response.Email = reader["Email"] == DBNull.Value ? default : reader.GetString("Email");
                        response.Password = reader["Password"] == DBNull.Value ? default : reader.GetString("Password");
                    }
                    return GenerateJWTToken(response.Email, response.UserId);
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

        //Method to Generate JWT Token for Athuntication and Athorization
        private string GenerateJWTToken(string email, int userId)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email),
                    new Claim("UserId",userId.ToString())
                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ForgetPasswordUser(string email)
        {
            SqlConnection connection = new SqlConnection(connectionString);

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spForgetPasswordUser", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", email);
                    var result = com.ExecuteNonQuery();
                    SqlDataReader rd = com.ExecuteReader();
                    GetUserModel response = new GetUserModel();
                    if (rd.Read())
                    {
                        response.UserId = rd["UserId"] == DBNull.Value ? default : rd.GetInt32("UserId");
                        response.Email = rd["Email"] == DBNull.Value ? default : rd.GetString("Email");
                        response.FullName = rd["FullName"] == DBNull.Value ? default : rd.GetString("FullName");
                    }
                    MessageQueue messageQueue;
                    //ADD MESSAGE TO QUEUE
                    if (MessageQueue.Exists(@".\Private$\BookStoreQueue"))
                    {
                        messageQueue = new MessageQueue(@".\Private$\BookStoreQueue");
                    }
                    else
                    {
                        messageQueue = MessageQueue.Create(@".\Private$\BookStoreQueue");
                    }
                    Message MyMessage = new Message();
                    MyMessage.Formatter = new BinaryMessageFormatter();
                    MyMessage.Body = GenerateJWTToken(email, response.UserId);
                    MyMessage.Label = "Forget Password Email";
                    messageQueue.Send(MyMessage);
                    Message msg = messageQueue.Receive();
                    msg.Formatter = new BinaryMessageFormatter();
                    EmailService.SendEmail(email, msg.Body.ToString(), response.FullName);
                    messageQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(msmqQueue_ReceiveCompleted);

                    messageQueue.BeginReceive();
                    messageQueue.Close();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void msmqQueue_ReceiveCompleted(object sender, ReceiveCompletedEventArgs e)
        {
            try
            {
                MessageQueue queue = (MessageQueue)sender;
                Message msg = queue.EndReceive(e.AsyncResult);
                EmailService.SendEmail(e.Message.ToString(), GenerateToken(e.Message.ToString()), e.Message.ToString());
                queue.BeginReceive();
            }
            catch (MessageQueueException ex)
            {
                if (ex.MessageQueueErrorCode ==
                    MessageQueueErrorCode.AccessDenied)
                {
                    Console.WriteLine("Access is denied. " + "Queue might be a system queue.");
                }
            }
        }

        private string GenerateToken(string email)
        {
            try
            {
                // generate token
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                    new Claim("Email", email)

                    }),
                    Expires = DateTime.UtcNow.AddHours(2),

                    SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
                };
                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool ResetPassoword(string email, PasswordModel modelPassword)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            var result = 0;

            try
            {
                using (connection)
                {
                    connection.Open();
                    SqlCommand com = new SqlCommand("spResetPassword", connection);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@Email", email);
                    com.Parameters.AddWithValue("@Password", modelPassword.Password);
                    if (modelPassword.Password == modelPassword.CPassword)
                    {
                        result = com.ExecuteNonQuery();
                    }

                    if (result > 0)
                        return true;
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
