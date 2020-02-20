using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class QuestionType
    {
        public int id
        {
            get; set;
        }

        public string name
        {
            get; set;
        }

        public bool isActive
        {
            get; set;
        }
    }
}
