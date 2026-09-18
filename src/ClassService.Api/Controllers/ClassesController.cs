using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Classes.GetClasses;
using ClassService.Application.Common.Mediator;
using ClassService.Application.SchoolYears.CreateSchoolYear;
using Microsoft.AspNetCore.Mvc;

namespace ClassService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassesController : Controller
    {
        private readonly IMediator _mediator;
        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult>GetPage(int? asOf, int pageNumber, int pageSize)
        {
            var classReponse = await _mediator.Send(new GetClassesQuery { PageNumber = pageNumber, PageSize = pageSize });
            return null;
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassRequest request)
        {
            try
            {
                var classResponse = await _mediator.Send(new CreateClassCommand { CreateClassRequest = request });
                return null;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = ex.Message });
            }
            catch (DuplicateClassNameException ex)
            {
                return Conflict(new ProblemDetails { Status = StatusCodes.Status409Conflict, Title = ex.Message });
            }
        }
    }
}
