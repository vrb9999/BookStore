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
        public ActionResult AddtoCart(CartModel cart)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var res = this.cartBL.AddCart(cart, UserId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Book added to the cart successfully", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to add book to cart", data = res });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpPut("UpdateCart")]
        public ActionResult updateCart(int cartId, CartModel cart)
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);

                var res = this.cartBL.UpdateCart(cartId, cart, UserId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Updated cart successfully", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to update cart", data = res });

            }
            catch (Exception e)
            {

                throw e;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllBookFromCart")]
        public ActionResult GetAllBookFromCart()
        {
            try
            {
                var currentUser = HttpContext.User;
                int UserId = Convert.ToInt32(currentUser.Claims.FirstOrDefault(c => c.Type == "UserId").Value);
                var res = this.cartBL.GetAllCart(UserId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Getting all books from cart", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to get cart Items", data = res });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteCartItem")]
        public ActionResult DeleteCart(int cartId)
        {
            try
            {
                var res = this.cartBL.DeleteCart(cartId);

                if (res != null)
                {
                    return this.Ok(new { success = true, message = "Deleting books from cart", data = res });
                }
                return this.BadRequest(new { success = false, message = "Failed to delete cart Items", data = res });
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetCartById")]
        public IActionResult GetCartById(int CartId)
        {
            try
            {
                var result = this.cartBL.GetCartById(CartId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, message = $"Failed to get cart id = {CartId}" });
                }
                return this.Ok(new { success = true, message = $"Cart id = {CartId} fetched Successfully", data = result });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
