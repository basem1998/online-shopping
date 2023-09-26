using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class Order
    {
        public int Id                                   { get; set; }
        public int NumberOfOrder                        { get; set; }
        public DateTime OrderDate                        { get; set; }
        public int CustomerId                           { get; set; }
        public virtual Customer Customer                { get; set; }
        public virtual ICollection<OrderItem>OrderItems { get; set; }
    }
}