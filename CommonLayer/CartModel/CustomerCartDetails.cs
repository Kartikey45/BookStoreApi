using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.CartModel
{
    public class CustomerCartDetails
    {
        public int CartId { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public Boolean IsUsed { get; set; }

        public Boolean IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
