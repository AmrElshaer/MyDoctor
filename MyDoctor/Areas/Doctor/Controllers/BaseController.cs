using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.ViewModels;

namespace MyDoctor.Areas.Doctor.Controllers
{
    [Area(nameof(Roles.Doctor))]
    public class BaseController : Controller
    {
       
    }
}