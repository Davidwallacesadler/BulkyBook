using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BulkyBook.Models
{
    
    // We Inherit from IdentityUser here so that when we add any new properties here and push migrations to our database it will add new columns to the ASPNET users table
    // This makes it so we can add new properties to the existing ASP Net users that we configured on the solution creation
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        // Not mapped data annotation means this will not be added to our db
        // We are not mapping this because...?
        [NotMapped]
        public string Role { get; set; }
    }
}
