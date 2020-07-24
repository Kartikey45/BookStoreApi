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

        //Place order from different Address
        OrderInfo OrderPlace(int UserId, int CartId, string Address, string City, int PinCode);

        // View Order details
        List<Orderdetails> ViewOrderPlaced(int UserId);

        //Cancell Order
        Response CancellOrder(int UserId, int OrderId);
    }
}
