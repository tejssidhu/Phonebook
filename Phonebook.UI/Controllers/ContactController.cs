using System;
using System.Web.Mvc;

namespace Phonebook.UI.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        public ActionResult Index(Guid userId)
        {
            return View(userId);
        }
    }
}
