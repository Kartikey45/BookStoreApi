using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        //Variable declared
        private readonly IWishListBL WishListBL;
        //private readonly IConfiguration _configuration;

        //Constructor 
        public WishListController(IWishListBL WishListBL)
        {
            this.WishListBL = WishListBL;
        }


        [HttpPost]
        [Route("")]
        [Authorize(Roles = "Customer")]
        public IActionResult AddToWishList(int BookId, int Quantity)
        {
            try
            {
                if (BookId < 1)
                {
                    throw new Exception("Invalid Book Id");
                }

                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = WishListBL.AddToWishList(UserId, BookId, Quantity);

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
                return BadRequest(new { success = false, message = ex.Message});
            }
        }

        [HttpGet]
        [Authorize(Roles = "Customer")]
        public IActionResult ViewWishListDetails()
        {
            try
            {
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = WishListBL.ViewWishListDetails(UserId);
                if (data != null)
                {
                    return Ok(new { success = true, message = "successfull", UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "Data not found" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Customer")]
        [Route("{WishListId}")]
        public IActionResult DeleteFromWishList(int WishListId)
        {
            try
            {
                if (WishListId < 0)
                {
                    throw new Exception("Invalid WishList Id");
                }
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = WishListBL.DeleteFromWishList(UserId,WishListId);
                if (data.Status == "Not Deleted")
                {
                    return NotFound(new { success = false, Message = "Failed to delete" });
                }
                else
                {
                    return Ok(new { success = true, Message = "deleted successfully" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
