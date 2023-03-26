using DoanNhom10.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanNhom10.Validation
{
    public class CategoryAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext
validationContext)
        {
            int CategoryID = int.Parse(value.ToString());
            var db = new ApplicationDbContext();
            if (db.Categories.Any(x => x.ID == CategoryID))
            {
                return ValidationResult.Success;
            }
            return new ValidationResult(
            ErrorMessage ?? "Category khong ton tai");
        }
    }
}