using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizResultDetails
    {
        public int Id { get; set; }
        public int? QuizResultMasterId { get; set; }
        public int? QuestionId { get; set; }
        public long? OptionId { get; set; }
        public bool? IsCorrectAnswer { get; set; }
    }
}
