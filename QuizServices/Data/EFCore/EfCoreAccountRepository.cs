using QuizServices.Models;
using QuizServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
namespace QuizServices.Data.EFCore
{
    public class EfCoreAccountRepository: EfCoreRepository<QuizAccounts,QuizContext>
    {
        public readonly QuizContext _context;
        public EfCoreAccountRepository(QuizContext context) : base(context)
        {
            _context = context;
        }

        internal List<QuizAccounts> GetAllAccounts()
        {
            return _context.QuizAccounts.ToList();
        }
    }
}
