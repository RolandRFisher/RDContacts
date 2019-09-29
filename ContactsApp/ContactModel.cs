namespace ContactsApp
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Linq;

    public class ContactModel : DbContext
    {
        // Your context has been configured to use a 'ContactModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'ContactsApp.ContactModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'ContactModel' 
        // connection string in the application configuration file.
        public ContactModel()
            : base("name=ContactModel")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Email>().Property(prop => prop.EmailAddress).HasMaxLength(254);
            //modelBuilder.Entity<Email>().HasIndex(ix => new {ix.EmailAddress}).IsUnique();

            //modelBuilder.Entity<ContactNumber>().HasIndex(ix => new {ix.Number, ix.ContactId}).IsUnique();
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Contact> Contacts { get; set; }

        public System.Data.Entity.DbSet<ContactsApp.Email> Emails { get; set; }

        public System.Data.Entity.DbSet<ContactsApp.ContactNumber> ContactNumbers { get; set; }
    }

    public class Contact
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public List<Email> EmailAddresses { get; set; }
        public List<ContactNumber> ContactNumbers { get; set; }
    }

    public class Email
    {
        public int Id { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }

    public class ContactNumber
    {
        public int Id { get; set; }
        [Required]
        public ContactNumberType ContactNumberType { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string Number { get; set; }

        public int ContactId { get; set; }
        public Contact Contact { get; set; }
    }

    public enum ContactNumberType
    {
        LandLine = 1,
        Cell = 2
    }

}