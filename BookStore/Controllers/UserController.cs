using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Variable declared
        private readonly IUserBL UserBl;

        //Constructor 
        public UserController(IUserBL UserBl)
        {
            this.UserBl = UserBl;
        }

        //Method to register user details 
        [HttpPost]
        [Route("")]
        public IActionResult UserRegistration(UserRegistration user)
        {
            try
            {
                var data = UserBl.Registration(user);
                if (data.Status == "Invalid Email")
                {
                    return Ok(new { success = false, Message = "registration failed" });
                }
                else
                {
                    return Ok(new { success = true, Message = "registration successfull"});
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
