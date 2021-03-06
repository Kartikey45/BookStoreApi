﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.CartModel
{
    public class AddToCartDetails
    {
        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }
    }
}
