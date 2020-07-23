using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookStoreModel
{
    public class UpdateBookDetails
    {
        public string Title { get; set; }
       
        public string Description { get; set; }
        
        public string Author { get; set; }
        
        public int BooksAvailable { get; set; }
        
        public double Price { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
