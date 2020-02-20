using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizQuestions
    {
        public QuizQuestions()
        {
            QuizQuestionsOptions = new HashSet<QuizQuestionsOptions>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public long? CorrectAnswerOptionId { get; set; }
        public short? QuestionTypeId { get; set; }
        public int? ClassSubjectId { get; set; }
        public int? AccountId { get; set; }

        public QuizAccounts Account { get; set; }
        public QuizClassesSubject ClassSubject { get; set; }
        public QuizQuestionsTypes QuestionType { get; set; }
        public ICollection<QuizQuestionsOptions> QuizQuestionsOptions { get; set; }
    }
}
