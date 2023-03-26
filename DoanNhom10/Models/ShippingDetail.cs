﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanNhom10.Models
{
    public class ShippingDetail
    {
        public int ID { get; set; }

        [Display(Name = "Tên")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Địa chỉ")]
        [Required]
        public string Address { get; set; }

        [Display(Name = "Email")]
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "Số điện thoại")]
        [Required]
        public int Phone { get; set; }

        [Display(Name = "Ngày giao")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode =
       true)]
        public DateTime ReleaseDate { get; set; }
    }
}