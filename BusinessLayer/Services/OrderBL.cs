using BusinessLayer.Interface;
using CommonLayer.Models;
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

        public Response CancellOrder(int UserId, int OrderId)
        {
            try
            {
                var data = order.CancellOrder(UserId, OrderId);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public OrderInfo OrderPlace(int UserId, int CartId, string Address, string City, int PinCode)
        {
            try
            {
                var data = order.OrderPlace(UserId, CartId, Address, City, PinCode);
                return data;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
