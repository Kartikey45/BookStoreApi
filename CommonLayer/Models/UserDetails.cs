﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CommonLayer.Models
{
    public class UserDetails
    {
        //public string Status { get; set; }


        public int UserId { get; set; }

        
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

       
        public string UserRole { get; set; }

        
        public string Email { get; set; }

       
        //public string Password { get; set; }

        
        public string Address { get; set; }

        
        public string City { get; set; }

        
        public string PhoneNumber { get; set; }


        //public DateTime CreatedDate { get; set; }


        //public DateTime ModifiedDate { get; set; }
    }
}
