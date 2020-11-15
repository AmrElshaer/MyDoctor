using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyDoctor.IRepository;
using MyDoctor.Models;

namespace MyDoctor.Controllers
{
    public class DoctorsController : Controller
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorsController(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        // GET: Doctors
        public async Task<IActionResult> Index(string special, string city, string country)
        {
            //var resuilt = await _doctorRepository.GetAllAsync(doctor =>
            //    (special == null || doctor.Specials.ToLower().Contains(special.ToLower())) &&
            //    (city == null || doctor.City.ToLower().Contains(city.ToLower())) &&
            //    (country == null || doctor.Country.ToLower().Contains(country.ToLower())));
            //return View(resuilt);
            return View();
        }

        /// <summary>
        ///     Send Message By Email
        /// </summary>
        /// <param name="body"></param>
        /// <param name="from"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public IActionResult messagetogmail(string body, string from, string password)
        {
            try
            {
                using (var mail = new MailMessage())
                {
                    mail.To.Add("amrelsher07@gmail.com");
                    mail.From = new MailAddress(from);
                    mail.IsBodyHtml = true;
                    mail.Body = $"<h3>{body}</h3>";
                    mail.Subject = "MY DOCTOR WEB";
                    using (var smtp = new SmtpClient())
                    {
                        smtp.Credentials = new NetworkCredential
                        {
                            UserName = from,
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
            //if (id == null) return NotFound();
            //var doctor = await _doctorRepository.GetDocWithPostsAsync(id);
            //if (doctor == null) return NotFound();
            return View();
        }

        // GET: Doctors/Create
        public IActionResult Create()
        {
            return View();
        }
    }
}