using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;

namespace QuizServices.Data.EFCore
{
    public class EfCoreQuestionsRepository : EfCoreRepository<QuizQuestions, QuizContext>
    {
        private readonly QuizContext _context;
        public EfCoreQuestionsRepository(QuizContext context) : base(context)
        {
            _context = context;
        }

        #region Get All the Questions Along With Options that will be rendered in the UI (ReactJS)
        /// <summary>
        /// This function will return all the questions will be rendered in the front end
        /// </summary>
        /// <param name="classSubjectId">class & subject id</param>
        /// <param name="accountId">account owner for whom the questions will be return</param>
        /// <returns></returns>
        public QuizQuestionsViewModel GetQuizQuestionsByClassSubjctAndAccountId(int classSubjectId, int accountId, int totalQuestionToFetch = 20)
        {
            var QuizClassSubject = _context
                                    .ClassSubject
                                    .FromSql($"GetQuizClassSubjectByID {classSubjectId}")
                                    .ToList();

            if (QuizClassSubject.Count.Equals(0)) return null;

            QuizQuestionsViewModel qvm = new QuizQuestionsViewModel
            {
                id = QuizClassSubject[0].ClassSubjectID,
                name = QuizClassSubject[0].SubjectName,
                description = QuizClassSubject[0].SubjectDesc,
                classname = QuizClassSubject[0].ClassDesc,
                questions = GetAllQuestions(classSubjectId, accountId, totalQuestionToFetch)
            };
            qvm.totalQuestions = qvm.questions.Count;            
            return qvm;
        }

        /// <summary>
        /// To get all the questions for a class and subject
        /// </summary>
        /// <param name="classSubjectId">classSubjectId</param>
        /// <param name="accountId">accountId</param>
        /// <param name="totalQuestionToFetch">totalQuestionToFetch</param>
        /// <returns></returns>
        private List<Question> GetAllQuestions(int classSubjectId, int accountId, int totalQuestionToFetch = 20)
        {

            var quizQuestionList = _context.QuizQuestions
                                            .Where(q => q.AccountId == accountId && q.ClassSubjectId == classSubjectId)
                                            .OrderBy(a => Guid.NewGuid())
                                            .ToList();

            if (totalQuestionToFetch > quizQuestionList.Count)
            {
                totalQuestionToFetch = quizQuestionList.Count;
            }

            List<Question> questionlist = new List<Question>();
            for (int ctr = 1; ctr <= totalQuestionToFetch; ctr++)
            {

                Question q = new Question
                {

                    Id = quizQuestionList[ctr - 1].Id,
                    name = quizQuestionList[ctr - 1].Description, //string.Format("Q#{0} - {1}", ctr, QuestionList[ctr - 1].Description),                                       
                    options = GetAllOptions(quizQuestionList[ctr - 1].Id, quizQuestionList[ctr - 1].CorrectAnswerOptionId)
                };
                q.questionType = GetQuestionTypes(q.questionTypeId);
                q.classSubjectId = classSubjectId;
                q.accountId = accountId;
                questionlist.Add(q);
            }


            return questionlist;
        }

        /// <summary>
        /// To determine what are the options of a question
        /// </summary>
        /// <param name="questionId">The question for whom the option needed</param>
        /// <param name="correctAnswerOptionId">Which one is the correct answer id</param>
        /// <returns>All options of the question</returns>
        private List<Option> GetAllOptions(int questionId, long? correctAnswerOptionId)
        {

            List<QuizQuestionsOptions> questionOptions = _context
                                                            .QuizQuestionsOptions
                                                            .Where(a => a.QuestionId == questionId)
                                                            .ToList();

            // Adding options for the question
            List<Option> oList = new List<Option>();
            int ctr = 0;
            foreach (QuizQuestionsOptions opt in questionOptions)
            {
                ctr++;
                Option o = new Option();
                o.id = opt.Id;
                o.questionId = questionId;
                o.name = opt.Description; //string.Format("Option {0}-{1} | {2} ", questionId, ctr, opt.Description);
                o.isAnswer = (opt.Id == correctAnswerOptionId);
                oList.Add(o);
            }

            return oList;
        }

        /// <summary>
        /// To get the type of the question
        /// </summary>
        /// <param name="questionTypeId"></param>
        /// <returns></returns>
        private QuestionType GetQuestionTypes(int questionTypeId)
        {
            QuestionType qt = new QuestionType();
            qt.id = 1;
            qt.name = "Multiple Choice";
            qt.isActive = true;
            return qt;
        }
        #endregion


        #region Question CRUD Operation
        /// <summary>
        /// To add q question along with the options
        /// </summary>
        /// <param name="question">The question to add</param>
        /// <returns>The new id of the question</returns>
        public int InsertQuestion(Question question)
        {
            int returnValue = 0;
            try
            {
                QuizQuestions ques = new QuizQuestions();
                ques.Description = question.name;
                ques.AccountId = question.accountId;
                ques.ClassSubjectId = question.classSubjectId;

                _context.QuizQuestions.Add(ques);
                _context.SaveChanges();

                int newQuestionId = ques.Id;
                long correctAnswerId = 0;
                foreach (Option o in question.options)
                {
                    QuizQuestionsOptions opt = new QuizQuestionsOptions();
                    opt.Description = o.name;
                    opt.QuestionId = newQuestionId;

                    _context.QuizQuestionsOptions.Add(opt);
                    _context.SaveChanges();

                    if (o.isAnswer)
                    {
                        correctAnswerId = opt.Id;
                    }
                }


                ques.CorrectAnswerOptionId = correctAnswerId;

                _context.QuizQuestions.Update(ques);
                _context.SaveChanges();

                returnValue = newQuestionId;
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {

            }
            return returnValue;
        }

        /// <summary>
        /// Update a question
        /// </summary>
        /// <param name="question">The question to update</param>
        /// <returns>The id of the updated question</returns>
        public int UpdateQuestion(Question question)
        {
            int returnValue = 0;
            try
            {
                QuizQuestions ques = new QuizQuestions();
                ques.Id = question.Id;
                ques.Description = question.name;
                ques.AccountId = question.accountId;
                ques.ClassSubjectId = question.classSubjectId;

                _context.QuizQuestions.Update(ques);
                _context.SaveChanges();

                long correctAnswerId = 0;
                foreach (Option o in question.options)
                {
                    QuizQuestionsOptions opt = new QuizQuestionsOptions();
                    opt.Id = o.id;
                    opt.Description = o.name;
                    opt.QuestionId = question.Id;

                    _context.QuizQuestionsOptions.Update(opt);
                    _context.SaveChanges();

                    if (o.isAnswer)
                    {
                        correctAnswerId = opt.Id;
                    }
                }


                ques.CorrectAnswerOptionId = correctAnswerId;

                _context.QuizQuestions.Update(ques);
                _context.SaveChanges();

                returnValue = ques.Id;
            }
            catch
            {
                returnValue = 0;
            }
            finally
            {

            }
            return returnValue;
        }

        /// <summary>
        /// Delete a question
        /// </summary>
        /// <param name="id">which question id to delete</param>
        /// <returns>0 if false or -1 if any error or the id of the question deleted</returns>
        public int DeleteQuestion(int id)
        {
            int returnValue = 0;
            try
            {
                var quizQuestion = _context.QuizQuestions.Find(id);
                if (quizQuestion == null)
                    returnValue = 0;
            }
            catch
            {
                returnValue = -1;
            }
            return returnValue;
        }
        #endregion
    }
}
