using BookStore.Controllers;
using BusinessLayer.Interface;
using BusinessLayer.Services;
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
    }
}
