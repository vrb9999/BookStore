using DatabaseLayer.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public CartModel AddCart(CartModel cart, int UserId);
        public List<GetCartModel> GetAllCart(int UserId);
        public string DeleteCart(int CartId);
        public CartModel UpdateCart(int CartId, CartModel cart, int UserId);
    }
}
