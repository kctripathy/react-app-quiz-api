//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using QuizServices.Models;

//namespace QuizServices.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class QuizController : ControllerBase
//    {
//        private readonly QuizContext _context;

//        public QuizController(QuizContext context)
//        {
//            _context = context;
//        }

//        // GET: api/Quiz
//        [HttpGet]
//        public IEnumerable<QuizMaster> GetQuizMaster()
//        {
//            return _context.QuizMaster;
//        }

//        // GET: api/Quiz/5
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetQuizMaster([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var quizMaster = await _context.QuizMaster.FindAsync(id);

//            if (quizMaster == null)
//            {
//                return NotFound();
//            }

//            return Ok(quizMaster);
//        }

//        // PUT: api/Quiz/5
//        [HttpPut("{id}")]
//        public async Task<IActionResult> PutQuizMaster([FromRoute] int id, [FromBody] QuizMaster quizMaster)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            if (id != quizMaster.Id)
//            {
//                return BadRequest();
//            }

//            _context.Entry(quizMaster).State = EntityState.Modified;

//            try
//            {
//                await _context.SaveChangesAsync();
//            }
//            catch (DbUpdateConcurrencyException)
//            {
//                if (!QuizMasterExists(id))
//                {
//                    return NotFound();
//                }
//                else
//                {
//                    throw;
//                }
//            }

//            return NoContent();
//        }

//        // POST: api/Quiz
//        [HttpPost]
//        public async Task<IActionResult> PostQuizMaster([FromBody] QuizMaster quizMaster)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            _context.QuizMaster.Add(quizMaster);
//            await _context.SaveChangesAsync();

//            return CreatedAtAction("GetQuizMaster", new { id = quizMaster.Id }, quizMaster);
//        }

//        // DELETE: api/Quiz/5
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> DeleteQuizMaster([FromRoute] int id)
//        {
//            if (!ModelState.IsValid)
//            {
//                return BadRequest(ModelState);
//            }

//            var quizMaster = await _context.QuizMaster.FindAsync(id);
//            if (quizMaster == null)
//            {
//                return NotFound();
//            }

//            _context.QuizMaster.Remove(quizMaster);
//            await _context.SaveChangesAsync();

//            return Ok(quizMaster);
//        }

//        private bool QuizMasterExists(int id)
//        {
//            return _context.QuizMaster.Any(e => e.Id == id);
//        }
//    }
//}