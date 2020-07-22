using CommonLayer.Models;
using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IOrderBL
    {
        //Place order
        Orderdetails PlaceOrder(int UserId, int CartId);

        // View Order details
        List<Orderdetails> ViewOrderPlaced(int UserId);

        //Cancell Order
        Response CancellOrder(int UserId, int OrderId);
    }
}
