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
    }
}
