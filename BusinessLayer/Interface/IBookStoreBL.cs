using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IBookStoreBL
    {
        //Method to add books in book store
        BooksDetails InsertBooks(BookStoreDetails details);

        //Method to get all book details
        List<BooksDetails> ViewAllBooks();

        //Method to Delete Book details
        Response DeleteBook(int BookId);

        //Method to update Book details
        BooksDetails UpdateBooks(int BookId, UpdateBookDetails details);

        //Method to search book 
        BooksDetails BookSearch(string search);

        //Method to sort By book details
        List<Sort> SortByBookDetails(string columnName, string order);
    }
}
