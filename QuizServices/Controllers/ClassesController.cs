using Microsoft.AspNetCore.Mvc;
using QuizServices.Data.EFCore;
using QuizServices.Models;

namespace QuizServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : QuizContextBaseController<QuizClasses, EfCoreClassesRepository>
    {
        EfCoreClassesRepository _repository;
        public ClassesController (EfCoreClassesRepository repository): base(repository)
        {
            _repository = repository;
        }        
    }
}