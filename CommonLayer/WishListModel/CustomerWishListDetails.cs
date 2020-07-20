using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.WishListModel
{
    public class CustomerWishListDetails
    {
        public int WishListId { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

        public bool IsMoved { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }
    }
}
