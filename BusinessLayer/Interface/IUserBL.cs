using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IUserBL
    {
         //Method to add user details
         UserRegistration Registration(UserRegistration user);
    }
}
