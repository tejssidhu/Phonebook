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
    [Authorize]
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
        [ActionName("GetAllByGUID")]
        public IEnumerable<ContactNumber> GetAll(Guid id)
        {
            Contact contact = _contactService.Get(id);

            if (contact != null)
            {
                return _contactNumberService.GetAllByContactId(id);
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        // GET api/<controller>/5
        public ContactNumber Get(Guid id, Guid itemId)
        {
             Contact contact = _contactService.Get(id);

            if (contact != null)
            {
                var contactNumber = _contactNumberService.Get(itemId);

                if (contactNumber == null)
                {
                    throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
                }

                return contactNumber;
            }

            throw new HttpResponseException(HttpStatusCode.Unauthorized);
        }

        // POST api/<controller>
        public HttpResponseMessage Post([FromBody]ContactNumber contactNumber)
        {
            var httpResponse = new HttpResponseMessage();

            if (ModelState.IsValid)
            {
                Contact contact = _contactService.Get(contactNumber.ContactId);

                if (contact != null)
                {
                    try
                    {
                        _contactNumberService.Create(contactNumber);
                        httpResponse = Request.CreateResponse(HttpStatusCode.Created, contactNumber);
                        httpResponse.Headers.Location = new Uri(Url.Link("DefaultDoubleGuidApi", new { id = contactNumber.ContactId, itemId = contactNumber.Id }));

                        return httpResponse;
                        //httpResponse.Headers.Location = need to set Uri of new resource
                    }
                    catch (Exception ex)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                    }
                }

                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            return httpResponse;
        }

        // PUT api/<controller>/5
        public HttpResponseMessage Put(Guid id, [FromBody]ContactNumber contactNumber)
        {
            if (!ModelState.IsValid)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }

            if (id != contactNumber.Id)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            var httpResponse = new HttpResponseMessage();

            Contact contact = _contactService.Get(contactNumber.ContactId);

            if (contact != null)
            {
                try
                {
                    ContactNumber existingContactNumber = _contactNumberService.Get(contactNumber.Id);

                    if (existingContactNumber != null)
                    {
                        _contactNumberService.Update(contactNumber);

                        httpResponse.StatusCode = HttpStatusCode.OK;
                    }
                    else
                        httpResponse.StatusCode = HttpStatusCode.NotFound;
                }
                catch (Exception ex)
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
                }
            }
            else
            {
                httpResponse.StatusCode = HttpStatusCode.Unauthorized;
            }

            return httpResponse;
        }

        // DELETE api/<controller>/5
        public HttpResponseMessage Delete(Guid id)
        {
            ContactNumber contactNumber = _contactNumberService.Get(id);

            if (contactNumber == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            try
            {
                _contactNumberService.Delete(id);

                return Request.CreateResponse(HttpStatusCode.OK, contactNumber);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, ex);
            }
        }

        protected override void Dispose(bool disposing)
        {
            _contactNumberService.Dispose();
            _contactService.Dispose();

            base.Dispose(disposing);
        }
    }
}