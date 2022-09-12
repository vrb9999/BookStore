using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DatabaseLayer.Feedback
{
    public class FeedbackModel
    {
        [Required]
        public int BookId { get; set; }

        [Required]
        public int AvgRating { get; set; }

        [Required]
        [DefaultValue("")]
        public string Comments { get; set; }
    }
}
