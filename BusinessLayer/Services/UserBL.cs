using BusinessLayer.Interface;
using CommonLayer.Models;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Services
{
    public class UserBL : IUserBL
    {
       

        //Initialise variable 
        private readonly IUserRL UserDetails;

        //constructore declare
        public UserBL(IUserRL UserDetails)
        {
            this.UserDetails = UserDetails;
        }

        //Method to Login user
        public UserDetails Login(UserLogin user)
        {
            try
            {
                var result = UserDetails.Login(user);
               
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //Method to register user details
        public UserDetails Registration(UserRegistration user)
        {
          
            try
            {
                if(user == null)
                {
                    throw new Exception("It cannot be null");
                }
                else if(user.FirstName == "" || user.LastName == "" || user.Email == "" || user.Password == "" || user.Address == "" || user.City == "" || user.PhoneNumber == "")
                {
                    throw new Exception("It cannot be Empty");
                }

                var result = UserDetails.Registration(user);
                return result;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
