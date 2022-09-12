using BusinessLayer.Interface;
using DatabaseLayer.Feedback;
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
    public class FeedbackController : ControllerBase
    {
        IFeedbackBL feedbackBL;
        public FeedbackController(IFeedbackBL feedbackBL)
        {
            this.feedbackBL = feedbackBL;
        }

        [Authorize]
        [HttpPost("AddFeedback")]
        public IActionResult AddFeedback(FeedbackModel feedbackModel)
        {
            try
            {
                var identity = User.Identity as ClaimsIdentity;
                IEnumerable<Claim> claims = identity.Claims;
                var userId = claims.Where(p => p.Type == @"UserId").FirstOrDefault()?.Value;
                int UserId = Convert.ToInt32(userId);
                var result = this.feedbackBL.AddFeedback(UserId, feedbackModel);
                if (result == false)
                {
                    return this.BadRequest(new { success = false, Message = $"Failed to add Feedback" });
                }

                return this.Ok(new { success = true, Message = $"Feedback added for BookId : {feedbackModel.BookId} sucessfully..." });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet("GetAllFeedback/{BookId}")]
        public IActionResult GetAllFeddback(int BookId)
        {
            try
            {
                List<GetFeedbackModel> result = this.feedbackBL.GetAllFeedback(BookId);
                if (result.Count == 0)
                {
                    return this.BadRequest(new { success = false, Message = $"No Feedbacks available for BookId:{BookId}" });
                }

                return this.Ok(new { success = true, Message = $"All Feedbacks fetched for BookId : {BookId} Sucessfully...", data = result });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
