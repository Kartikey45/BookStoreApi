﻿using System;
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
        [Route("{BookId}")]
        [Authorize(Roles = "Customer")]
        public IActionResult AddToCart(int BookId)
        {
            try
            {
                var user = HttpContext.User;
                int UserId = Convert.ToInt32(user.Claims.FirstOrDefault(u => u.Type == "UserId").Value);
                var data = cartBL.AddToCart(UserId, BookId);
                if (data.Title != null)
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
    }
}
