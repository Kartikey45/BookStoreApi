using BookStore.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Services;
using CommonLayer.BookStoreModel;
using CommonLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using Xunit;

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
            ;
            _IBookStoreRL = new BookStoreRL(configuration);
            _IBookStoreBL = new BookStoreBL(_IBookStoreRL);
            bookController = new BookController(_IBookStoreBL);

            _ICartRL = new CartRL(configuration);
            _ICartBL = new CartBL(_ICartRL);
            cartController = new CartController(_ICartBL);
        }

        //User Registration returns bad request
        [Fact]
        public void UserRegistration_Returns_BadRequest()
        {
            UserRegistration details = null;

            //Act
            var result = userController.UserRegistration(details);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        //User Registration returns ok result
        [Fact]
        public void UserRegistration_Returns_Ok_Result()
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
            var result = userController.UserRegistration(info);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

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
            var result = userController.UserRegistration(info);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

        [Fact]
        public void UserLogin_returns_OkResult()
        {
            UserLogin login = new UserLogin()
            {
                Email = "singhkartikey45@gmail.com",
                Password = "kartikey@123"
            };

            //Act
            var result = userController.UserLogin(login);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UserLogin_returns_NotFound()
        {
            UserLogin login = new UserLogin()
            {
                Email = "singhkartikey45@gmail.com",
                Password = "nkjley@123"
            };

            //Act
            var result = userController.UserLogin(login);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void UserLogin_returns_BadRequest()
        {
            UserLogin login = new UserLogin()
            {
                Email = "singhkartikey45gmail.com",
                Password = "kartik123"
            };

            //Act
            var result = userController.UserLogin(login);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

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
            var result = bookController.InsertBooks(details);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

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
            var result = bookController.InsertBooks(details);

            //Assert
            Assert.IsType<ConflictObjectResult>(result);
        }

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

        [Fact]
        public void ViewAllBookDetails_returns_OkResult()
        {
            //Act
            var result = bookController.ViewAllAbooks();

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

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

        [Fact]
        public void DeleteBookById_returnsOkresult()
        {
            //Act
            var result = bookController.DeleteBookyId(7);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void DeleteBookById_RetunsBadRequest()
        {
            //Act
            var result = bookController.DeleteBookyId(-7);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void DeleteBookById_RetunsNotFound()
        {
            //Act
            var result = bookController.DeleteBookyId(100);

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void SearchBook_ReturnsOkResult()
        {
            //Act
            var result = bookController.BookSearch("house");

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SearchBook_returnsNotFound()
        {
            //Act
            var result = bookController.BookSearch("kartikey");

            //Assert
            Assert.IsType<NotFoundObjectResult>(result);
        }

        [Fact]
        public void SortBook_returnsOkResult()
        {
            //Act
            var result = bookController.SortByBookDetails("Title", "Ascending");

            //Assert 
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void SortBook_returnsBadRequest()
        {
            //Act
            var result = bookController.SortByBookDetails("abc", "xyz");

            //Assert 
            Assert.IsType<BadRequestObjectResult>(result);
        }
    }
}
