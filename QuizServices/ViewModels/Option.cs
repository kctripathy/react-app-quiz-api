using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class Option
    {
        public long id
        {
            get; set;
        }

        public int questionId
        {
            get; set;
        }


        public string name
        {
            get; set;
        }

        public bool isAnswer
        {
            get; set;
        }
    }
}
