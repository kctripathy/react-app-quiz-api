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
    public class UsersController : QuizContextBaseController<QuizUsers, EfCoreUsersRepository>
    {
        EfCoreUsersRepository _repository;

        public UsersController(EfCoreUsersRepository repository) : base(repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[Action]")]
        public IActionResult Register([FromBody] QuizUsers user)
        {
            ReturnResponse returnResponse = null;
            int newUserId = _repository.Register(user);
           
            if (newUserId > 0)
            {
                returnResponse = ReturnResponse.Get(newUserId, user);
                return Ok(returnResponse);
            }
            else
            {
                return BadRequest(ReturnResponse.Get(newUserId));
            }
                
        }


        [HttpPost]
        [Route("[Action]")]
        [EnableCors("MyPolicy")]
        public IActionResult Login([FromBody] UserLogin userLoginCredentials)
        {
            int returnCode = 0;
            IActionResult returnResponse;
            User loggedOnUser = _repository.Login(userLoginCredentials, out returnCode);
            if (!(loggedOnUser == null))
                returnResponse = Ok(ReturnResponse.Get(loggedOnUser));
            else
                returnResponse = BadRequest(ReturnResponse.Get(returnCode));

            return returnResponse;
        }


        [HttpGet]
        [Route("[Action]")]
        [EnableCors("MyPolicy")]
        public IActionResult Logout()
        {
            return Ok("Logged out successfully");
        }
    }
}