using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using QuizServices.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizServices.Data.EFCore;
using QuizServices.ViewModels;

namespace QuizServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesSubjectsController : QuizContextBaseController<QuizClassesSubject, EfCoreClassesSubjectsRepository>
    {
        private readonly EfCoreClassesSubjectsRepository _repository;

        public ClassesSubjectsController(EfCoreClassesSubjectsRepository repository) : base(repository)
        {
            this._repository = repository;
        }

        /// <summary>
        /// Get all the classes and subjects for which the question is availble for an account
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <returns>List of classes and subjects for which some question is available</returns>
        [HttpGet]
        [Route("[action]/{accountId}")]
        public IActionResult QuestionsAvailable(int accountId)
        {
            List<QuestionAvailaleClassAndSubject> list = _repository.GetAvailableQuestionClassesSubjectsByAccountId(accountId);
            if (list != null && list.Count > 0)
                return Ok(ReturnResponse.GetSuccessStatus(list));
            else
                return NotFound(ReturnResponse.Get(ReturnConstant.CLASS_SUBJECT_NOT_FOUND_QUESTION_AVAILABLE));
        }

        /// <summary>
        /// Get all the classes and subjects for an account id
        /// </summary>
        /// <param name="accountId">accountId</param>
        /// <returns>All the classes and subjects for the account</returns>
        [HttpGet]
        [Route("all/{accountId}")]
        public IActionResult GetAllClassesSubjectsByAccountId(int accountId)
        {
            List<QuestionAvailaleClassAndSubject> list = _repository.GetAllClassesSubjectsByAccountId(accountId);
            if (list != null && list.Count > 0)
                return Ok(ReturnResponse.GetSuccessStatus(list));
            else
                return NotFound(ReturnResponse.Get(ReturnConstant.CLASS_SUBJECT_ALL_NOT_FOUND));
        }

    }
}
