using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactApp;
using ContactApp.Controllers;
using ContactApp.Models;
using System.Web.Mvc;
using System.Configuration;

namespace ContactApp.Tests.Controllers
{
    [TestClass]
    public class ContactControllerTest
    {
        private string ContactApiBaseAddress;       
        public ContactControllerTest()
        {            
            ContactApiBaseAddress = ConfigurationManager.AppSettings.Get("contactApiBaseAddress");            
        }

        public IEnumerable<Contact> GetAllContactsFromRepository()
        {
            ContactController obj = new ContactController();
            obj.BaseAddress = ContactApiBaseAddress;

            ViewResult vr = (ViewResult)obj.Index();
            Assert.IsNotNull(vr.Model);
            if (!(vr.Model is IEnumerable<Contact>)) { Assert.Fail("Invalid Model Type"); }
            IEnumerable<Contact> lstContacts = (IEnumerable<Contact>)vr.Model;
            return lstContacts;
        }

        [TestMethod]
        public void Index() {
            ContactController obj = new ContactController();
            obj.BaseAddress = ContactApiBaseAddress;
            ViewResult vr = (ViewResult) obj.Index();
            Assert.IsNotNull(vr.Model);
            if (!(vr.Model is IEnumerable<Contact>)) { Assert.Fail("Invalid Model Type"); }
            IEnumerable<Contact> lstContacts = (IEnumerable<Contact>)vr.Model;
            Assert.AreNotEqual(0, lstContacts.Count());
        }

        [TestMethod]
        public void Create() {
            ContactController obj = new ContactController();
            obj.BaseAddress = ContactApiBaseAddress;
            Contact newContact = new Contact();
            newContact.firstName = "New_Sachin";
            newContact.lastName = "New_Erande";
            newContact.phoneNumber = "+91-1234078900";
            newContact.email = "sac_erande@yahoo1.co.in";
            newContact.status = status.Active;

            var vr = obj.Create(newContact);
            if (!(vr is RedirectToRouteResult)) { Assert.Fail("Creation Failed."); }
            Assert.IsNotNull(vr);

            //Get the latest created record..verify if properties are successfully created..
            var createdItem = GetAllContactsFromRepository().OrderByDescending(x => x.Id).FirstOrDefault();
            Assert.IsNotNull(createdItem);
            Assert.AreEqual(newContact.firstName, createdItem.firstName);
            Assert.AreEqual(newContact.lastName, createdItem.lastName);
            Assert.AreEqual(newContact.phoneNumber, createdItem.phoneNumber);
            Assert.AreEqual(newContact.email, createdItem.email);
            Assert.AreEqual(newContact.status, createdItem.status);
        }

        [TestMethod]
        public void Edit()
        {
            ContactController obj = new ContactController();
            obj.BaseAddress = ContactApiBaseAddress;

            //Get the latest contact to edit...
            var itemToEdit = GetAllContactsFromRepository().OrderByDescending(x => x.Id).FirstOrDefault();
            itemToEdit.firstName = "edit_New_Sachin";
            itemToEdit.lastName = "edit_New_Erande";
            itemToEdit.phoneNumber = "+91-90823677222";
            itemToEdit.email = "edit_sac_erande@yahoo.co.in";
            itemToEdit.status = status.InActive;

            var vr = obj.Edit(itemToEdit.Id, itemToEdit);
            if (!(vr is RedirectToRouteResult)) { Assert.Fail("Edit Failed."); }
            Assert.IsNotNull(vr);

            //Get the edited contact from repository..
            var EditedItem = GetAllContactsFromRepository().Where(x => x.Id == itemToEdit.Id).FirstOrDefault();
            Assert.IsNotNull(EditedItem);

            //check all the properties were updated successfully.
            Assert.AreEqual(EditedItem.Id, itemToEdit.Id);
            Assert.AreEqual(EditedItem.firstName, itemToEdit.firstName);
            Assert.AreEqual(EditedItem.lastName, itemToEdit.lastName);
            Assert.AreEqual(EditedItem.phoneNumber, itemToEdit.phoneNumber);
            Assert.AreEqual(EditedItem.email, itemToEdit.email);
            Assert.AreEqual(EditedItem.status, itemToEdit.status);
        }

        [TestMethod]
        public void Delete() {
            ///Arrange
            ContactController obj = new ContactController();
            obj.BaseAddress = ContactApiBaseAddress;
            //Get the latest contact to delete...
            var itemToDelete = GetAllContactsFromRepository().OrderByDescending(x => x.Id).FirstOrDefault();

            ///Act
            var result = obj.Delete(itemToDelete.Id, itemToDelete);

            //Assert
            Assert.AreEqual(typeof(RedirectToRouteResult), result.GetType());
            //check if the contact doesn't exist in repository.
            var DeletedItem = GetAllContactsFromRepository().Where(x => x.Id == itemToDelete.Id).FirstOrDefault();
            Assert.IsNull(DeletedItem);
        }

    }
}
