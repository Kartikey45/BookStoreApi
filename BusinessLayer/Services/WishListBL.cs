using BusinessLayer.Interface;
using CommonLayer.Models;
using CommonLayer.WishListModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class WishListBL : IWishListBL
    {
        //Initialise variable 
        private readonly IWishListRL WishListRL;

        //constructore declare
        public WishListBL(IWishListRL WishListRL)
        {
            this.WishListRL = WishListRL;
        }

        public AddToWishListDetails AddToWishList(int UserId, int BookId, int Quantity)
        {
            try
            {
                var data = WishListRL.AddToWishList(UserId,BookId,Quantity);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerWishListDetails> ViewWishListDetails(int UserId)
        {
            try
            {
                var data = WishListRL.ViewWishListDetails(UserId);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response DeleteFromWishList(int UserId, int WishListId)
        {
            try
            {
                var data = WishListRL.DeleteFromWishList(UserId, WishListId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
