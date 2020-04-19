using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BulkyBook.Models
{
    public class Category
    {
        // With entity framework if the name of a property is Id will automatically be a primary key
        // OTherwise have to explicitly annotate it with a [Key]
        public int Id { get; set; }
       
        [Display(Name="Category Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        // We will use a repository here that will allow for common accessing methods for the DB.
        // This has 2 parts:
            // 1 - The IRepository: this is an interface (protocol) with the generic method definitions
            // 2 - The Repository: this class will provide the implementation of the repository interface methods
    }
}
