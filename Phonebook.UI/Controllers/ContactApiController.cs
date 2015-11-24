using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json.Linq;
using Phonebook.Domain.Interfaces.Services;
using Phonebook.Domain.Model;
using Phonebook.UI.Models;

namespace Phonebook.UI.Controllers
{
    public class ContactApiController : ApiController
    {
        private readonly IContactService _contactService;
        private readonly IUserService _userService;

        public ContactApiController(IContactService contactService, IUserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }

        // GET api/<controller>/7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf
        [ActionName("GetAllByGUID")]
        public IEnumerable<Contact> Get(Guid id) 
        {
            User user = _userService.Get(id);

            if (user != null)
            {
                return _contactService.GetAllByUserId(id);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        // GET api/<controller>/7b8ceac1-9fb1-4e15-af4b-890b1f0c3ebf/81c4763c-b225-4756-903a-750064167813
        [ActionName("GetSingleByGUID")]
        public Contact Get(Guid id, Guid itemId)
        {
            User user = _userService.Get(id);

            if (user != null)
            {
                return _contactService.Get((Guid)itemId);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]Contact newContact)
        {
            var httpResponse = new HttpResponseMessage();

            User user = _userService.Get(newContact.UserId);

            if (user != null)
            {
                try
                {
                    _contactService.Create(newContact);
                    httpResponse.StatusCode = HttpStatusCode.Created;
                    //httpResponse.Headers.Location = need to set Uri of new resource
                }
                catch (Exception)
                {
                    httpResponse.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            }

            return httpResponse;
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(Guid id, [FromBody]Contact existingContact)
        {
            var httpResponse = new HttpResponseMessage();

            User user = _userService.Get(existingContact.UserId);

            if (user != null)
            {
                try
                {
                    Contact contact = _contactService.Get(existingContact.Id);

                    if (contact != null)
                    {
                        _contactService.Update(existingContact);

                        httpResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                        httpResponse.StatusCode = HttpStatusCode.NotFound;
                }
                catch (Exception)
                {
                    httpResponse.StatusCode = HttpStatusCode.BadRequest;
                }
            }
            else
            {
                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            }

            return httpResponse;
        }

        // DELETE api/<controller>/5
        public void Delete(Guid id)
        {
            _contactService.Delete(id);
        }
    }
}