using CommonLayer.Models;
using CommonLayer.WishListModel;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class WishListRL : IWishListRL
    {
        //Configuration initialized
        private readonly IConfiguration Configuration;

        //constructor 
        public WishListRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public AddToWishListDetails AddToWishList(int UserId, int BookId, int Quantity)
        {
            AddToWishListDetails WishList = new AddToWishListDetails();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("AddTowishList", Connection);
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
                        WishList.WishListId = Convert.ToInt32(reader["WishListId"].ToString());
                        WishList.UserId = Convert.ToInt32(reader["UserId"].ToString());
                        WishList.BookId = Convert.ToInt32(reader["BookId"].ToString());
                        WishList.Title = reader["Title"].ToString();
                        WishList.Description = reader["Description"].ToString();
                        WishList.Author = reader["Author"].ToString();
                        WishList.Price = Convert.ToDouble(reader["Price"].ToString());
                        WishList.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                    }

                    //connection close
                    Connection.Close();
                }
                return WishList;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerWishListDetails> ViewWishListDetails(int UserId)
        {
            List<CustomerWishListDetails> list = new List<CustomerWishListDetails>();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("ViewWishListById", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);

                    //connection open 
                    Connection.Open();

                    // Read data form database
                    SqlDataReader reader = sqlCommand.ExecuteReader();

                    //While Loop For Reading status result from SqlDataReader.
                    while (reader.Read())
                    {
                        CustomerWishListDetails WishList = new CustomerWishListDetails();
                        WishList.WishListId = Convert.ToInt32(reader["WishListId"].ToString());
                        WishList.BookId = Convert.ToInt32(reader["BookId"].ToString());
                        WishList.Title = reader["Title"].ToString();
                        WishList.Author = reader["Author"].ToString();
                        WishList.Price = Convert.ToDouble(reader["Price"].ToString());
                        WishList.Quantity = Convert.ToInt32(reader["Quantity"].ToString());
                        WishList.IsMoved = Convert.ToBoolean(reader["IsMoved"]);
                        WishList.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                        WishList.DateCreated = Convert.ToDateTime(reader["DateCreatedWL"].ToString());
                        WishList.DateModified = Convert.ToDateTime(reader["DateModifiedWL"].ToString());
                        list.Add(WishList);
                    }

                    //Connection close
                    Connection.Close();
                }
                return list;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response DeleteFromWishList(int UserId, int WishListId)
        {
            Response response = new Response();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("DeleteFromWishListById", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@UserId", UserId);
                    sqlCommand.Parameters.AddWithValue("@WishListId", WishListId);

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
