using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        // Add to cart
        AddToCartDetails AddToCart(int UserId, int BookId);
    }
}
