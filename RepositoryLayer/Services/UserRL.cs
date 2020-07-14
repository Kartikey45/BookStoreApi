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

        //Method to register user in the dataabase
        public Response Registration(UserRegistration user)
        {
            Response response = new Response();
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
                    //sqlCommand.Parameters.AddWithValue("@UserRole", user.UserRole);
                    sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);
                    sqlCommand.Parameters.AddWithValue("@Address", user.Address);
                    sqlCommand.Parameters.AddWithValue("@City", user.City);
                    sqlCommand.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
                    sqlCommand.Parameters.AddWithValue("@CreatedDate", createdDate);

                    //connection open 
                    Connection.Open();

                    int status = 0;

                    //Execute query
                    status = sqlCommand.ExecuteNonQuery();

                    //connection close
                    Connection.Close();

                    //validation
                    if (status == 1)
                    {
                        response.Status = "Valid Email";
                    }
                    else
                    {
                        response.Status = "Invalid Email";
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method for user login
        public UserDetails Login(UserLogin user)
        {
            UserDetails details = new UserDetails();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                //Password encrypted
                string Password = EncryptedPassword.EncodePasswordToBase64(user.Password);

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("UserLogin", Connection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@Email", user.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);

                    //connection open 
                    Connection.Open();

                    /*
                    int status = 0;

                    //Execute query
                    status = sqlCommand.ExecuteNonQuery();
                    */

                    var temp = details.Email;

                    //read data form the database
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    

                    //While Loop For Reading status result from SqlDataReader.
                    while (reader.Read())
                    {
                        details.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        details.FirstName = reader["FirstName"].ToString();
                        details.LastName = reader["LastName"].ToString();
                        details.UserRole = reader["UserRole"].ToString();
                        details.Email = reader["Email"].ToString();
                        temp = details.Email;
                        //details.Password = reader["Password"].ToString();
                        details.Address = reader["Address"].ToString();
                        details.City = reader["City"].ToString();
                        details.PhoneNumber = reader["PhoneNumber"].ToString();
                        //details.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        //details.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    }

                    //connection close 
                    Connection.Close();

                    if(temp == null)
                    {
                        details = null;
                    }

                    /*
                    //validation
                    if (status == 1)
                    {
                        details.Status = "Valid Email";
                    }
                    else
                    {
                        details.Status = "Invalid Email";
                    }
                    */
                    if(details == null)
                    {
                        throw new Exception();
                    }
                    else
                    {
                        return details;
                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
