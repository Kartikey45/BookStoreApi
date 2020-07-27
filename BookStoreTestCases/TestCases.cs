using BookStore.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using Experimental.System.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using Xunit;
using Xunit.Sdk;

namespace BookStoreTestCases
{
    public class TestCases
    {
        UserController userController;
        BookController bookController;
        CartController cartController;

        private readonly IUserBL _IUserBL;
        private readonly IBookStoreBL _IBookStoreBL;
        private readonly ICartBL _ICartBL;

        private readonly IUserRL _IUserRL;
        private readonly IBookStoreRL _IBookStoreRL;
        private readonly ICartRL _ICartRL;

        public readonly IConfiguration configuration;

        public TestCases()
        {
            IConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddJsonFile("appsettings.json");
            this.configuration = configurationBuilder.Build();


            _IUserRL = new UserRL(configuration);
            _IUserBL = new UserBL(_IUserRL);
            userController = new UserController(_IUserBL, configuration);
            
            _IBookStoreRL = new BookStoreRL(configuration);
            _IBookStoreBL = new BookStoreBL(_IBookStoreRL);
            bookController = new BookController(_IBookStoreBL);

            _ICartRL = new CartRL(configuration);
            _ICartBL = new CartBL(_ICartRL);
            cartController = new CartController(_ICartBL);
        }

        //variable declared
        bool SuccessTrue = true;
        bool SuccessFalse = false;

        //User Registration returns bad request
        [Fact]
        public void UserRegistration_Returns_BadRequest()
        {
            UserRegistration details = new UserRegistration()
            {
                FirstName = "",
                LastName = "",
                Email = "",
                Password = "",
                Address = "",
                City = "",
                PhoneNumber = ""
            };

            //Act
            var result = userController.UserRegistration(details) as BadRequestObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["message"].ToString();

            string message = "It cannot be Empty";
            
            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(SuccessFalse, responseSuccess);
            Assert.Equal(message, responseMessage);
        }

        //User Registration returns ok result
        [Fact]
        public void UserRegistration_Returns_Ok_Result()
        {
            UserRegistration info = new UserRegistration()
            {
                FirstName = "Vandana",
                LastName = "Karki",
                Email = "Vandana@gmail.com",
                Password = "vandana@123",
                Address = "Ratlam",
                City = "Ratlam",
                PhoneNumber = "8521479635"
            };

            //Act
            var result = userController.UserRegistration(info) as OkObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["Message"].ToString();
            var responseData = dataResponse["Data"].ToObject<UserRegistration>();

            string message = "registration successfull";
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(SuccessTrue, responseSuccess);
            Assert.Equal(message, responseMessage);
            Assert.Equal(info.FirstName, responseData.FirstName);
        }

        //User registration returns conflict
        [Fact]
        public void UserRegistration_returns_Conflict()
        {
            UserRegistration info = new UserRegistration()
            {
                FirstName = "Ruchika",
                LastName = "Ahire",
                Email = "ruchika@gmail.com",
                Password = "ruchika@123",
                Address = "Thane",
                City = "Mumbai",
                PhoneNumber = "8521479635"
            };

            //Act
            var result = userController.UserRegistration(info) as ConflictObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["Message"].ToString();
           

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(SuccessFalse, responseSuccess);
            Assert.Equal("registration failed", responseMessage);
        }

        //user login return ok result
        [Fact]
        public void UserLogin_returns_OkResult()
        {
            UserLogin login = new UserLogin()
            {
                Email = "singhkartikey45@gmail.com",
                Password = "kartikey@123"
            };

            //Act
            var result = userController.UserLogin(login) as OkObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["message"].ToString();
            var responseData = dataResponse["DATA"].ToObject<UserDetails>();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(SuccessTrue, responseSuccess);
            Assert.Equal("Login Successfully", responseMessage);
            Assert.Equal("Kartikey", responseData.FirstName);
        }

        //userlogin returns not found
        [Fact]
        public void UserLogin_returns_NotFound()
        {
            UserLogin login = new UserLogin()
            {
                Email = "singhkartikey45@gmail.com",
                Password = "nkjley@123"
            };

            //Act
            var result = userController.UserLogin(login) as NotFoundObjectResult ;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["message"].ToString();

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(SuccessFalse, responseSuccess);
            Assert.Equal("Enter Valid Email & Password", responseMessage);
        }

        //user login returns bad request
        [Fact]
        public void UserLogin_returns_BadRequest()
        {
            UserLogin login = new UserLogin()
            {
                Email = "singhkartikey45gmail.com",
                Password = "kartik123"
            };

            //Act
            var result = userController.UserLogin(login) as BadRequestObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["message"].ToString();

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(SuccessFalse, responseSuccess);
            Assert.Equal("Invalid Password type", responseMessage);
        }

        // insert book in book store returns ok result
        [Fact]
        public void InsertBookInBookStore_returns_Ok_Result()
        {
            BookStoreDetails details = new BookStoreDetails()
            {
                Title = "Harry Potter",
                Description = "It is a Movie.",
                Author = "JKRowling",
                BooksAvailable = 6,
                Price = 600.00
            };

            //Act
            var result = bookController.InsertBooks(details) as OkObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["Message"].ToString();
            var responseData = dataResponse["Data"].ToObject<BookStoreDetails>();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(SuccessTrue, responseSuccess);
            Assert.Equal("Book Inserted successfully", responseMessage);
            Assert.Equal(details.Title, responseData.Title);
        }

