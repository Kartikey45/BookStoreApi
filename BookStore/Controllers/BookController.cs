using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.BookStoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        //Variable declared
        private readonly IBookStoreBL BookDetails;
        private readonly IConfiguration _configuration;

        //Constructor 
        public BookController(IBookStoreBL BookDetails, IConfiguration _configuration)
        {
            this.BookDetails = BookDetails;
            this._configuration = _configuration;
        }

        //Method to register Book details 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("")]
        public IActionResult InsertBooks(BookStoreDetails details)
        {
            try
            {
                var data = BookDetails.InsertBooks(details);
                if (data.Status == "Invalid")
                {
                    return Ok(new { success = false, Message = "Books are already in the slot" });
                }
                else
                {
                    return Ok(new { success = true, Message = "Book Inserted" }); 
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method to Get all Books details
        [HttpGet]
        [Route("")]
        public IActionResult ViewAllAbooks()
        {
            try
            {
                var data = BookDetails.ViewAllBooks();
                if (data == null)
                {
                    return NotFound();
                }
                else
                {
                    return Ok(new { success = true, message = "successfull" , Data = data});
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false , message = ex.Message });
            }
        }
    }
}
