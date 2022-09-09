using DatabaseLayer.WishList;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IWishListRL
    {
        public bool AddToWishList(int UserId, WishListPostModel wishListPostModel);
        public List<WishListModel> GetAllWishList(int UserId);
        public bool DeleteWishList(int UserId, int WishListId);
    }
}
