using BusinessLayer.Interface;
using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class BookStoreBL : IBookStoreBL
    {
        //Initialise variable 
        private readonly IBookStoreRL BookDetails;

        //constructore declare
        public BookStoreBL(IBookStoreRL BookDetails)
        {
            this.BookDetails = BookDetails;
        }

        //Method to insert book
        public BooksDetails InsertBooks(BookStoreDetails details)
        {
            try
            {
                var result = BookDetails.InsertBooks(details);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Get all Books details
        public List<BooksDetails> ViewAllBooks()
        {
            try
            {
                var result = BookDetails.ViewAllBooks();
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to sort By book details
        public List<Sort> SortByBookDetails(string columnName, string order)
        {
            try
            {
                var data = BookDetails.SortByBookDetails(columnName, order);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Delete Book By Id
        public Response DeleteBook(int BookId)
        {
           try
            {
                var result = BookDetails.DeleteBook(BookId);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to update book
        public BooksDetails UpdateBooks(int BookId, UpdateBookDetails details)
        {
            try
            {
                var data = BookDetails.UpdateBooks(BookId, details);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to serach book
        public List<BooksDetails> BookSearch(string search)
        {
            try
            {
                var data = BookDetails.BookSearch(search);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
