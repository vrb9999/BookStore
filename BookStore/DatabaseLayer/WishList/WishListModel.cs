using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.WishList
{
    public class WishListModel
    {
        public int WishListId { get; set; }

        public int UserId { get; set; }

        public int BookId { get; set; }

        public string BookName { get; set; }

        public string AuthorName { get; set; }

        public int OriginalPrice { get; set; }

        public int DiscountPrice { get; set; }

        public string BookImg { get; set; }
    }
}
