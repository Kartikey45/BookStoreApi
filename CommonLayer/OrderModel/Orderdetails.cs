using System;
using System.Collections.Generic;
using System.Text;

namespace CommonLayer.OrderModel
{
    public class Orderdetails
    {
        public int OrderId { get; set; }

        public int CartId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public string PhoneNumber { get; set; }

        public double TotalPrice { get; set; }

        public bool OrderPlaced { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}
