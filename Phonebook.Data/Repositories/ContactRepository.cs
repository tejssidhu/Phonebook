﻿using Phonebook.Data.Context;
using Phonebook.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Phonebook.Data.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly PhonebookContext _phonebookContext;

        public ContactRepository(Configuration config)
        {
            _phonebookContext = new PhonebookContext(config);
        }

        public IList<Domain.Model.Contact> GetAll()
        {
            return _phonebookContext.Contacts;
        }

        public Domain.Model.Contact Get(Guid id)
        {
            return _phonebookContext.Contacts.FirstOrDefault(c => c.Id == id);
        }

        public Guid Create(Domain.Model.Contact model)
        {
            model.Id = Guid.NewGuid();

            _phonebookContext.Contacts.Add(model);

            _phonebookContext.SaveContactChanges();

            return model.Id;
        }

        public void Update(Domain.Model.Contact model)
        {
            var contact = Get(model.Id);

            if (contact != null)
            {
                contact.Title = model.Title;
                contact.Forename = model.Forename;
                contact.Surname = model.Surname;
                contact.Email = model.Email;
            }

            _phonebookContext.SaveContactChanges();
        }

        public void Delete(Guid id)
        {
            var contactNumbers = _phonebookContext.ContactNumbers.Where(cn => cn.ContactId == id).ToList();

            foreach (var contactNumber in contactNumbers)
            {
                _phonebookContext.ContactNumbers.Remove(contactNumber);
            }
            _phonebookContext.SaveContactNumberChanges();

            _phonebookContext.Contacts.Remove(_phonebookContext.Contacts.FirstOrDefault(c => c.Id == id));

            _phonebookContext.SaveContactChanges();
        }

        public void Dispose()
        {
            _phonebookContext.Dispose();
        }
    }
}
