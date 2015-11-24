using System;
using Phonebook.Domain.Model;

namespace Phonebook.UI.Models
{
    public class PutPushContact
    {
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public string Title { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
    }
}