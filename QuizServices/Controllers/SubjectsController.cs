using Microsoft.AspNetCore.Mvc;
using QuizServices.Data.EFCore;
using QuizServices.Models;

namespace QuizServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : QuizContextBaseController<QuizSubjects, EfCoreSubjectsRepository>
    {
        public SubjectsController (EfCoreSubjectsRepository repository): base(repository)
        {

        }
    }
}