using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ContactApp.Models;

namespace ContactApp.Controllers
{
    public class ContactApiController : ApiController
    {
        //static readonly IContactRepository repository = new ContactRepository();
        private IContactRepository repository;
        public ContactApiController(IContactRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<Contact> Get()
        {
            return repository.GetAll();
        }

        public Contact Get(int id)
        {
            Contact item = repository.Get(id);
            if (item == null)
                ContactUtil.ThrowHttpResponseException("Contact having id " + id + "not found", HttpStatusCode.NotFound, "Item not found");
            return item;
        }

        public HttpResponseMessage Post(Contact item)
        {
            //check repository validation for update.
            String validationError = repository.ValidateAdd(item);
            if (validationError != null) { ContactUtil.ThrowHttpResponseException(validationError, HttpStatusCode.BadRequest, "Add validation failed"); }

            // insert item into repository
            item = repository.Add(item);
            var response = Request.CreateResponse<Contact>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Put(Contact item)
        {
            //check repository validation for update.
            String validationError = repository.ValidateUpdate(item);
            if (validationError != null) { ContactUtil.ThrowHttpResponseException(validationError, HttpStatusCode.BadRequest, "Update validation failed"); }

            //update item in repository.
            if (!repository.Update(item))
                ContactUtil.ThrowHttpResponseException("Contact having id " + item.Id + " not found.", HttpStatusCode.NotFound, "Item to be updated not found.");

            var response = Request.CreateResponse<Contact>(HttpStatusCode.OK, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage Delete(int id)
        {
            //check if item to be deleted exist in repository.
            Contact item = repository.Get(id);
            if (item == null)
                ContactUtil.ThrowHttpResponseException("Contact having id " + id + "not found", HttpStatusCode.NotFound, "Item not found");

            repository.Remove(id);

            var response = Request.CreateResponse<Contact>(HttpStatusCode.OK, item);
            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }
    }
}