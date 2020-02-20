using QuizServices.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.Data.EFCore
{
    public class EfCoreClassesRepository : EfCoreRepository<QuizClasses, QuizContext>
    {
        public EfCoreClassesRepository(QuizContext context) : base(context)
        {

        }
    }
}
