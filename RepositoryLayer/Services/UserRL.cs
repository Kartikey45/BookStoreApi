using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class UserRL : IUserRL
    {
        //Configuration initialized
        private readonly IConfiguration Configuration;

        //constructor 
        public UserRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public UserRegistration Registration(UserRegistration user)
        {
           try
           {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                //password encrypted
                string Password = EncryptedPassword.EncodePasswordToBase64(user.Password);
                DateTime createdDate = user.CreatedDate;
                createdDate = DateTime.Now;

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("UserRegistration", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@FirstName", user.FirstName);
                    sqlCommand.Parameters.AddWithValue("@LastName", user.LastName);
                    sqlCommand.Parameters.AddWithValue("@UserRole", user.UserRole);
                    sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@Address", user.Address);
                    sqlCommand.Parameters.AddWithValue("@City", user.City);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", createdDate);

                    //connection open 
                    Connection.Open();

                    //Execute query
                    sqlCommand.ExecuteNonQuery();

                    //connection close
                    Connection.Close();
                }
            }
           catch(Exception ex)
           {
                throw new Exception(ex.Message);
           }
            return user;
        }
    }
}
