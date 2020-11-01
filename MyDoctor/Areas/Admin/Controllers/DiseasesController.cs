using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DiseasesController : Controller
    {
        private readonly IDiseasesRepository _diseasesRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IDiseaseRelativeRepository _diseaseRelativeRepository;
        public DiseasesController(IDiseasesRepository diseasesRepository, ICommentRepository commentRepository, IDiseaseRelativeRepository diseaseRelativeRepository)
        {
            _diseasesRepository = diseasesRepository;
            _commentRepository = commentRepository;
            _diseaseRelativeRepository = diseaseRelativeRepository;
        }


        public async Task<IActionResult> Index()
        {
            var disease = await _diseasesRepository.GetAllAsync();
            return View(disease);
        }

      
    }
}
