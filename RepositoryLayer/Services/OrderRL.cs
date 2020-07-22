using CommonLayer.OrderModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class OrderRL : IOrderRL
    {
        //Configuration initialized
        private readonly IConfiguration Configuration;

        //constructor 
        public OrderRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        //Place order
        public Orderdetails PlaceOrder(int UserId, int CartId)
        {
            Orderdetails details = new Orderdetails();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("PlaceOrder", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand.Parameters.AddWithValue("@CartId", CartId);

                    //connection open 
                    Connection.Open();

                    // Read data form database
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //While Loop For Reading status result from SqlDataReader.
                    while (reader.Read())
                    {
                        details.OrderId = Convert.ToInt32(reader["OrderId"].ToString());
                        details.CartId = Convert.ToInt32(reader["CartId"].ToString());
                        details.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        details.BookId = Convert.ToInt32(reader["BookId"].ToString());
                        details.Title = reader["Title"].ToString();
                        details.Author = reader["Author"].ToString();
                        details.Address = reader["Address"].ToString();
                        details.City = reader["City"].ToString();
                        details.PhoneNumber = reader["PhoneNumber"].ToString();
                        details.TotalPrice = Convert.ToDouble(reader["TotalPrice"].ToString());
                        details.OrderPlaced = Convert.ToBoolean(reader["OrderPlaced"].ToString());
                        details.CreatedDate = Convert.ToDateTime(reader["CreatedDate"].ToString());
                        details.ModifiedDate = Convert.ToDateTime(reader["ModifiedDate"].ToString());
                    }

                    //connection close
                    Connection.Close();
                }
                return details;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
