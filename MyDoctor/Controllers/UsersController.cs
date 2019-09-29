using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.Data;

namespace MyDoctor.Controllers
{
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsersController(ApplicationDbContext context)
        {
            _context = context;

        }
        public IActionResult GetUser(string id)
        {
            if (id==null)
            {
                return new NotFoundResult();
            }
            var resuilt=this._context.Users.FirstOrDefault(a=>a.UserName==id);
            if (resuilt==null)
            {
                return new NotFoundResult();
            }
            return new JsonResult(resuilt);
        }
    }
}