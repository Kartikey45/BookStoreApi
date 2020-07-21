using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.Models;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        // Add to cart
        AddToCartDetails AddToCart(int UserId, int BookId, int Quantity);

        // view from cart
        List<CustomerCartDetails> ViewCartDetails(int UserId);

        //Delete cart details
        Response DeleteFromCart(int UserId, int CartId);

        //Add to cart from Wish list
        AddToCartDetails WishListToCart(int UserId, int WishListId);
    }
}
