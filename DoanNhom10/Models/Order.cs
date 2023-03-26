using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DoanNhom10.Models
{
    public class Order
    {
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
        [Key]
        public int OrderID { get; set; }
        public IList<OrderDetail> OrderDetails { get; set; }
        public double Total { get; set; }
        public DateTime CreateAt { get; set; }
        public int? ClientID { get; set; }
    }
}