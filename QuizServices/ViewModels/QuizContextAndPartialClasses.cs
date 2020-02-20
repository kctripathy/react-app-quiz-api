using Microsoft.EntityFrameworkCore;
using QuizServices.Data;
using QuizServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.Models
{
    public partial class QuizContext
    {
        public virtual DbSet<QuestionAvailaleClassAndSubject> QuestionAvailaleClassAndSubjects { get; set; }
        public virtual DbSet<ClassSubject> ClassSubject { get; set; }
        public virtual DbSet<Question> Question { get; set; }
    }

    public partial class QuizAccounts : IEntity
    {

    }

    public partial class QuizClasses : IEntity
    {

    }

    public partial class QuizQuestions : IEntity
    {

    }

    public partial class QuizSubjects : IEntity
    {

    }

    public partial class QuizUsers : IEntity
    {
    }

    public partial class QuizClassesSubject: IEntity
    {

    }
}