using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizClasses
    {
        public QuizClasses()
        {
            QuizClassesSubject = new HashSet<QuizClassesSubject>();
        }

        public int Id { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<QuizClassesSubject> QuizClassesSubject { get; set; }
    }
}
