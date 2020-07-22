using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
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
    }
}
