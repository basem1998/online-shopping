using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class SizeGroup
    {
        public int Id { get; set; }


        [Display(Name="Name_Of_sizeGroup")]
        public string Name { get; set; }

      

        public int producttypeId { get; set; }
        public virtual Producttype productype { get; set; }


       
        public virtual ICollection<Size> Sizes { get; set; }
    }
}