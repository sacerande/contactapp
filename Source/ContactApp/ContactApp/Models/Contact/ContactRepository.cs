using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactApp.Models
{
    public class ContactRepository : IContactRepository
    {
        private List<Contact> lstContacts = new List<Contact>() { 
        new Contact { Id = 1000,firstName = "Sachin", lastName = "Erande", email = "sac_erande@yahoo.co.in", phoneNumber = "+91-7709862546", status = status.Active },
        new Contact { Id = 1001,firstName = "Swapnil", lastName = "Patil", email = "swapnil_patil@yahoo.co.in", phoneNumber = "+91-020123456789", status = status.InActive },
        new Contact { Id = 1002,firstName = "Rahul", lastName = "Patil", email = "rahul_patil@gmail.com", phoneNumber = "+91-123456789", status = status.Active }
        };
        private int _nextId = 1003;

        public IEnumerable<Contact> GetAll()
        {
            return lstContacts;
        }

        public Contact Get(int id)
        {
            return lstContacts.Find(c => c.Id == id);
        }

        public Contact Add(Contact item)
        {
            //check if duplicate item.
            String validationError = ValidateAdd(item);
            if (validationError != null) { throw new Exception(validationError); }

            item.Id = _nextId++;
            lstContacts.Add(item);
            return item;
        }

        public void Remove(int id)
        {
            lstContacts.RemoveAll(p => p.Id == id);
        }

        public bool Update(Contact item)
        {
            int index = lstContacts.FindIndex(c => c.Id == item.Id);
            if (index == -1)
            {
                return false;
            }
            lstContacts.RemoveAt(index);
            lstContacts.Add(item);
            return true;
        }        

        #region validation methods
        public String ValidateAdd(Contact item) {
            if (item == null) { return "Received Null/Empty Contact Object. item to be added can't be null."; }
            if (lstContacts.Any(x => x.Id == item.Id)) { return String.Format("Contact having Id {0} exists in repository.", item.Id); }
            if (lstContacts.Any(x => x.email == item.email)) { return String.Format("Contact having same email address {0} exists in repository.", item.email); }
            if (lstContacts.Any(x => x.phoneNumber == item.phoneNumber)) { return String.Format("Contact having same phone number {0} exists in repository.", item.phoneNumber); }
            return null;
        }
        public String ValidateUpdate(Contact item)
        {
            if (item == null) { return "Received Null/Empty Contact Object. item to be updated can't be null."; }
            if (lstContacts.Any(x => x.Id == item.Id && x.Id != item.Id)) { return String.Format("Contact having Id {0} exists in repository.", item.Id); }
            if (lstContacts.Any(x => x.email == item.email && x.Id!= item.Id)) { return String.Format("Contact having same email address {0} exists in repository.", item.email); }
            if (lstContacts.Any(x => x.phoneNumber == item.phoneNumber && x.Id != item.Id)) { return String.Format("Contact having same phone number {0} exists in repository.", item.phoneNumber); }
            return null;
        }
        #endregion
    }
}
