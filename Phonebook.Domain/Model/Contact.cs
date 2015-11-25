using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Domain.Model
{
    public class Contact
    {
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [MaxLength(100)]
        public string Title { get; set; }
        
        [MaxLength(100)]
        public string Forename { get; set; }

        [MaxLength(100)]
        public string Surname { get; set; }
        
        [Required]
        [EmailAddress]
        [DataType(DataType.EmailAddress)]
        [MaxLength(100)]
        public string Email { get; set; }

        public List<ContactNumber> ContactNumbers { get; set; }

        public Contact()
        {
            if (ContactNumbers == null)
                ContactNumbers = new List<ContactNumber>();
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            Contact c = (Contact)obj;

            return (UserId == c.UserId) && (Title == c.Title) && (Forename == c.Forename) && (Surname == c.Surname) && (Email == c.Email);
        }

        protected bool Equals(Contact other)
        {
            return UserId.Equals(other.UserId) && string.Equals(Title, other.Title) && string.Equals(Forename, other.Forename) && string.Equals(Surname, other.Surname) && string.Equals(Email, other.Email);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = UserId.GetHashCode();
                hashCode = (hashCode*397) ^ (Title != null ? Title.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Forename != null ? Forename.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Surname != null ? Surname.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (Email != null ? Email.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
