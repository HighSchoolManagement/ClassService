using ClassService.Application.ClassRoomSchedule.GetClassRoomSchedule;
using ClassService.Application.Common.Mediator;
using ClassService.Application.Rooms;
using ClassService.Application.Rooms.GetRooms;
using ClassService.Application.Schools.GetSchoolById;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ClassService.Api.Controllers
{
    [Route("api/{schoolId:int}/[controller]")]
    [ApiController]
    public class RoomsController : Controller
    {
        private readonly IMediator mediator;
        public RoomsController(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetListRoom([FromRoute] int schoolId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await mediator.Send(new GetRoomsQuery { PageNumber = pageNumber, PageSize = pageSize, SchoolId = schoolId });
            return Ok(response);
        }

        [HttpGet("{roomId:int}/schedule")]
        public async Task<IActionResult> GetRoomSchedule([FromRoute] int schoolId, [FromQuery] DateOnly from, [FromQuery] DateOnly to, [FromRoute] int roomId)
        {
            try
            {
                var response = await mediator.Send(new GetRoomScheduleQuery { From = from, To = to, RoomId = roomId, SchoolId = schoolId });
                return Ok(response);
            }
            catch (SchoolNotFoundException ex)
            {
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound, Title = ex.Message });
            }
            catch (RoomNotFoundException ex)
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
