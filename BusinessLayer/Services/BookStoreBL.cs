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
        public Response InsertBooks(BookStoreDetails details)
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
    }
}
