using CommonLayer.Models;
using CommonLayer.WishListModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        // Add to WishList
        AddToWishListDetails AddToWishList(int UserId, int BookId, int Quantity);

        //View Cart details by UserId
        List<CustomerWishListDetails> ViewWishListDetails(int UserId);

        //Delete cart details
        Response DeleteFromWishList(int UserId, int WishListId);
    }
}
