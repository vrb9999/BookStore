using BusinessLayer.Interface;
using DatabaseLayer.WishList;
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
    public class WishListController : ControllerBase
    {
        IWishListBL wishListBL;
        public WishListController(IWishListBL wishListBL)
        {
            this.wishListBL = wishListBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddTOWishList")]
        public IActionResult AddToWishList(WishListPostModel wishListPostModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.wishListBL.AddToWishList(UserId, wishListPostModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Check if Book is availbale OR it is already in WishList!!  WishListId : {wishListPostModel.BookId} to the WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"WishListId : {wishListPostModel.BookId} Added to WishList Sucessfull..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllWishList")]
        public IActionResult GetAllWishList()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                List<WishListModel> result = this.wishListBL.GetAllWishList(UserId);
                if (result == null)
                {
                    return this.BadRequest(new { success = false, Message = $"No Book available in WishList!!" });
                }

                return this.Ok(new { success = true, Message = $"Books in WishList fetched Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteWishList/{WishListId}")]
        public IActionResult DeleteWishList(int WishListId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.wishListBL.DeleteWishList(UserId, WishListId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Failed to remove from wishlist" });
                }

                return this.Ok(new { success = true, Message = $"WishListId : {WishListId} removed from WishList Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }    
}
