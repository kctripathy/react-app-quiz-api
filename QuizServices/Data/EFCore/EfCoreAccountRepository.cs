using QuizServices.Controllers;
using QuizServices.Models;
using QuizServices.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
namespace QuizServices.Data.EFCore
{
    public class EfCoreAccountRepository : EfCoreRepository<QuizAccounts, QuizContext>
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

        internal bool CreateNewAccount(Account account)
        {
            try
            {
                //=======================================================
                //Step 1: Create New Account
                //=======================================================
                QuizAccounts quizAccounts = new QuizAccounts();
                quizAccounts.AccountName = account.accountName;
                quizAccounts.ContactName = account.accountName;
                quizAccounts.Phone = account.contactPhone;

                _context.QuizAccounts.Add(quizAccounts);
                _context.SaveChanges();
                int newAccountId = quizAccounts.Id;

                //=======================================================
                //Step 2: Create an user for the created account
                //=======================================================
                 
                QuizUsers quizUser = new QuizUsers
                {
                    AccountId = newAccountId,
                    Fullname = account.contactName,
                    UserEmail = account.contactEmail,
                    UserPassword = account.loginPassword,
                    AccessLevel=10,
                    ClassId = 1,
                    SubjectIds = string.Empty,
                    UserName = account.contactEmail
                };
                int returValue = AddNewUser(quizUser);

                //=======================================================
                //Step 3: Create all class and subjects for the account
                //=======================================================
                string[] classSubjectIds = account.classSubjects.Split(',');
                foreach (var classSubjectId in classSubjectIds)
                {
                    var objClassSubject = _context.QuizClassesSubject
                                                        .Where(cs => cs.Id == Convert.ToInt32(classSubjectId))
                                                        .ToList();

                    if (objClassSubject == null) continue;

                    int classId = objClassSubject[0].ClassId;
                    int subjectId = objClassSubject[0].SubjectId;

                    QuizClassesSubject newClassSubject = new QuizClassesSubject
                    {
                        AccountId = newAccountId,
                        ClassId = classId,
                        SubjectId = subjectId
                    };
                    _context.QuizClassesSubject.Add(newClassSubject);
                    _context.SaveChanges();
                    //System.Console.WriteLine($"{classSubjectId}");
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        private int AddNewUser(QuizUsers user)
        {
            int returnValue = 0;
            try
            {
                QuizUsers qu = new QuizUsers
                {
                    AccountId = user.AccountId,
                    UserEmail = user.UserEmail,
                    UserName = user.UserName,
                    Fullname = user.Fullname,
                    UserPhone = user.UserPhone,
                    ClassId = user.ClassId,
                    SubjectIds = user.SubjectIds,
                    Salt = Security.GetNewSalt(5)

                };
                qu.UserPassword = Security.GetSaltedHashPassword(qu.Salt, user.UserPassword);
                qu.AccessLevel = user.AccessLevel;

                _context.QuizUsers.Add(qu);
                _context.SaveChanges();

                return qu.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
                returnValue = -1;
            }
            return returnValue;
        }
    }
}
