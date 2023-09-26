using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<City> Cities { get; set; }
    }
}