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

        public CartModel AddCart(CartModel cart, int UserId)
        {
            try
            {
                return this.cartRL.AddCart(cart, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<GetCartModel> GetAllCart(int UserId)
        {
            try
            {
                return this.cartRL.GetAllCart(UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string DeleteCart(int CartId)
        {
            try
            {
                return this.cartRL.DeleteCart(CartId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public CartModel UpdateCart(int CartId, CartModel cart, int UserId)
        {
            try
            {
                return this.cartRL.UpdateCart(CartId, cart, UserId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public GetCartModel GetCartById(int CartId)
        {
            try
            {
                return this.cartRL.GetCartById(CartId);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
