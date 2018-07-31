using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ContactApp.Models
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        Contact Get(int id);
        Contact Add(Contact item);
        void Remove(int id);
        bool Update(Contact item);

        string ValidateAdd(Contact item);
        string ValidateUpdate(Contact item);
    }
}
