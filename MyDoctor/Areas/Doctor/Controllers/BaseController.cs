
using Microsoft.AspNetCore.Mvc;
using MYDoctor.Core.Application.Common.Enum;

namespace MyDoctor.Areas.Doctor.Controllers
{
    [Area(nameof(Roles.Doctor))]
    public class BaseController : Controller
    {
       
    }
}