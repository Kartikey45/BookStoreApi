using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
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
        private readonly IConfiguration _configuration;

        //Constructor 
        public CartController(ICartBL cartBL, IConfiguration _configuration)
        {
            this.cartBL = cartBL;
            this._configuration = _configuration;
        }

        [HttpPost]
        [Route("{BookId}")]
        public IActionResult AddToCart(int BookId)
        {
            try
            {
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = cartBL.AddToCart(UserId, BookId);
                if(data.Title != null)
                {
                    return Ok(new { success = true, message = "successfull" , UserId, Data = data });
                }
                else
                {
                    return NotFound(new { success = false , message = "Data not found" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { succcess = false, message = ex.Message});
            }
        }
    }
}
