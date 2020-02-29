using Microsoft.AspNetCore.Mvc;
using QuizServices.Data.EFCore;
using QuizServices.Models;
using QuizServices.ViewModels;

namespace QuizServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : QuizContextBaseController<QuizQuestions, EfCoreQuestionsRepository>
    {
        private readonly EfCoreQuestionsRepository _repository;
        public QuestionsController(EfCoreQuestionsRepository repository, QuizContext context) : base(repository)
        {
            _repository = repository;            
        }

        [HttpGet("{classSubjectid}/{accountId}", Name = "GetQuestionsByClassSubjectAndAccountId")]
        public IActionResult Get(int classSubjectid, int accountId)
        {
            return Ok(_repository.GetQuizQuestionsByClassSubjctAndAccountId(classSubjectid, accountId));
        }


        [HttpGet("{classSubjectid}/{accountId}/{totalQuestionsToFetch}", Name = "GetQuestionsByClassSubjectAndAccountIdWithCountOfRecords")]
        public IActionResult Get(int classSubjectid, int accountId, int totalQuestionsToFetch)
        {
            return Ok(_repository.GetQuizQuestionsByClassSubjctAndAccountId(classSubjectid, accountId, totalQuestionsToFetch));
        }

        [HttpGet("list/{accountId}", Name = "GetAllQuestionsByAccountId")]
        public IActionResult GetAllQuestionsByAccountId(int accountId)
        {
            return Ok(_repository.GetAllQuestionsByAccountId(accountId));
        }

        [HttpGet("getbyid/{questionId}", Name = "GetQuestionById")]
        public IActionResult GetAllQuestionById(int questionId)
        {
            return Ok(_repository.GetQuestionById(questionId));
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult Add([FromBody] Question question)
        {
            if (question == null || question.options == null || question.questionType == null)
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Bad Request"));
            }

            int newQuestionId = _repository.InsertQuestion(question);

            if (newQuestionId > 0)
                return Ok(ReturnResponse.GetSuccessStatus("Successfully Inserted: new id is: " + newQuestionId.ToString()));
            else
                return BadRequest("Failed to insert the question");
        }

        [HttpPut]
        [Route("[action]")]
        public IActionResult Update([FromBody] Question question)
        {
            if (question == null || question.options == null || question.questionType == null)
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Bad Request"));
            }

            int newQuestionId = _repository.UpdateQuestion(question);

            if (newQuestionId > 0)
                return Ok(ReturnResponse.GetSuccessStatus("Successfully Updated the question : " + newQuestionId.ToString()));
            else
                return BadRequest("Failed to Update the question");
        }

        [HttpDelete]
        [Route("[action]")]
        public new IActionResult Delete ([FromBody] int id)
        {
            IActionResult returnResult;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            int returnValue = _repository.DeleteQuestion(id);

            if (returnValue > 0)
                returnResult = Ok("Successfully deleted the question");
            else if (returnValue.Equals(0))
                returnResult = NotFound("Question Not found");
            else
                returnResult = BadRequest("Failed to delete");

            return returnResult;
        }
    }
}