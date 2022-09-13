using BusinessLayer.Interface;
using DatabaseLayer.Order;
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
    public class OrderController : ControllerBase
    {
        IOrderBL orderBL;
        public OrderController(IOrderBL orderBL)
        {
            this.orderBL = orderBL;
        }

        [Authorize(Roles = Role.User)]
        [HttpPost("AddOrder")]
        public IActionResult AddOrder(OrderModel orderModel)
        {
            try
            {
                var result = this.orderBL.AddOrder(orderModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Check if Book is availbale in cart OR Check enough Books are in stock !! OR Check AddressId Exists!!" });
                }

                return this.Ok(new { success = true, Message = $"Order placed Sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllAddress()
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                List<GetOrderModel> result = this.orderBL.GetAllOrders(UserId);
                if (result.Count == 0)
                {
                    return this.BadRequest(new { success = false, Message = $"Failed to fetch orders for UserId : {UserId}" });
                }

                return this.Ok(new { success = true, Message = $"Order list fetched sucessfully for UserId : {UserId}", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize(Roles = Role.User)]
        [HttpDelete("DeleteOrder/{OrderId}")]
        public IActionResult DeleteOrder(int OrderId)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                bool result = orderBL.DeleteOrder(UserId, OrderId);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Failed to delete order for UserId : {UserId}!!" });
                }

                return this.Ok(new { success = true, Message = $"Order deleted sucessfully for UserId : {UserId}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
