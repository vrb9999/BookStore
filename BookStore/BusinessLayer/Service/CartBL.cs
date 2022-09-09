using BusinessLayer.Interface;
using DatabaseLayer.Cart;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class CartBL : ICartBL
    {
        ICartRL cartRL;
        public CartBL(ICartRL cartRL)
        {
            this.cartRL = cartRL;
        }

        public bool AddToCart(int UserId, CartPostModel cartPostModel)
        {
            try
            {
                return this.cartRL.AddToCart(UserId, cartPostModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<CartModel> GetCart(int UserId)
        {
            try
            {
                return this.cartRL.GetCart(UserId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
