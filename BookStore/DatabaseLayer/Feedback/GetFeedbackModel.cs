using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseLayer.Feedback
{
    public class GetFeedbackModel
    {
        public int FeedbackId { get; set; }

        public string Comments { get; set; }

        public int AvgRating { get; set; }

        public int BookId { get; set; }

        public int UserId { get; set; }

        public string FullName { get; set; }
    }
}
