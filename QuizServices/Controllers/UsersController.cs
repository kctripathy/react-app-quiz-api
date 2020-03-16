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


        [HttpGet]
        [Route("[Action]")]
        public IActionResult all(int accountId)
        {
            return Ok(ReturnResponse.GetSuccessStatus(_repository.GetAllUsersByAccountId(accountId)));
        }

        [HttpPut]
        [Route("[Action]")]
        public IActionResult update([FromBody] QuizUsers user)
        {

            ReturnResponse returnResponse = null;

            int returnResult = _repository.UpdateUser(user);
            
            if (returnResult > 0) // Successfully updated
            {
                return Ok(ReturnResponse.GetSuccessStatus(returnResult));
            }
            else // Failed to updated
            {
                return BadRequest (ReturnResponse.GetFailureStatus(returnResult));
            }


            return Ok(returnResponse);
            //int newUserId = _repository.Register(user);

            //if (newUserId > 0)
            //{
            //    returnResponse = ReturnResponse.Get(newUserId, user);
            //    return Ok(returnResponse);
            //}
            //else
            //{
            //    return BadRequest(ReturnResponse.Get(newUserId));
            //}

        }

        [HttpPost]
        [Route("[Action]")]
        [EnableCors("MyPolicy")]
        public IActionResult GetResetPasswordLink(User user)
        {
            string authKey = string.Empty;
            int returnResult = _repository.GetResetPasswordLink(user.UserEmail, out authKey);
            if (returnResult > 0)
            {
                return Ok(ReturnResponse.GetSuccessStatus(authKey));
            }
            else if (returnResult == -1) {
                return BadRequest(ReturnResponse.GetFailureStatus("Email doesn't exists"));
            }
            else
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Failed to get password reset link"));
            }             
        }

        [HttpPost]
        [Route("[Action]")]
        [EnableCors("MyPolicy")]
        public IActionResult ResetPassword(User user)
        {
            
            int returnResult = _repository.ResetPassword(user);
            if (returnResult > 0)
            {
                return Ok(ReturnResponse.GetSuccessStatus("Password changed successfully"));
            }
            else if (returnResult == -1)
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Link doesn't exists"));
            }
            else if (returnResult == -2)
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Link expired"));
            }
            else if (returnResult == -3)
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Invalid auth key"));
            }
            else
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Failed to reset password"));
            }
        }

        [HttpPost]
        [Route("[Action]")]
        [EnableCors("MyPolicy")]
        public IActionResult InsertContactLog(Quiz_ContactLog contactLog)
        {

            int returnResult = _repository.InsertContactLog(contactLog);
            if (returnResult > 0)
            {
                return Ok(ReturnResponse.GetSuccessStatus("Contact logged successfully"));
            }             
            else
            {
                return BadRequest(ReturnResponse.GetFailureStatus("Failed to log the contact"));
            }
        }
    }
}