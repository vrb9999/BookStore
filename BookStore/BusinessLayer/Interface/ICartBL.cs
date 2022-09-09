using DatabaseLayer.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface ICartBL
    {
        public bool AddToCart(int UserId, CartPostModel cartPostModel);

        public List<CartModel> GetCart(int UserId);
    }
}
