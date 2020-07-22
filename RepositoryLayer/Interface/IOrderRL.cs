using CommonLayer.Models;
using CommonLayer.OrderModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IOrderRL
    {
        //Place order
        Orderdetails PlaceOrder(int UserId, int CartId);

        // View Order details
        List<Orderdetails> ViewOrderPlaced(int UserId);

        //Cancell Order
        Response CancellOrder(int UserId, int OrderId);
    }
}
