using DatabaseLayer.Cart;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface ICartRL
    {
        public bool AddToCart(int UserId, CartPostModel cartPostModel);

        public List<CartModel> GetCart(int UserId);
    }
}
