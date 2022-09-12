using BusinessLayer.Interface;
using DatabaseLayer.Feedback;
using RepositoryLayer.Interface;
using RepositoryLayer.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Service
{
    public class FeedbackBL : IFeedbackBL
    {
        IFeedbackRL feedbackRL;
        public FeedbackBL(IFeedbackRL feedbackRL)
        {
            this.feedbackRL = feedbackRL;
        }

        public bool AddFeedback(int UserId, FeedbackModel feedbackModel)
        {
            try
            {
                return this.feedbackRL.AddFeedback(UserId, feedbackModel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<GetFeedbackModel> GetAllFeedback(int BookId)
        {
            try
            {
                return this.feedbackRL.GetAllFeedback(BookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
