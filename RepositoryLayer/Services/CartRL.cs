﻿using CommonLayer.CartModel;
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
        public AddToCartDetails AddToCart(int UserId, int BookId)
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
    }
}