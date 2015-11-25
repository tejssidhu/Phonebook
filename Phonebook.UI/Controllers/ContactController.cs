using Phonebook.Domain.Interfaces.Services;
using System;
using System.Web.Mvc;

namespace Phonebook.UI.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly IContactService _contactService;

        public ContactController(IContactService contactService)
        {
            _contactService = contactService;
        }

        public ActionResult Index(Guid userId)
        {
            return View(userId);
        }
    }
}
