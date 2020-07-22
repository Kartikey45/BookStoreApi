using BusinessLayer.Interface;
using CommonLayer.OrderModel;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class OrderBL : IOrderBL
    {
        //Initialise variable 
        private readonly IOrderRL order;

        //constructore declare
        public OrderBL(IOrderRL order)
        {
            this.order = order;
        }

        //place order
        public Orderdetails PlaceOrder(int UserId, int CartId)
        {
            try
            {
                var data = order.PlaceOrder(UserId, CartId);
                return data;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Orderdetails> ViewOrderPlaced(int UserId)
        {
            try
            {
                var data = order.ViewOrderPlaced(UserId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
