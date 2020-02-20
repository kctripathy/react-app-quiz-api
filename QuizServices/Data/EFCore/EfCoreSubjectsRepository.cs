using QuizServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.Data.EFCore
{
    public class EfCoreSubjectsRepository: EfCoreRepository<QuizSubjects, QuizContext>
    {
        public EfCoreSubjectsRepository(QuizContext context): base(context)
        {

        }
    }     
}
