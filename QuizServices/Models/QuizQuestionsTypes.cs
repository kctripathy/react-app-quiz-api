using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizQuestionsTypes
    {
        public QuizQuestionsTypes()
        {
            QuizQuestions = new HashSet<QuizQuestions>();
        }

        public short Id { get; set; }
        public string Description { get; set; }

        public ICollection<QuizQuestions> QuizQuestions { get; set; }
    }
}
