using DatabaseLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IWishListBL
    {
        public bool AddToWishList(int UserId, WishListPostModel wishListPostModel);
        public List<WishListModel> GetAllWishList(int UserId);
        public bool DeleteWishList(int UserId, int WishListId);
    }
}
