﻿using BusinessLayer.Interface;
using CommonLayer.CartModel;
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

        public AddToCartDetails AddToCart(int UserId, int BookId)
        {
            try
            {
                var data = cartRL.AddToCart(UserId, BookId);
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
    }
}
