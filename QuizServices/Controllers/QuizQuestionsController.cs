using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Cors;

namespace QuizServices.Controllers
{
    [EnableCors("AllowOrigin")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuizQuestionsController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizQuestionsController(QuizContext context)
        {
            _context = context;
        }

        //GetAvailaleClassAndSubjects

        // GET: api/Questions
        [HttpGet]
        public IActionResult Get()
        {

            //return Get(1, 1);
            return Ok(_context.QuizQuestions.ToList());
            //return new string[] { "Questions value1", "Questions value2" };
        }

        [HttpGet("available_class_subjects/{accountId}", Name = "GetAvailableClassAndSubject")]
        public IActionResult Get(int accountId)
        {
            return Ok("ok" + accountId.ToString());
        }

        [HttpGet("{classSubjectid}/{accountId}", Name = "GetQuestions")]
        public IActionResult Get(int classSubjectid, int accountId)
        {
            try
            {
                QuizQuestionsViewModel qvm =  GetQuizQuestionMasterOutput(classSubjectid, accountId);

                if (qvm == null)                    
                    return BadRequest(string.Format(@"[No Questions for classSubjectid={0} and quizId={1} ]", classSubjectid, accountId));
                else
                    return Ok(qvm);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message.ToString());
            }
            
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult add([FromBody] Question question)
        {
            if (question == null || question.options == null || question.questionType == null)
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Bad Request"));
            }

            QuizQuestions ques = new QuizQuestions();
            ques.Description = question.name;
            ques.AccountId = question.accountId;
            ques.ClassSubjectId =  question.classSubjectId;

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

            return Ok(ReturnResponse.GetSuccessStatus("Successfully Inserted: new id is: " + newQuestionId.ToString()));
        }

        private QuizQuestionsViewModel GetQuizQuestionMasterOutput(int classSubjectId, int accountId)
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
                questions = GetAllQuestions(classSubjectId, accountId)
            };
            return qvm;
        }


        private List<Question> GetAllQuestions(int classSubjectId, int accountId)
        {

            var QuestionList = _context
                                    .QuizQuestions
                                    .FromSql($"GetQuestions {classSubjectId}, {accountId}")
                                    .ToList();

            var OptionList = _context
                                    .QuizQuestionsOptions
                                    .FromSql($"GetQuestions {classSubjectId}, {accountId},'Options'")
                                    .ToList();


            List<Question> questionlist = new List<Question>();
            for (int ctr = 1; ctr <= QuestionList.Count; ctr++)
            {
                Question q = new Question
                {
                    Id = QuestionList[ctr - 1].Id,
                    name = QuestionList[ctr - 1].Description, //string.Format("Q#{0} - {1}", ctr, QuestionList[ctr - 1].Description),
                    options = GetAllOptions(QuestionList[ctr - 1].Id, OptionList, QuestionList[ctr - 1].CorrectAnswerOptionId)
                };
                q.questionType = GetQuestionTypes(q.questionTypeId);
                questionlist.Add(q);
            }


            return questionlist;
        }


        private List<Option> GetAllOptions(int questionId, List<QuizQuestionsOptions> optionList, long? correctAnswerOptionId)
        {

            List<QuizQuestionsOptions> questionOptions = optionList.FindAll(a => a.QuestionId == questionId);

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


        private QuestionType GetQuestionTypes(int questionTypeId)
        {
            QuestionType qt = new QuestionType();
            qt.id = 1;
            qt.name = "Multiple Choice";
            qt.isActive = true;
            return qt;
        }
    }
}
