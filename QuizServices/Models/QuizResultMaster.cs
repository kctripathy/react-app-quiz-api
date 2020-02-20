using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizResultMaster
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int? QuizMasterId { get; set; }
        public int? ClassSubjectId { get; set; }
        public short? TotalQuestions { get; set; }
        public short? TotalQuestionsAppeared { get; set; }
        public short? TotalAnswerCorrect { get; set; }
        public short? TotalAnswerWrong { get; set; }
        public DateTime? QuizAppearDate { get; set; }
    }
}
