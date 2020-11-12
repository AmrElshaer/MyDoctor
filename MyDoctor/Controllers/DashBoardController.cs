
using Microsoft.AspNetCore.Mvc;

namespace MyDoctor.Controllers
{
    public class DashBoardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}