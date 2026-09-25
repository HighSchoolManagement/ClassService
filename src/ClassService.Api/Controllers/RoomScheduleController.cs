using Microsoft.AspNetCore.Mvc;

namespace ClassService.Api.Controllers
{

    [ApiController]
    public class RoomScheduleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
