using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace RepositoryLayer.Services
{
    public class BookStoreRL : IBookStoreRL
    {
        //Configuration initialized
        private readonly IConfiguration Configuration;

        //constructor 
        public BookStoreRL(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Method to insert books
        public Response InsertBooks(BookStoreDetails details)
        {
            Response response = new Response();

            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                DateTime createdDate = details.CreatedDate;
                createdDate = DateTime.Now;

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("InsertBooksInBookStore", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@Title", details.Title);
                    sqlCommand.Parameters.AddWithValue("@Description", details.Description);
                    sqlCommand.Parameters.AddWithValue("@Author", details.Author);
                    sqlCommand.Parameters.AddWithValue("@BooksAvailable", details.BooksAvailable);
                    sqlCommand.Parameters.AddWithValue("@Price", details.Price);
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
                        response.Status = "Valid";
                    }
                    else
                    {
                        response.Status = "Invalid";
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
