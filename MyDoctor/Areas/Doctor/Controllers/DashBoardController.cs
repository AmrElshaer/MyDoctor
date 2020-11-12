using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MyDoctor.Areas.Doctor.Controllers
{
    public class DashBoardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}