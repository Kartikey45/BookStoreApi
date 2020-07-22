﻿using CommonLayer.BookStoreModel;
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
        BooksDetails InsertBooks(BookStoreDetails details);

        //Method to get all book details
        List<BooksDetails> ViewAllBooks();

        //Method to Delete Book details
        Response DeleteBook(int BookId);

        //Method to update book
        BooksDetails UpdateBooks(int BookId, UpdateBookDetails details);

        //Method to search book 
        List<BooksDetails> BookSearch(string search);

        //Method to sort By book details
        List<Sort> SortByBookDetails(string columnName, string order);

        //Insert BookImage
        Storedetails InsertImage(IFormFile BookImage, int BookId);
    }
}
