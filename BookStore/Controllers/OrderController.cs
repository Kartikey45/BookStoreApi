using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.OrderModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //Variable declared
        private readonly IOrderBL order;

        //Instance of Sender class
        Sender sender = new Sender();

        //Constructor 
        public OrderController(IOrderBL order)
        {
            this.order = order;
        }

        [HttpPost]
        [Route("{CartId}")]
        [Authorize(Roles = "Customer")]
        public IActionResult PlaceOrder(int CartId)
        {
            try
            {
                if (CartId < 1)
                {
                    throw new Exception("Invalid Cart Id");
                }
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = order.PlaceOrder(UserId, CartId);
                if (data.Title != null)
                {

                    string MSMQ = "\n Order Id : " + Convert.ToInt32(data.OrderId) +
                                  "\n Cart Id : " + Convert.ToInt32(data.CartId) +
                                  "\n User Id : " + Convert.ToInt32(data.UserId) +
                                  "\n Book Id : " + Convert.ToInt32(data.BookId) +
                                  "\n Title : " + Convert.ToString(data.Title) +
                                  "\n Author : " + Convert.ToString(data.Author) +
                                  "\n Address : " + Convert.ToString(data.Address) +
                                  "\n City : " + Convert.ToString(data.City) +
                                  "\n Phone Number : " + Convert.ToString(data.PhoneNumber) +
                                  "\n Total Price : " + Convert.ToDouble(data.TotalPrice) +
                                  "\n Ordered Placed : " + Convert.ToBoolean(data.OrderPlaced);
                    sender.Message(MSMQ);
                    return Ok(new { success = true, message = "Order Placed", UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "CartId  not found" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ViewOrderPlaced()
        {
            try
            {
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = order.ViewOrderPlaced(UserId);
                if (data != null)
                {
                    return Ok(new { success = true, message = "Order fetched successfully", UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "Orders not found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }

        [HttpPut]
        [Authorize(Roles = "Customer")]
        public IActionResult CancellOrder(int OrderId)
        {
            try
            {
                if(OrderId < 0)
                {
                    throw new Exception();
                }
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = order.CancellOrder(UserId, OrderId);
                if (data.Status == "Not Cancelled")
                {
                    return NotFound(new { success = false, Message = "Failed to Cancelled" });
                }
                else
                {
                    return Ok(new { success = true, Message = "Order Cancelled" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }
    }
}
