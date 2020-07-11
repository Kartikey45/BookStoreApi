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

        //Add an employee's record to the database
        [HttpPost]
        [Route("")]
        public IActionResult UserRegistration(UserRegistration user)
        {
            var data = UserBl.Registration(user);
            return Ok(new { data = data });
        }
    }
}
