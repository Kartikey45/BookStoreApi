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
    }
}
