using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MYDoctor.Core.Application.IRepository;
using System.Threading.Tasks;

namespace MyDoctor.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        private readonly IInboxMessageRepsitory _inboxMessageRepsitory;

        public MessagesController(IInboxMessageRepsitory inboxMessageRepsitory)
        {
            _inboxMessageRepsitory = inboxMessageRepsitory;
        }
        public async Task<IActionResult> Index()
        {
            var name = User.Identity.Name;
            var allMessages =await _inboxMessageRepsitory.GetALLMessagesAsync(name);
            return View(allMessages);
        }
        public async Task<IActionResult> MessageDetail(string fromName) {
            var name = User.Identity.Name;
            await _inboxMessageRepsitory.MakeMessagesSeeAsync(name);
            var messages =await _inboxMessageRepsitory.MessagesDetails(fromName,name);
            return View(messages);
        }
        public IActionResult SentMessage()
        {
            return View();
        }
        public IActionResult Support() {
            return View();
        }
    }
}