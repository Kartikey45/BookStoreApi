using BusinessLayer.Interface;
using CommonLayer.CartModel;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class CartBL : ICartBL
    {
        //Initialise variable 
        private readonly ICartRL cartRL;

        //constructore declare
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public AddToCartDetails AddToCart(int UserId, int BookId, int Quantity)
        {
            try
            {
                var data = cartRL.AddToCart(UserId, BookId, Quantity);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<CustomerCartDetails> ViewCartDetails(int UserId)
        {
            try
            {
                var data = cartRL.ViewCartDetails(UserId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Response DeleteFromCart(int UserId, int CartId)
        {
            try
            {
                var data = cartRL.DeleteFromCart(UserId, CartId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public AddToCartDetails WishListToCart(int UserId, int WishListId)
        {
            try
            {
                var data = cartRL.WishListToCart(UserId, WishListId);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
