using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class Color
    {
        public int  Id { get; set; }
        [Display(Name = "Color")]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}