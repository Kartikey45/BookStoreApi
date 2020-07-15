using CommonLayer.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IUserRL
    {
        //Method to register user details
        UserDetails Registration(UserRegistration user);

        //Method for user login
        UserDetails Login(UserLogin user);
    }
}
