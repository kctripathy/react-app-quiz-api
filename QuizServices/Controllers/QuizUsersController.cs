using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuizServices.Models;
using QuizServices.ViewModels;

namespace QuizServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizUsersController : ControllerBase
    {
        private readonly QuizContext _context;

        public QuizUsersController(QuizContext context)
        {
            _context = context;
        }

        // GET: api/QuizUsers
        [HttpGet]
        public IEnumerable<QuizUsers> GetQuizUsers()
        {
            return _context.QuizUsers.Where(u=>u.AccountId == 1);
        }

        // GET: api/QuizUsers/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuizUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quizUsers = await _context.QuizUsers.FindAsync(id);

            if (quizUsers == null)
            {
                return NotFound();
            }

            return Ok(quizUsers);
        }

        // PUT: api/QuizUsers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutQuizUsers([FromRoute] int id, [FromBody] QuizUsers quizUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != quizUsers.UserId)
            {
                return BadRequest();
            }

            _context.Entry(quizUsers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!QuizUsersExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// POST: api/QuizUsers
        //[HttpPost]
        //public async Task<IActionResult> PostQuizUsers([FromBody] QuizUsers quizUsers)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    _context.QuizUsers.Add(quizUsers);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetQuizUsers", new { id = quizUsers.UserId }, quizUsers);
        //}

         
        [HttpPost]
        public IActionResult Register([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            QuizUsers qu = new QuizUsers();
            qu.AccountId = user.AccountId;
            qu.UserEmail = user.UserEmail;
            qu.UserName = user.UserName;
            qu.Fullname = user.Fullname;
            qu.Salt = Security.GetNewSalt(5);
            qu.UserPassword = Security.GetSaltedHashPassword(qu.Salt, user.UserPassword);
            qu.AccessLevel = user.AccessLevel;

            _context.QuizUsers.Add(qu);
            _context.SaveChanges();

            return CreatedAtAction("GetQuizUsers", new { id = qu.UserId }, qu);
        }


        [HttpPost]        
        [Route("[action]")]
        public ActionResult Login([FromBody] UserLogin user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var quizUsers = _context.QuizUsers.Where(u => u.UserName == user.UserName).ToList();
            if (quizUsers == null)
            {
                return NotFound();
            }

            QuizUsers quizUser = quizUsers[0];
                
            string suppliedHasedPassword = Security.GetSaltedHashPassword(quizUser.Salt, user.UserPassword);
            string actualHashedPassword = quizUser.UserPassword;

            if (!(suppliedHasedPassword.Equals(actualHashedPassword)))
            {
                return BadRequest(ReturnResponse.GetFailureStatus(ModelState));
            }

            //ReturnResponse response = new ReturnResponse();
            //ReturnStatus status = new ReturnStatus();
            //status.Code = "1";
            //status.Message = "Success";

            //response.Result = quizUser; //GetUserToRespond(quizUser);
            //response.Status = status;

            return Ok(ReturnResponse.GetSuccessStatus(quizUser));
            
        }

        //private User GetUserToRespond(QuizUsers quizUser)
        //{
        //    throw new NotImplementedException();
        //}

        // DELETE: api/QuizUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuizUsers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var quizUsers = await _context.QuizUsers.FindAsync(id);
            if (quizUsers == null)
            {
                return NotFound();
            }

            _context.QuizUsers.Remove(quizUsers);
            await _context.SaveChangesAsync();

            return Ok(quizUsers);
        }

        private bool QuizUsersExists(int id)
        {
            return _context.QuizUsers.Any(e => e.UserId == id);
        }
    }
}