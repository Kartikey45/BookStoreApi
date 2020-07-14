using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer.Interface;
using CommonLayer.BookStoreModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
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
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method to update book details
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{BookId}")]
        public IActionResult UpdateBooksDetails(int BookId, UpdateBookDetails details)
        {
            try
            {
                var data = BookDetails.UpdateBooks(BookId, details);
                if(data.Status == "failed")
                {
                    return Ok(new { success = false, messsage = "failed to update data" });
                }
                else
                {
                    return Ok(new { success = true, messsage = "successfully updated" });
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
                    return Ok(new { success = true, message = "successfull", Data = data });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method to delete Book by id
        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{BookId}")]
        public IActionResult DeleteBookyId(int BookId)
        {
            try
            {
                var data = BookDetails.DeleteBook(BookId);
                if(data.Status == "Not Deleted")
                {
                    return Ok(new { success = false, Message = "Failed to delete" });
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
