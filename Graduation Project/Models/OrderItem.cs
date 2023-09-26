using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int OrderId { get; set; }
        public Guid ProductId { get; set; }
        public decimal Price { get; set; }

      [Required,Range(1,10,ErrorMessage = "Error: Must Choose a Country")]
        public int Qty { get; set; }
        public virtual Order Order { get; set; }
        public virtual Product Product { get; set; }
        public int SizeId { get; set; }
        public virtual Size Size { get; set; }

      
    }
}