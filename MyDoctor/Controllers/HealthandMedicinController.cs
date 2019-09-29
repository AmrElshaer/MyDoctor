using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Data;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class HealthandMedicinController : Controller
    {
        private readonly ApplicationDbContext context;

        public HealthandMedicinController(ApplicationDbContext context)
        {
            this.context = context;

        }
        public IActionResult Index()
        {
            return View();
        }
     
    }
}