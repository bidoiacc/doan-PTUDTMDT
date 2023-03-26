using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoanNhom10.Models
{
    public class Product
    {
        public int ID { get; set; }


        [Display(Name = "Tên sản phẩm")]
        [Required]
        [StringLength(200, MinimumLength = 3)]
        public string Name { get; set; }


        [Display(Name = "Mô tả")]
        [StringLength(int.MaxValue, MinimumLength = 3)]
        public string Description { get; set; }


        [Display(Name = "Giá")]
        [Range(5000, double.MaxValue)]
        [DisplayFormat(DataFormatString = "{0:0,000}")]
        public decimal Price { get; set; }


        public byte[] Picture { get; set; }
        [NotMapped]
        [Display(Name = "Hình ảnh")]
        public HttpPostedFileBase PictureUpload { get; set; }


        [ForeignKey("CategoryObj")]
        public int? CategoryID { get; set; }
        public virtual Category CategoryObj { get; set; }
    }
}