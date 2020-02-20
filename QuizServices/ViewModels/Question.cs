using QuizServices.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.ViewModels
{
    public class Question: IEntity
    {
        public int Id { get; set;} //Just for IEntity

        //public int id
        //{
        //    get; set;
        //}

        public string name
        {
            get; set;
        }

        public int questionTypeId
        {
            get; set;
        }

        public List<Option> options
        {
            get; set;
        }

        public QuestionType questionType
        {
            get; set;
        }

        public int? classSubjectId
        {
            get; set;
        }

        public int? accountId
        {
            get; set;
        }
    }
}
