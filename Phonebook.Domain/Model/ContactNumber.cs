using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Phonebook.Domain.Model
{
    public class ContactNumber
    {
        public Guid Id { get; set; }
        
        [Required]
        public Guid ContactId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Description { get; set; }

        [Required]
        [MaxLength(20)]
        [DisplayName("Telephone Number")]
        public string TelephoneNumber { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            ContactNumber cn = (ContactNumber)obj;

            return (ContactId == cn.ContactId) && (Description == cn.Description) && (TelephoneNumber == cn.TelephoneNumber);
        }

        protected bool Equals(ContactNumber other)
        {
            return ContactId.Equals(other.ContactId) && string.Equals(Description, other.Description) && string.Equals(TelephoneNumber, other.TelephoneNumber);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = ContactId.GetHashCode();
                hashCode = (hashCode*397) ^ (Description != null ? Description.GetHashCode() : 0);
                hashCode = (hashCode*397) ^ (TelephoneNumber != null ? TelephoneNumber.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}