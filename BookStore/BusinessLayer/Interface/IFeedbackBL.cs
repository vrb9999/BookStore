using DatabaseLayer.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLayer.Interface
{
    public interface IFeedbackBL
    {
        public bool AddFeedback(int UserId, FeedbackModel feedbackModel);

        public List<GetFeedbackModel> GetAllFeedback(int BookId);
    }
}
