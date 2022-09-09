using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Cart
{
    public class CartModel
    {
        public int CartId { get; set; }
        public int BookId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }


        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public double OriginalPrice { get; set; }
        public double DiscountPrice { get; set; }
        public string BookImg { get; set; }
    }
}
