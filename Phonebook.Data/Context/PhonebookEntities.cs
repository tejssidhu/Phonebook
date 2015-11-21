﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Phonebook.Data.JsonUtilities;
using Phonebook.Domain.Model;
using Phonebook.Data.Properties;

namespace Phonebook.Data.Context
{
    public class PhonebookContext : IDisposable
    {
        private readonly string _usersFilePath;
        private readonly string _contactsFilePath;
        private readonly string _contactNumbersFilePath;

        public IList<User> Users { get; set; }
        public IList<Contact> Contacts { get; set; }
        public IList<ContactNumber> ContactNumbers { get; set; }
        
        public PhonebookContext()
        {
            //TODO: magic settings - need to remove and create configuration class that gets passed into constructor
            var folderPath = Settings.Default.FilePaths;
            var usersFileName = Settings.Default.UserFile;
            var contactsFileName = Settings.Default.ContactFile;
            var contactNumbersFileName = Settings.Default.ContactNumbersFile;

            if (!Directory.Exists(folderPath))
            {
                throw new DirectoryNotFoundException(folderPath + " not found");
            }

            if (folderPath.EndsWith("\\"))
            {
                _usersFilePath = folderPath + usersFileName;
                _contactsFilePath = folderPath + contactsFileName;
                _contactNumbersFilePath = folderPath + contactNumbersFileName;
            }
            else
            {
                _usersFilePath = folderPath + "\\" + usersFileName;
                _contactsFilePath = folderPath + "\\" + contactsFileName;
                _contactNumbersFilePath = folderPath + "\\" + contactNumbersFileName;
            }

            LoadUsersFile();
            LoadContactsFile();
            LoadContactNumbersFile();
        }

        //Json.NET
        private void LoadUsersFile()
        {
            if (File.Exists(_usersFilePath))
            {
                try
                {
                    Users = JsonSerialization.ReadFromJsonFile<List<User>>(_usersFilePath) ?? new List<User>();
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem reading from " + _usersFilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_usersFilePath + " not found");
            }
        }

        private void SaveUsersFile()
        {
            if (File.Exists(_usersFilePath))
            {
                try
                {
                    JsonSerialization.WriteToJsonFile<List<User>>(_usersFilePath, Users.ToList());
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem writing to " + _usersFilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_usersFilePath + " not found");
            }
        }

        private void LoadContactsFile()
        {
            if (File.Exists(_contactsFilePath))
            {
                try
                {
                    Contacts = JsonSerialization.ReadFromJsonFile<List<Contact>>(_contactsFilePath) ?? new List<Contact>();
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem reading from " + _contactsFilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_contactsFilePath + " not found");
            }
        }

        private void SaveContactsFile()
        {
            if (File.Exists(_contactsFilePath))
            {
                try
                {
                    JsonSerialization.WriteToJsonFile<List<Contact>>(_contactsFilePath, Contacts.ToList());
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem writing to " + _contactsFilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_contactsFilePath + " not found");
            }
        }

        private void LoadContactNumbersFile()
        {
            if (File.Exists(_contactNumbersFilePath))
            {
                try
                {
                    ContactNumbers = JsonSerialization.ReadFromJsonFile<List<ContactNumber>>(_contactNumbersFilePath) ?? new List<ContactNumber>();
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem reading from " + _contactNumbersFilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_contactNumbersFilePath + " not found");
            }
        }

        private void SaveContactNumbersFile()
        {
            if (File.Exists(_contactNumbersFilePath))
            {
                try
                {
                    JsonSerialization.WriteToJsonFile<List<ContactNumber>>(_contactNumbersFilePath, ContactNumbers.ToList());
                }
                catch (Exception)
                {
                    throw new Exception("There was a problem writing to " + _contactNumbersFilePath);
                }
            }
            else
            {
                throw new DirectoryNotFoundException(_contactNumbersFilePath + " not found");
            }
        }

        public void SaveUserChanges()
        {
            SaveUsersFile();
        }

        public void SaveContactChanges()
        {
            SaveContactsFile();
        }

        public void SaveContactNumberChanges()
        {
            SaveContactNumbersFile();
        }

        public void Dispose()
        {
            SaveUsersFile();
            SaveContactsFile();
            SaveContactNumbersFile();
            Users = null;
            Contacts = null;
            ContactNumbers = null;
        }
    }
}
