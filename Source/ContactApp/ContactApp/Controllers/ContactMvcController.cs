using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using ContactApp.Models;

namespace ContactApp.Controllers
{
    public class ContactController : Controller
    {
        public String BaseAddress = null;
        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            BaseAddress = "http://" + requestContext.HttpContext.Request.Url.Authority + "/api/";
            base.Initialize(requestContext);
        }

        // GET: /Contact/
        public ActionResult Index()
        {
            IEnumerable<Contact> lstContacts = Enumerable.Empty<Contact>();
            try
            {
                lstContacts = WebAPIConsumer.GetAllObjects<Contact>(BaseAddress + "contactapi");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }                       
            return View(lstContacts);
        }

        // GET: /Contact/Details/5
        public ActionResult Details(int id)
        {
            Contact contact = WebAPIConsumer.GetObject<Contact>(BaseAddress + "contactapi?id=" + id.ToString());
            if (contact == null) {
                ModelState.AddModelError("ItemNotFound", "Item not found.");
                return RedirectToAction("Index"); 
            }
            return View(contact);
        }

        // GET: /Contact/Create
        public ActionResult Create()
        {
            return View();
        }
        
        // POST: /Contact/Create
        [HttpPost]
        public ActionResult Create(Contact Contact)
        {
            if (ModelState.IsValid == false) { return View(Contact); }
            try
            {
                System.Net.HttpStatusCode httpStatusCode = WebAPIConsumer.CreateObject<Contact>(BaseAddress + "contactapi", Contact);
                if (httpStatusCode == System.Net.HttpStatusCode.Created)
                    return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return View(Contact);
        }

        // GET: /Contact/Edit/5
        public ActionResult Edit(int id)
        {
            Contact contact = WebAPIConsumer.GetObject<Contact>(BaseAddress + "contactapi?id=" + id.ToString());
            if (contact == null)
            {
                ModelState.AddModelError("ItemNotFound", "Item not found.");
                return RedirectToAction("Index");
            }
            return View(contact);     
        }

        // POST: /Contact/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Contact Contact)
        {
            if (ModelState.IsValid == false) { return View(Contact); }
            try
            {
                System.Net.HttpStatusCode httpStatusCode = WebAPIConsumer.UpdateObject<Contact>(BaseAddress + "contactapi", Contact);
                if (httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex) {
                HandleException(ex);
            }
            return View(Contact);         
        }

        // GET: /Contact/Delete/5
        public ActionResult Delete(int id)
        {
            Contact contact = WebAPIConsumer.GetObject<Contact>(BaseAddress + "contactapi?id=" + id.ToString());
            if (contact == null)
            {
                ModelState.AddModelError("ItemNotFound", "Item not found.");
                return RedirectToAction("Index");
            }
            return View(contact);         
        }

        // POST: /Contact/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Contact Contact)
        {
            try
            {
                System.Net.HttpStatusCode httpStatusCode = WebAPIConsumer.DeleteObject<Contact>(BaseAddress + "contactapi/" + id.ToString());
                if (httpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                HandleException(ex);
            }
            return View(Contact);
        }


        #region NonAction Methods
        [NonAction]
        public void HandleException(Exception ex) {
            if (ex is System.Web.Http.HttpResponseException)
                ModelState.AddModelError(String.Empty, ((System.Web.Http.HttpResponseException)ex).Response.ErrorMessage());
            else
                ModelState.AddModelError(String.Empty, ex.Message);
        }
        #endregion
    }
}
