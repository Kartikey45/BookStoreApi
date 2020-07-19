using CommonLayer.CartModel;
using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        // Add to cart
        AddToCartDetails AddToCart(int UserId, int BookId, int Quantity);

        // view from cart
        List<CustomerCartDetails> ViewCartDetails(int UserId);

        //Delete cart details
        Response DeleteFromCart(int UserId, int CartId);
    }
}
