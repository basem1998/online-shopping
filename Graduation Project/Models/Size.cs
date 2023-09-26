using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class Size
    {
        public int Id { get; set; }


        [Display(Name ="Sizes")]
        public string Name { get; set; }
      
        public virtual ICollection<OrderItem> OrderItems { get; set; }

        public int SizegroupId                   { get; set; }
        public virtual SizeGroup Sizegroup       { get; set; }
    }
}