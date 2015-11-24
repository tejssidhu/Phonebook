using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;

namespace Phonebook.UI.Controllers
{
    public class ContactNumberApiController : ApiController
    {
        private readonly IContactNumberService _contactNumberService;
        private readonly IContactService _contactService;

        public ContactNumberApiController(IContactNumberService contactNumberService, IContactService contactService)
        {
            _contactNumberService = contactNumberService;
            _contactService = contactService;
        }

        // GET api/<controller>
        [HttpGet]
        public IEnumerable<ContactNumber> GetAll(Guid contactId)
        {
            return _contactNumberService.GetAllByContactId(contactId);
        }

        // GET api/<controller>/5
        public ContactNumber Get(Guid contactNumberId)
        {
            return _contactNumberService.Get(contactNumberId);
        }

        // POST api/<controller>
        public void Post([FromBody]ContactNumber contactNumber)
        {
            _contactNumberService.Create(contactNumber);
        }

        // PUT api/<controller>/5
        public void Put(Guid contactNumberId, [FromBody]ContactNumber contactNumber)
        {
            _contactNumberService.Update(contactNumber);
        }

        // DELETE api/<controller>/5
        public void Delete(Guid contactNumberId)
        {
            _contactNumberService.Delete(contactNumberId);
        }
    }
}