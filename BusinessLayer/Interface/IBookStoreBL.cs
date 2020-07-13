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
        Response InsertBooks(BookStoreDetails details);

        //Method to get all book details
        List<BooksDetails> ViewAllBooks();

        //Method to Delete Book details
        Response DeleteBook(int BookId);
    }
}
