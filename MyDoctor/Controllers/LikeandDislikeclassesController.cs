using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class LikeandDislikeclassesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LikeandDislikeclassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LikeandDislikeclasses
        [HttpGet]
        public async Task<JsonResult> Index(string Diseasename)
        {
           

            var likeandDislikeclass = await _context.LikeandDislikeclass
                .FirstOrDefaultAsync(m => m.DiseaseName == Diseasename);

            return Json(likeandDislikeclass);
        }

        // GET: LikeandDislikeclasses/Details/5
        public async Task Like(string diseasename)
        {
            if (diseasename == null)
            {
                return;
            }

            var likeandDislikeclass = await _context.LikeandDislikeclass
                .FirstOrDefaultAsync(m => m.DiseaseName==diseasename);
            if (likeandDislikeclass == null)
            {
                return;
            }
            else
            {
                likeandDislikeclass.Like = likeandDislikeclass.Like + 1;
                await _context.SaveChangesAsync();
            }
            
        }
        public async Task DisLike(string diseasename)
        {

            if (diseasename == null)
            {
                return;
            }
            var likeandDislikeclass = await _context.LikeandDislikeclass
               .FirstOrDefaultAsync(m => m.DiseaseName == diseasename);

            if (likeandDislikeclass == null)
            {
                return;
            }
            else
            {
                likeandDislikeclass.DisLike = likeandDislikeclass.DisLike + 1;
                await _context.SaveChangesAsync();
            }

           

            
        }








    }
}
