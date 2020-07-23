using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookStoreRL
    {
        //Method to add books in book store
        Storedetails InsertBooks(BookStoreDetails details);

        //Method to get all book details
        List<Storedetails> ViewAllBooks();

        //Method to Delete Book details
        Response DeleteBook(int BookId);

        //Method to update book
        Storedetails UpdateBooks(int BookId, UpdateBookDetails data);

        //Method to search book 
        List<Storedetails> BookSearch(string search);

        //Method to sort By book details
        List<Storedetails> SortByBookDetails(string columnName, string order);

        //Insert BookImage
        Storedetails InsertImage(IFormFile BookImage, int BookId);
    }
}
