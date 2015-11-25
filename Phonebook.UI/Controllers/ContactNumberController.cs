using Phonebook.Domain.Interfaces.Services;
using System;
using System.Web.Mvc;

namespace Phonebook.UI.Controllers
{
    [Authorize]
    public class ContactNumberController : Controller
    {
        private readonly IContactNumberService _contactNumberService;
        private readonly IContactService _contactService;

        public ContactNumberController(IContactNumberService contactNumberService, IContactService contactService)
        {
            _contactNumberService = contactNumberService;
            _contactService = contactService;
        }

        public ActionResult Index(Guid contactId)
        {
            return View(contactId);
            //var contactNumbers = _contactNumberService.GetAllByContactId(contactId);

            //var contact = _contactService.Get(contactId);
            //ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;
            //ViewBag.ContactId = contact.Id;
            //ViewBag.UserId = contact.UserId;

            //return View(contactNumbers);
        }

        //public ActionResult ManageContactNumber(Guid contactNumberId, Guid contactId)
        //{
        //    var contact = _contactService.Get(contactId);
        //    ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

        //    if (contactNumberId != Guid.Empty)
        //        return View(_contactNumberService.Get(contactNumberId));
        //    else
        //        return View(new ContactNumber { Id = Guid.Empty, ContactId = contactId });
        //}

        //[HttpPost]
        //public ActionResult ManageContactNumber(ContactNumber contactNumber)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(contactNumber);
        //    }

        //    if (contactNumber.Id != Guid.Empty)
        //    {
        //        try
        //        {
        //            _contactNumberService.Update(contactNumber);
        //        }
        //        catch (ObjectAlreadyExistException oae)
        //        {
        //            var contact = _contactService.Get(contactNumber.ContactId);
        //            ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

        //            ModelState.AddModelError("ContactNumber", oae.Message);
        //            return View(contactNumber);
        //        }
        //    }
        //    else
        //    {
        //        try
        //        {
        //            _contactNumberService.Create(contactNumber);
        //        }
        //        catch (ObjectNotFoundException onf)
        //        {
        //            var contact = _contactService.Get(contactNumber.ContactId);
        //            ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

        //            ModelState.AddModelError("TelephoneNumber", onf.Message);
        //            return View(contactNumber);
        //        }
        //        catch (ObjectAlreadyExistException oae)
        //        {
        //            var contact = _contactService.Get(contactNumber.ContactId);
        //            ViewBag.ContactName = contact.Title + " " + contact.Forename + " " + contact.Surname;

        //            ModelState.AddModelError("TelephoneNumber", oae.Message);
        //            return View(contactNumber);
        //        }
        //    }

        //    return RedirectToAction("Index", new {contactNumber.ContactId });
        //}

        //public ActionResult DeleteContactNumber(Guid contactNumberId, Guid contactId)
        //{
        //    _contactNumberService.Delete(contactNumberId);

        //    return RedirectToAction("Index", new { ContactId = contactId });
        //}
    }
}
