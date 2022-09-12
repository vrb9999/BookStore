using DatabaseLayer.Feedback;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IFeedbackRL
    {
        public bool AddFeedback(int UserId, FeedbackModel feedbackModel);

        public List<GetFeedbackModel> GetAllFeedback(int BookId);
    }
}
