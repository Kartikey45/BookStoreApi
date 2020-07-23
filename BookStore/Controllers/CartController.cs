using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        //Variable declared
        private readonly ICartBL cartBL;
        //private readonly IConfiguration _configuration;

        //Constructor 
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Customer")]
        public IActionResult AddToCart(int BookId, int Quantity)
        {
            try
            {
                if(BookId < 1)
                {
                    throw new Exception("Invalid Book Id");
                }

                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = cartBL.AddToCart(UserId, BookId, Quantity);
                if (data.Title != null)
                {
                    return Ok(new { success = true, message = "successfull", UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "Book is not available in store" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ViewCartDetails()
        {
            try
            {
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = cartBL.ViewCartDetails(UserId);
                if (data != null)
                {
                    return Ok(new { success = true, message = "successfull", UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "Data not found" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Customer")]
        [Route("{CartId}")]
        public IActionResult DeleteFromCart(int CartId)
        {
            try
            {
                if (CartId < 0)
                {
                    throw new Exception("Invalid Cart Id");
                }
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = cartBL.DeleteFromCart(UserId, CartId);
                if (data.Status == "Not Deleted")
                {
                    return NotFound(new { success = false, Message = "Failed to delete" });
                }
                else
                {
                    return Ok(new { success = true, Message = "deleted successfully" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }

        [HttpPost]
        [Route("WishListToCart/{WishListId}")]
        [Authorize(Roles = "Customer")]
        public IActionResult WishListToCart(int WishListId)
        {
            try
            {
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = cartBL.WishListToCart(UserId, WishListId);
                if (data.Title != null)
                {
                    return Ok(new { success = true, message = "successfull", UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "Book is not available in store" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message });
            }
        }
    }
}
