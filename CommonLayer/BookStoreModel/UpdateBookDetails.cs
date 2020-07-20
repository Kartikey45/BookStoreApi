using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookStoreModel
{
    public class UpdateBookDetails
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int BooksAvailable { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; }

    }
}
