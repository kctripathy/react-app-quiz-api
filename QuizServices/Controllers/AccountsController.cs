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
        public IActionResult all()
        {
            var accounts = _repository.GetAllAccounts();
            return Ok(ReturnResponse.GetSuccessStatus(accounts));
        }
    }
}