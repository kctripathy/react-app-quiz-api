using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizServices.Data.EFCore
{
    public class EfCoreClassesSubjectsRepository : EfCoreRepository<QuizClassesSubject, QuizContext>
    {
        private readonly QuizContext _context;

        public EfCoreClassesSubjectsRepository(QuizContext context) : base(context)
        {
            this._context = context;
        }

        public List<QuestionAvailaleClassAndSubject> GetAvailableQuestionClassesSubjectsByAccountId(int accountId)
        {
            //List<ClassSubject> listClassSubjects = new List<ClassSubject>();
            //var acsList = _context.QuizClassesSubject.Where(acs => acs.AccountId == accountId).ToList();

            //if (acsList == null && acsList.Count == 0)
            //{
            //    return null; // No classes and subjects found
            //}
            //return listClassSubjects;
            List<QuestionAvailaleClassAndSubject> questionvailableClassesSubjectsList = _context
                                                    .QuestionAvailaleClassAndSubjects
                                                    .FromSql($"GetQuestionsAvailaleClassesAndSubjectsByAccountId {accountId}")
                                                    .ToList();

            return questionvailableClassesSubjectsList;
        }


        public List<QuestionAvailaleClassAndSubject> GetAllClassesSubjectsByAccountId(int accountId)
        {
            //List<ClassSubject> listClassSubjects = new List<ClassSubject>();
            //var acsList = _context.QuizClassesSubject.Where(acs => acs.AccountId == accountId).ToList();

            //if (acsList == null && acsList.Count == 0)
            //{
            //    return null; // No classes and subjects found
            //}
            //return listClassSubjects;
            List<QuestionAvailaleClassAndSubject> questionvailableClassesSubjectsList = _context
                                                    .QuestionAvailaleClassAndSubjects
                                                    .FromSql($"GetAllClassesSubjectsByAccountId {accountId}")
                                                    .ToList();

            return questionvailableClassesSubjectsList;
        }


    }
}
