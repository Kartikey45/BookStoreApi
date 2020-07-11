using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
         //Method to register user details
         Response Registration(UserRegistration user);
    }
}
