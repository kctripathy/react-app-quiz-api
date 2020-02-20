using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizQuestionsOptions
    {
        public long Id { get; set; }
        public int QuestionId { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }

        public QuizQuestions Question { get; set; }
    }
}
