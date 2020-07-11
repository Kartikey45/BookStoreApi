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

        //Method to register user details
        public Response Registration(UserRegistration user)
        {
            try
            {
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
