using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.BookStoreModel
{
    public class Sort
    {
        public int BookId { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }

        
        
        public string Author { get; set; }

       
        public int BooksAvailable { get; set; }

      
        public double Price { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
