using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class Customer
    {
        public int Id                                 { get; set; }
        [Display(Name = "Customer Name")]
        public string Name                            { get; set; }
        public string Address                          { get; set; }

        public string PhoneNumber                     { get; set; }
        public virtual ICollection<Order> Orders      { get; set; }
        [Required]
        [Display(Name ="City")]
        public int CityId                          { get; set; }
        public virtual City City                { get; set; }
        public string UserName               { get; set; }
    }
}