using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class QuizQuestionsViewModel
    {
        public int id
        {
            get; set;
        }

        public string name
        {
            get; set;
        }
        public string description
        {
            get; set;
        }
        public string classname
        {
            get; set;
        }

        public int totalQuestions
        {
            get; set;
        }

        public List<Question> questions
        {
            get; set;
        }
    }
}
