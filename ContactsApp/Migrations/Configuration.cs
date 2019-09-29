namespace ContactsApp.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ContactsApp.ContactModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ContactsApp.ContactModel context)
        {
            if (!context.Contacts.Any())
            {
                context.Contacts.Add(FirstContact());
                context.Contacts.Add(SecondContact());

                context.SaveChanges();
            }
        }
               
        private static Contact FirstContact()
        {
            var contactNumbers = new List<ContactNumber>()
                {
                    new ContactNumber
                    {
                        ContactNumberType = ContactNumberType.Cell,
                        Number = "0123456789"
                    },
                    new ContactNumber
                    {
                        ContactNumberType = ContactNumberType.LandLine,
                        Number = "0723658974"
                    }
                };

            var emailAddresses = new List<Email>()
                {
                    new Email()
                    {
                        EmailAddress = @"JohnSmith1234@gmail.com",
                    },
                    new Email()
                    {
                        EmailAddress = @"JohnSmith1234@outlook.com",
                    }
                };

            var contact = new Contact()
            {
                FirstName = @"John",
                LastName = @"Smith",
                ContactNumbers = contactNumbers,
                EmailAddresses = emailAddresses
            };
            return contact;
        }
        private static Contact SecondContact()
        {
            var contactNumbers = new List<ContactNumber>()
                {
                    new ContactNumber
                    {
                        ContactNumberType = ContactNumberType.Cell,
                        Number = "0658756981"
                    },
                    new ContactNumber
                    {
                        ContactNumberType = ContactNumberType.Cell,
                        Number = "0862546985"
                    },
                    new ContactNumber
                    {
                        ContactNumberType = ContactNumberType.LandLine,
                        Number = "0123456789"
                    },
                    new ContactNumber
                    {
                        ContactNumberType = ContactNumberType.LandLine,
                        Number = "0313568748"
                    }
                };

            var emailAddresses = new List<Email>()
                {
                    new Email()
                    {
                        EmailAddress = @"AlbertWilliams7777@gmail.com",
                    },
                    new Email()
                    {
                        EmailAddress = @"AlbertWilliams4444@gmail.com",
                    }
                };

            var contact = new Contact()
            {
                FirstName = @"Albert",
                LastName = @"Williams",
                ContactNumbers = contactNumbers,
                EmailAddresses = emailAddresses
            };
            return contact;
        }
    }
}
