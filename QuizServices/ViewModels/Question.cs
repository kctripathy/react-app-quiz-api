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

        public string questionName
        {
            get; set;
        }

        public short questionTypeId
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

        public int? classId
        {
            get; set;
        }
        public int? subjectId
        {
            get; set;
        }
        public int? accountId
        {
            get; set;
        }

        public bool isActive { get; set; } = true;
    }
}
