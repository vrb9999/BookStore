using BusinessLayer.Interface;
using DatabaseLayer.WishList;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class WishListBL : IWishListBL
    {
        IWishListRL wishListRL;
        public WishListBL(IWishListRL wishListRL)
        {
            this.wishListRL = wishListRL;
        }

        public bool AddToWishList(int UserId, WishListPostModel wishListPostModel)
        {
            try
            {
                return this.wishListRL.AddToWishList(UserId, wishListPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<WishListModel> GetAllWishList(int UserId)
        {
            try
            {
                return this.wishListRL.GetAllWishList(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DeleteWishList(int UserId, int WishListId)
        {
            try
            {
                return this.wishListRL.DeleteWishList(UserId, WishListId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
