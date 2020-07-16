using CommonLayer.CartModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        // Add to cart
        AddToCartDetails AddToCart(int UserId, int BookId);

        // view form cart
        List<CustomerCartDetails> ViewCartDetails(int UserId);
    }
}
