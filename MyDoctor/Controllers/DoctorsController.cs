using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyDoctor.Data;
using MyDoctor.Models;
using System.Web;
using System.Net.Mail;
using System.Net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace MyDoctor.Controllers
{
    
    public class DoctorsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<CutomPropertiy> _userManager;

        public DoctorsController(ApplicationDbContext context,UserManager<CutomPropertiy> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
       
        // GET: Doctors
        public async Task<IActionResult> Index(string special,string city,string country)
        {
            IEnumerable<Doctor> Doctrolist;
           
            if (special==null&&city==null&&country==null)
            {
                return View(await _context.Doctor.ToListAsync());
            }
            else
            {
                Doctrolist = _context.Doctor.Where(a => a.Specials == special && a.City == city && a.Country == country);

            }
            if (Doctrolist.ToArray().Length>0)
            {
               
                return View(Doctrolist);
            }
        return View(await  _context.Doctor.ToListAsync());
           
        }
      
       
        public IActionResult messagetogmail(string Body, string From, string password)
        {
            try
            {
                using (MailMessage mail = new MailMessage())
                {
                    mail.To.Add("amrelsher07@gmail.com");
                    mail.From = new MailAddress(From);
                    mail.IsBodyHtml = true;
                    mail.Body = $"<h3>{Body}</h3>";
                    mail.Subject = "MY DOCTOR WEB";
                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential()
                        {
                            UserName = From,
                            Password = password
                        };
                        smtp.Host = "smtp.gmail.com";
                        smtp.Port = 587;
                        smtp.EnableSsl = true;
                        smtp.Send(mail);
                    }

                }
            }
            catch (Exception)
            {
                return BadRequest();
            }
           
            return Ok();
        }
        // GET: Doctors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.Id == id);
            ViewData["Posts"] = _context.Posts.Where(a=>a.doctorid==id.ToString()).ToList();
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Specials,Country,City,Telephone,Others,Email,Password,ConfirmPassword")] Doctor doctor)
        {
            if (ModelState.IsValid)

            {
               
                var user = new CutomPropertiy { UserName = doctor.Email, Email = doctor.Email };
                var result = await _userManager.CreateAsync(user, doctor.Password);

                if (result.Succeeded)
                {
                    _context.Add(doctor);
                    await _context.SaveChangesAsync();
                    await _userManager.AddToRoleAsync(await _userManager.FindByNameAsync(doctor.Email), "Doctor");
                

                    
                    return RedirectToAction(nameof(Index));
                  
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
          
            return View(doctor);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor.FindAsync(id);
            if (doctor == null)
            {
                return NotFound();
            }
            return View(doctor);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Specials,Country,City,Telephone,Others")] Doctor doctor)
        {
            if (id != doctor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(doctor);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DoctorExists(doctor.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(doctor);
        }

        // GET: Doctors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var doctor = await _context.Doctor
                .FirstOrDefaultAsync(m => m.Id == id);
            if (doctor == null)
            {
                return NotFound();
            }

            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var doctor = await _context.Doctor.FindAsync(id);
            _context.Doctor.Remove(doctor);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DoctorExists(int id)
        {
            return _context.Doctor.Any(e => e.Id == id);
        }
    }
}
