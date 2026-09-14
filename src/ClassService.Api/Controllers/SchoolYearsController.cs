using ClassService.Application.Common.Mediator;
using ClassService.Application.SchoolYears.CreateSchoolYear;
using Microsoft.AspNetCore.Mvc;

namespace ClassService.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchoolYearsController : Controller
    {
        private readonly IMediator _mediator;
        public SchoolYearsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSchoolYearRequest request)
        {
            try
            {
                var school = await _mediator.Send(new CreateSchoolYearCommand { CreateSchoolYearRequest = request });
                return null;
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = ex.Message });
            }
            catch (DuplicateSchoolYearNameException ex)
            {
                return Conflict(new ProblemDetails { Status = StatusCodes.Status409Conflict, Title = ex.Message });
            }
        }
    }
}
