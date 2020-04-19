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
    }
}
