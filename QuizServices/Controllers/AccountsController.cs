using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizServices.Models;
using QuizServices.Data.EFCore;
using QuizServices.ViewModels;
using Microsoft.AspNetCore.Cors;

namespace QuizServices.Controllers
{
    [EnableCors("MyPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : QuizContextBaseController<QuizAccounts, EfCoreAccountRepository>
    {
        EfCoreAccountRepository _repository;
        public AccountsController(EfCoreAccountRepository repository): base(repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("[Action]")]
        public IActionResult All()
        {
            var accounts = _repository.GetAllAccounts();
            return Ok(ReturnResponse.GetSuccessStatus(accounts));
        }

        [HttpPost]
        [Route("[Action]")]
        public IActionResult AddNew([FromBody] Account account)
        {
            bool result = _repository.CreateNewAccount(account);
            if (result == true)
                return Ok(ReturnResponse.GetSuccessStatus(result));
            else
                return BadRequest(ReturnResponse.GetFailureStatus(result));
        }

        [HttpPut]
        [Route("[Action]")]
        public IActionResult UpdateAccount([FromBody] Account account)
        {
            bool result = _repository.UpdateAccount(account);
            if (result == true)
                return Ok(ReturnResponse.GetSuccessStatus(result));
            else
                return BadRequest(ReturnResponse.GetFailureStatus(result));
        }
    }
}