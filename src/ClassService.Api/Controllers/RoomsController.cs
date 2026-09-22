using ClassService.Application.Common.Mediator;
using ClassService.Application.Rooms.GetRooms;
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

        public async Task<IActionResult> GetListRoom([FromRoute] int schoolId, [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var response = await mediator.Send(new GetRoomsQuery { PageNumber = pageNumber, PageSize = pageSize, SchoolId = schoolId });
            return Ok(response);
        }
    }
}
