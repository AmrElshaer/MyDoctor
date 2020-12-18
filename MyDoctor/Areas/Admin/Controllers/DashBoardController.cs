using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace MyDoctor.Areas.Admin.Controllers
{
    [Authorize]
    public class DashBoardController : BaseController
    {
  
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }

       
    }
}