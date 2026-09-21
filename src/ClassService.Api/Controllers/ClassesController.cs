using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Classes.GetClassById;
using ClassService.Application.Classes.GetClasses;
using ClassService.Application.Classes.UpdateClass;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Schools.GetSchoolById;
using ClassService.Application.SchoolYears.CreateSchoolYear;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClassService.Api.Controllers
{
    [Route("api/{schoolYearId:int}/[controller]")]
    [ApiController]
    public class ClassesController : Controller
    {
        private readonly IMediator _mediator;
        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult>GetPage(int? asOf, int pageNumber, int pageSize, [FromRoute] int schoolYearId)
        {
            var classReponse = await _mediator.Send(new GetClassesQuery { PageNumber = pageNumber, PageSize = pageSize, AsOfId = asOf, SchoolYearId = schoolYearId });
            return Ok(classReponse);
        }


        [HttpGet("{classId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int schoolYearId, [FromRoute] int classId)
        {
            try
            {
                var result = await _mediator.Send(new GetClassByIdQuery
                {
                    SchoolYearId = schoolYearId,
                    ClassId = classId
                });
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = ex.Message });
            }
            catch (ClassNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound, Title = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClassRequest request)
        {
            try
            {
                var classResponse = await _mediator.Send(new CreateClassCommand { CreateClassRequest = request });
                return CreatedAtAction(nameof(GetById), new { id = classResponse.ClassId }, classResponse);
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

        [HttpPatch("{classId:int}")]
        public async Task<IActionResult> Update([FromRoute] int classId, [FromRoute] int schoolYearId, [FromBody] UpdateClassRequest request)
        {
            try
            {
                await _mediator.Send(new UpdateClassCommand { ClassId = classId,SchoolYearId = schoolYearId, UpdateClassRequest = request });
                return NoContent();
            }
            catch (SchoolNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound, Title = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = ex.Message });
            }
        }
    }
}
