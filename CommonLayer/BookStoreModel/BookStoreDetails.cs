using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.BookStoreModel
{
    public class BookStoreDetails
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage = "First Letter would be capital")]
        public string Author { get; set; }

        [Required]
        public int BooksAvailable { get; set; }

        [Required]
        public double Price { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
