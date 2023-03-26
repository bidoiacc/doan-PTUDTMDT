using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DoanNhom10.Models
{
    public class OrderDetail
    {
        [Key]
        public int OrderDetailID { get; set; }
        [ForeignKey("OrderObj")]
        public int OrderID { get; set; }
        public virtual Order OrderObj { get; set; }
        [ForeignKey("ProductObj")]
        public int ProductID { get; set; }
        public virtual Product ProductObj { get; set; }
        public int Amount { get; set; }
    }
}