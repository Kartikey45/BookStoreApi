using CommonLayer.CartModel;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class CartRL : ICartRL
    {
        //Configuration initialized
        private readonly IConfiguration Configuration;

        //constructor 
        public CartRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // Add to cart
        public AddToCartDetails AddToCart(int UserId, int BookId, int Quantity)
        {
            AddToCartDetails cart = new AddToCartDetails();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("AddToCart", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                    sqlCommand.Parameters.AddWithValue("@Quantity", Quantity);

                    //connection open 
                    Connection.Open();

                    // Read data form database
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //While Loop For Reading status result from SqlDataReader.
                    while (reader.Read())
                    {
                        cart.CartId = Convert.ToInt32(reader["CartId"].ToString());
                        cart.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        cart.BookId = Convert.ToInt32(reader["BookId"].ToString());
                        cart.Title = reader["Title"].ToString();
                        cart.Description = reader["Description"].ToString();
                        cart.Author = reader["Author"].ToString();
                        cart.Price = Convert.ToDouble(reader["Price"].ToString());
                        cart.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    }

                    //connection close
                    Connection.Close();
                }
                return cart;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //View Cart details by UserId
        public List<CustomerCartDetails> ViewCartDetails(int UserId)
        {
            List<CustomerCartDetails> list = new List<CustomerCartDetails>();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("ViewCartById", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);

                    //connection open 
                    Connection.Open();

                    // Read data form database
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //While Loop For Reading status result from SqlDataReader.
                    while (reader.Read())
                    {
                        CustomerCartDetails cart = new CustomerCartDetails();
                        cart.CartId = Convert.ToInt32(reader["CartId"].ToString());
                        cart.BookId = Convert.ToInt32(reader["BookId"].ToString());
                        cart.Title = reader["Title"].ToString();
                        cart.Author = reader["Author"].ToString();
                        cart.Price = Convert.ToDouble(reader["Price"].ToString());
                        cart.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                        cart.IsUsed = Convert.ToBoolean(reader["IsUsed"]);
                        cart.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                        cart.DateCreated = Convert.ToDateTime(reader["DateCreated"].ToString());
                        cart.DateModified = Convert.ToDateTime(reader["DateModified"].ToString());
                        list.Add(cart);
                    }

                    //Connection close
                    Connection.Close();
                }
                return list;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete cart details
        public Response DeleteFromCart(int UserId, int CartId)
        {
            Response response = new Response();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("DeleteFromCartById", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand.Parameters.AddWithValue("@CartId", CartId);

                    Connection.Open();

                    int data = 0;

                    data = sqlCommand.ExecuteNonQuery();
                    Connection.Close();

                    if (data == 1)
                    {
                        response.Status = "Deleted";
                    }
                    else
                    {
                        response.Status = "Not Deleted";
                    }
                }
                return response;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
