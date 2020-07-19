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
        public BooksDetails InsertBooks(BookStoreDetails details)
        {
            BooksDetails data = new BooksDetails();

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

                    //int status = 0;

                    
                    //Read the data by using sql command
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        
                        data.BookId = Convert.ToInt32(dataReader["BookId"].ToString());
                        data.Title = dataReader["Title"].ToString();
                        data.Description = dataReader["Description"].ToString();
                        data.Author = dataReader["Author"].ToString();
                        data.BooksAvailable = Convert.ToInt32(dataReader["BooksAvailable"].ToString());
                        data.Price = Convert.ToDouble(dataReader["Price"].ToString());
                        data.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"].ToString());
                        
                    }

                    //connection close
                    Connection.Close();

                }
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to vieww all books
        public List<BooksDetails> ViewAllBooks()
        {
            // Creat list of recoeds of all the books
            List<BooksDetails> list = new List<BooksDetails>();

            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    //Calling stored procedure
                    SqlCommand sqlCommand = new SqlCommand("ViewAllBooks", Connection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    //Connection open
                    Connection.Open();

                    //Read the data by using sql command
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        BooksDetails details = new BooksDetails();
                        details.BookId = Convert.ToInt32(dataReader["BookId"].ToString()); 
                        details.Title = dataReader["Title"].ToString();
                        details.Description = dataReader["Description"].ToString();
                        details.Author = dataReader["Author"].ToString();
                        details.BooksAvailable = Convert.ToInt32(dataReader["BooksAvailable"].ToString());
                        details.Price = Convert.ToDouble(dataReader["Price"].ToString());
                        details.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"].ToString());
                        list.Add(details);
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

        //Method to sort By book details
        public List<Sort> SortByBookDetails(string columnName, string order)
        {
            List<Sort> list = new List<Sort>(); 
            try
            {

                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    //Calling stored procedure
                    SqlCommand sqlCommand = new SqlCommand("SortedBooksDetails", Connection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@SortCol", columnName);
                    sqlCommand.Parameters.AddWithValue("@SortDir", order);

                    //Connection open
                    Connection.Open();

                    //Read the data by using sql command
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {
                        Sort sort = new Sort();
                        sort.BookId = Convert.ToInt32(dataReader["BookId"].ToString());
                        sort.Title = dataReader["Title"].ToString();
                        sort.Description = dataReader["Description"].ToString();
                        sort.Author = dataReader["Author"].ToString();
                        sort.BooksAvailable = Convert.ToInt32(dataReader["BooksAvailable"].ToString());
                        sort.Price = Convert.ToDouble(dataReader["Price"].ToString());
                        //sort.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"].ToString());
                        //sort.ModifiedDate = Convert.ToDateTime(dataReader["ModifiedDate"].ToString());
                        list.Add(sort);
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

        //method to search book
        public BooksDetails BookSearch(string search)
        {
            //Create instance of Model class
            BooksDetails details = new BooksDetails();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("BookSearch", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;

                    sqlCommand.Parameters.AddWithValue("@search", search);
                    //sqlCommand.Parameters.AddWithValue("@Description", search.Description);
                    //sqlCommand.Parameters.AddWithValue("@Author", search.Description);

                    Connection.Open();
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();
                    while (dataReader.Read())
                    {
                        details.BookId = Convert.ToInt32(dataReader["BookId"].ToString());
                        details.Title = dataReader["Title"].ToString();
                        details.Description = dataReader["Description"].ToString();
                        details.Author = dataReader["Author"].ToString();
                        details.BooksAvailable = Convert.ToInt32(dataReader["BooksAvailable"].ToString());
                        details.Price = Convert.ToDouble(dataReader["Price"].ToString());
                    }
                }
                return details;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to delete book By id
        public Response DeleteBook(int BookId)
        {
            Response response = new Response();
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("DeleteBookById", Connection);
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);

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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to update book
        public BooksDetails UpdateBooks(int BookId, UpdateBookDetails details)
        {
            BooksDetails data = new BooksDetails();
            
            try
            {
                //Connection string declared
                string connect = Configuration.GetConnectionString("MyConnection");

                DateTime modifiedDate = details.ModifiedDate;
                modifiedDate = DateTime.Now;

                using (SqlConnection Connection = new SqlConnection(connect))
                {
                    SqlCommand sqlCommand = new SqlCommand("UpdatBookDetails", Connection);

                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.AddWithValue("@BookId", BookId);
                    sqlCommand.Parameters.AddWithValue("@Title", details.Title);
                    sqlCommand.Parameters.AddWithValue("@Description", details.Description);
                    sqlCommand.Parameters.AddWithValue("@Author", details.Author);
                    sqlCommand.Parameters.AddWithValue("@BooksAvailable", details.BooksAvailable);
                    sqlCommand.Parameters.AddWithValue("@Price", details.Price);
                    sqlCommand.Parameters.AddWithValue("@ModifiedDate", modifiedDate);

                    //connection open 
                    Connection.Open();

                    //Read the data by using sql command
                    SqlDataReader dataReader = sqlCommand.ExecuteReader();

                    while (dataReader.Read())
                    {

                        data.BookId = Convert.ToInt32(dataReader["BookId"].ToString());
                        data.Title = dataReader["Title"].ToString();
                        data.Description = dataReader["Description"].ToString();
                        data.Author = dataReader["Author"].ToString();
                        data.BooksAvailable = Convert.ToInt32(dataReader["BooksAvailable"].ToString());
                        data.Price = Convert.ToDouble(dataReader["Price"].ToString());
                        //data.CreatedDate = Convert.ToDateTime(dataReader["CreatedDate"].ToString());

                    }

                    //connection close
                    Connection.Close();
                }
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
