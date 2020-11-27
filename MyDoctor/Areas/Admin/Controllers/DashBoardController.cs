using Microsoft.AspNetCore.Mvc;
namespace MyDoctor.Areas.Admin.Controllers
{
    public class DashBoardController : BaseController
    {
  
        // GET: DashBoard
        public ActionResult Index()
        {
            return View();
        }

       
    }
}