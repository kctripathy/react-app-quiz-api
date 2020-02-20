using System;
using System.Collections.Generic;

namespace QuizServices.Models
{
    public partial class QuizSubjects
    {
        public QuizSubjects()
        {
            QuizClassesSubject = new HashSet<QuizClassesSubject>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }

        public ICollection<QuizClassesSubject> QuizClassesSubject { get; set; }
    }
}
