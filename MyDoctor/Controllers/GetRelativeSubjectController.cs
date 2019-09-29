using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Data;

namespace MyDoctor.Controllers
{
    public class GetRelativeSubjectController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GetRelativeSubjectController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string relativeitem)
        {
        var list=_context.Posts.Where(a=>a.specific==relativeitem);
            if (list==null)
            {
                return null;
            }
            return new JsonResult(list.ToList());
        }
    }
}