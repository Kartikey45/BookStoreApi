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
    }
}
