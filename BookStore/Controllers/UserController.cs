using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        //Variable declared
        private readonly IUserBL UserBl;
        private readonly IConfiguration _configuration;

        //Instance of Sender class
        Sender sender = new Sender();

        //Constructor 
        public UserController(IUserBL UserBl, IConfiguration _configuration)
        {
            this.UserBl = UserBl;
            this._configuration = _configuration;
        }

        //Method to register user details 
        [HttpPost]
        [Route("Registration")]
        public IActionResult UserRegistration(UserRegistration user)
        {
            try
            {
                var data = UserBl.Registration(user);
                if (data.Email != null)
                {
                    
                    string MSMQ = "\n First Name : " + Convert.ToString(user.FirstName) +
                                    "\n Last Name : " + Convert.ToString(user.LastName) +
                                    "\n User Role : " + Convert.ToString("Customer") +
                                    "\n Email : " + Convert.ToString(user.Email);
                    sender.Message(MSMQ);
                    
                    return Ok(new { success = true, Message = "registration successfull", Data = data });
                }
                else
                {
                    return Conflict(new { success = false, Message = "registration failed" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method of User Login 
        [HttpPost]
        [Route("Login")]
        public IActionResult UserLogin(UserLogin login)
        {
            try
            {
                UserDetails data = UserBl.Login(login);
                
                bool success = false;
                string message;
                UserDetails DATA;

                UserDetails Data = new UserDetails()
                {
                    UserId = data.UserId,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    UserRole = data.UserRole,
                    Email = data.Email,
                    Address = data.Address,
                    City = data.City,
                    PhoneNumber = data.PhoneNumber
                };
                
                if (data.Email != null )
                {
                    string JsonToken = CreateToken(data, "AuthenticateUserRole");
                    success = true;
                    message = "Login Successfully";
                    DATA = Data;
                    return Ok(new { success, message, DATA, JsonToken });
                }
                else
                {
                    message = "Enter Valid Email & Password";
                    //DATA = login;
                    return NotFound(new { success, message });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method to create JWT token
        private string CreateToken(UserDetails responseData, string type)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, responseData.UserRole));
                claims.Add(new Claim("Email", responseData.Email.ToString()));
                claims.Add(new Claim("UserId", responseData.UserId.ToString()));
                claims.Add(new Claim("TokenType", type));
                claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

                var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                    _configuration["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddDays(1),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
