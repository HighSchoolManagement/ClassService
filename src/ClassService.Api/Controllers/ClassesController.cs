using ClassService.Application.Classes;
using ClassService.Application.Classes.CreateClass;
using ClassService.Application.Classes.DeleteClass;
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
    [Route("api/{schoolYearId:int}/{schoolId:int}/[controller]")]
    [ApiController]
    public class ClassesController : Controller
    {
        private readonly IMediator _mediator;
        public ClassesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult>GetPage(int pageNumber, int pageSize, [FromRoute] int schoolYearId, [FromRoute] int schoolId, [FromRoute] string? className)
        {
            var classReponse = await _mediator.Send(new GetClassesQuery { PageNumber = pageNumber, PageSize = pageSize, SchoolYearId = schoolYearId, SchoolId = schoolId, ClassName = className});
            return Ok(classReponse);
        }


        [HttpGet("{classId:int}")]
        public async Task<IActionResult> GetById([FromRoute] int schoolYearId,[FromRoute] int schoolId, [FromRoute] int classId)
        {
            try
            {
                var result = await _mediator.Send(new GetClassByIdQuery
                {
                    SchoolYearId = schoolYearId,
                    ClassId = classId,
                    SchoolId = schoolId
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
        public async Task<IActionResult> Create([FromRoute] int schoolYearId, [FromRoute] int schoolId, [FromBody] CreateClassRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var classResponse = await _mediator.Send(new CreateClassCommand { SchoolYearId = schoolYearId, SchoolId = schoolId, CreateClassRequest = request }, cancellationToken);
                return CreatedAtAction(nameof(GetById), new { schoolYearId = classResponse.SchoolYearId, schoolId = classResponse.SchoolId, classId = classResponse.ClassId }, classResponse);
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
        public async Task<IActionResult> Update([FromRoute] int classId, [FromRoute] int schoolYearId, [FromBody] UpdateClassRequest request, [FromRoute] int schoolId, [FromHeader(Name = "If-Match")] string? ifMatch)
        {
            try
            {
                byte[]? rowVersion = null;
                if (!string.IsNullOrEmpty(ifMatch))
                {
                    rowVersion = Convert.FromBase64String(ifMatch);
                }
                var response = await _mediator.Send(new UpdateClassCommand { ClassId = classId,SchoolYearId = schoolYearId, SchoolId = schoolId, UpdateClassRequest = request, RowVersion = rowVersion });
                return Ok(response);
            }
            catch (FormatException)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = "If-Match is not a valid RowVersion" });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = ex.Message });
            }
            catch (RowVersionConflitException ex)
            {
                return Conflict(new ProblemDetails { Status = StatusCodes.Status409Conflict
                    , Title = ex.Message });
            }
        }

        [HttpDelete("{classId:int}")]
        public async Task<IActionResult> Delete([FromRoute] int classId, [FromRoute] int schoolYearId,
            [FromRoute] int schoolId, [FromHeader(Name = "If-Match")] string? ifMatch)
        {
            try
            {
                byte[]? rowVersion = null;
                if (!string.IsNullOrEmpty(ifMatch))
                {
                    rowVersion = Convert.FromBase64String(ifMatch);
                }
                await _mediator.Send(new DeleteClassCommand
                {
                    SchoolYearId = schoolYearId, SchoolId = schoolId, ClassId = classId, RowVersion = rowVersion
                });
                return NoContent();
            }
            catch (FormatException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest, Title = "If-Match is not a valid RowVersion" });
            }
            catch (ClassNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound, Title = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ProblemDetails { Status = StatusCodes.Status400BadRequest
                    , Title = ex.Message });
            }
            catch (RowVersionConflitException ex)
            {
                return Conflict(new ProblemDetails { Status = StatusCodes.Status409Conflict
                    , Title = ex.Message });
            }
        }
    }
}
