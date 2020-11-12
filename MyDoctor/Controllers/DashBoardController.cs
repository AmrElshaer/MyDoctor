
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyDoctor.IRepository;
using MyDoctor.ISerivce;
using MyDoctor.ViewModels;

namespace MyDoctor.Controllers
{
    public class DashBoardController : Controller
    {
        private readonly IDashBoardSerivce _dashBoardSerivce;
        public DashBoardController(IDashBoardSerivce dashBoardSerivce)
        {
            _dashBoardSerivce = dashBoardSerivce;
        }
        public async Task<IActionResult>  Index()
        {
            var result = await _dashBoardSerivce.GetBoardViewModel(4);
            return View(result);
        }
    }
}