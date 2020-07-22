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
        //private readonly IConfiguration _configuration;

        //Constructor 
        public BookController(IBookStoreBL BookDetails )
        {
            this.BookDetails = BookDetails;
           
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
                if (data.Title != null)
                {
                    return Ok(new { success = true, Message = "Book Inserted successfully", Data = data });   
                }
                else
                {
                    return Conflict(new { success = false, Message = "This Book is Already registered" });
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
                if(BookId < 0)
                {
                    throw new Exception("Invalid BookId");
                }

                var data = BookDetails.UpdateBooks(BookId, details);
                if(data.Title != null)
                {
                    return Ok(new { success = true, messsage = "successfully updated", Data = data});
                }
                else
                {
                    return Conflict(new { success = false, messsage = "failed to update" });
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

        //Method to sort By book details
        [HttpGet]
        [Route("{columnName}/SortBy/{order}")]
        public IActionResult SortByBookDetails(string columnName, string order)
        {
            try
            {
                var data = BookDetails.SortByBookDetails(columnName, order);
                
                if(data != null)
                {
                    return Ok(new { success = true, message = "Successfull", Data = data});
                }
                else
                {
                    return NotFound(new { success = false, message = "failed" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method to search book
        [HttpGet]
        [Route("{Search}")]
        public IActionResult BookSearch(string Search)
        {
            try
            {
                var data = BookDetails.BookSearch(Search);
                if ( data != null)
                {
                    return Ok(new { success = true, message = "found", Data = data });
                }
                else
                {
                    return NotFound(new { success = false, message = "not found" });
                }
            }
            catch(Exception ex)
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
                if(BookId < 0)
                {
                    throw new Exception("Invalid Id");
                }

                var data = BookDetails.DeleteBook(BookId);
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
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        //Method to delete Book by id
        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("ImageInsert")]
        public IActionResult InsertImage(IFormFile BookImage, int BookId)
        {
            try
            {
                if(BookId < 0  || BookImage == null)
                {
                    throw new Exception("Given Invalid details");
                }
                var data = BookDetails.InsertImage(BookImage, BookId);
                if(data.Title != null)
                {
                    return Ok(new { success = true, message = "Image Inserted successfully", Data = data });
                }
                else
                {
                    return Conflict(new { success = false, messsage = "failed to insert image" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
