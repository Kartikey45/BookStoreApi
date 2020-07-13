using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IBookStoreRL
    {
        //Method to add books in book store
        Response InsertBooks(BookStoreDetails details);
    }
}
