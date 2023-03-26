using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanNhom10.Models
{
    public class Category
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Danh mục")]
        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        public virtual IList<Product> Product { get; set; }
    }
}