        // insert book in book store returns conflict
        [Fact]
        public void InsertBookInBookStore_Returns_Conflict()
        {
            BookStoreDetails details = new BookStoreDetails()
            {
                Title = "Harry Potter",
                Description = "It is a Movie.",
                Author = "JKRowling",
                BooksAvailable = 6,
                Price = 600.00
            };

            //Act
            var result = bookController.InsertBooks(details) as ConflictObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["Message"].ToString();

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
            Assert.Equal(SuccessFalse, responseSuccess);
            Assert.Equal("This Book is Already registered", responseMessage);
        }

        // insert book in book store returns badRequest
        [Fact]
        public void InsertInValidAuthorNameBookInBookStore_returns_BadRequest()
        {
            BookStoreDetails details = new BookStoreDetails()
            {
                Title = "",
                Description = "",
                Author = "Bruice Lee",
                BooksAvailable = 0,
                Price = 0.0
            };

            //Act
            var result = bookController.InsertBooks(details);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        // View all details returns ok result
        [Fact]
        public void ViewAllBookDetails_returns_OkResult()
        {
            //Act
            var result = bookController.ViewAllAbooks() as OkObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["message"].ToString();
            
            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(SuccessTrue, responseSuccess);
            Assert.Equal("Data fetched successfully", responseMessage);
        }

        //update book by id returns ok result
        [Fact]
        public void UpdateBookById_retursOkResult()
        {
            UpdateBookDetails details = new UpdateBookDetails()
            {
                Title = "The Great Gatsby",
                Description = "I'm honestly a little surprised to see Gatsby topping this list. I mean yes, sure, it's " +
                                    "absolutely one of the or whatever. ",
                Author = "ScottFitzgerald",
                BooksAvailable = 8,
                Price = 200.00
            };

            //Act
            var result = bookController.UpdateBooksDetails(8, details);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //update book by id returns conflict
        [Fact]
        public void UpdateBookById_returns_Conflict()
        {
            UpdateBookDetails details = new UpdateBookDetails()
            {
                Title = "The Great Gatsby",
                Description = "I'm honestly a little surprised to see Gatsby topping this list. I mean yes, sure, it's " +
                                    "absolutely one of the or whatever. ",
                Author = "ScottFitzgerald",
                BooksAvailable = 8,
                Price = 200.00
            };

            //Act
            var result = bookController.UpdateBooksDetails(50, details);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        // update book by id returns  bad request
        [Fact]
        public void UpdateBookById_returns_BadRequest()
        {
            UpdateBookDetails details = new UpdateBookDetails()
            {
                Title = "The Great Gatsby",
                Description = "I'm honestly a little surprised to see Gatsby topping this list. I mean yes, sure, it's " +
                                    "absolutely one of the or whatever. ",
                Author = "ScottFitzgerald",
                BooksAvailable = 8,
                Price = 200.00
            };

            //Act
            var result = bookController.UpdateBooksDetails(-5, details);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //Delete book by id returns ok result
        [Fact]
        public void DeleteBookById_returnsOkresult()
        {
            //Act
            var result = bookController.DeleteBookyId(7);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        // Delete book by id returns bad request
        [Fact]
        public void DeleteBookById_RetunsBadRequest()
        {
            //Act
            var result = bookController.DeleteBookyId(-7);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //Delete book by id returns not found
        [Fact]
        public void DeleteBookById_RetunsNotFound()
        {
            //Act
            var result = bookController.DeleteBookyId(100);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        // search book returns ok result
        [Fact]
        public void SearchBook_ReturnsOkResult()
        {
            //Act
            var result = bookController.BookSearch("house");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        //search book returns not found
        [Fact]
        public void SearchBook_returnsNotFound()
        {
            //Act
            var result = bookController.BookSearch("kartikey");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        //Sorting Book returns ok result
        [Fact]
        public void SortBook_returnsOkResult()
        {
            //Act
            var result = bookController.SortByBookDetails("Title", "Ascending");

            //Assert 
            Assert.IsType<OkObjectResult>(result);
        }

        //sorting book returns bad request
        [Fact]
        public void SortBook_returnsBadRequest()
        {
            //Act
            var result = bookController.SortByBookDetails("abc", "xyz");

            //Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //Add To cart  returns ok result
        [Fact]
        public void AddToCart_returnsOkResult()
        {
            Claim Name = new Claim("UserId", "25");
            ClaimsIdentity identity = new ClaimsIdentity();
            identity.AddClaim(Name);
            ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal();
            claimsPrincipal.AddIdentity(identity);
            var contextmock = new Mock<HttpContext>();
            contextmock.Setup(x => x.User).Returns(claimsPrincipal);
            cartController.ControllerContext.HttpContext = contextmock.Object;

            //Act
            var result = cartController.AddToCart(1,5) as OkObjectResult;
            var dataResponse = JToken.Parse(JsonConvert.SerializeObject(result.Value));
            var responseSuccess = dataResponse["success"].ToObject<bool>();
            var responseMessage = dataResponse["message"].ToString();
            //var responseData = dataResponse["Data"].ToString();

            //Assert
            Assert.IsType<OkObjectResult>(result);
            Assert.Equal(SuccessTrue, responseSuccess);
            Assert.Equal("successfully added to cart", responseMessage);
            //Assert.Equal(result., responseData);
        }
    }
}
