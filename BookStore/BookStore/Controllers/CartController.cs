using BusinessLayer.Interface;
using DatabaseLayer.Cart;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        ICartBL cartBL;
        public CartController(ICartBL cartBL)
        {
            this.cartBL = cartBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddToCart")]
        public IActionResult AddtoCart(CartPostModel cartPostModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                var result = this.cartBL.AddToCart(UserId, cartPostModel);
                if (result == true)
                {
                    return this.Ok(new { Status = true, Message = "Book is added to cart" });
                }
                else
                {
                    return this.BadRequest(new { Status = false, Message = "Adding to bag failed ! try again" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetCartBooks")]
        public IActionResult GetCart()
        {
            List<CartModel> carts = new List<CartModel>();
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = int.Parse(userId);
                carts = this.cartBL.GetCart(UserId);

                if (carts != null)
                {
                    return this.Ok(new { success = true, Message = "Cart Get All Successfully", data = carts });
                }
                else
                {
                    return this.BadRequest(new { success = false, Message = "Cart Get All Unsuccessfully " });

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
