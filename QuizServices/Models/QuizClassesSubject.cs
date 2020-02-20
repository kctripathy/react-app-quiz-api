using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizClassesSubject
    {
        public QuizClassesSubject()
        {
            QuizQuestions = new HashSet<QuizQuestions>();
        }

        public int Id { get; set; }
        public int ClassId { get; set; }
        public int SubjectId { get; set; }
        public bool? IsActive { get; set; }
        public int? AccountId { get; set; }

        public QuizClasses Class { get; set; }
        public QuizSubjects Subject { get; set; }
        public ICollection<QuizQuestions> QuizQuestions { get; set; }
    }
}
