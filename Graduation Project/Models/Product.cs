   using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Graduation_Project.Models
{
    public class Product
    {
       
        public Guid Id                            { get; set; }

        [Display(Name="Product Name")]
        public string Name                        { get; set; }
        public string Discription                 { get; set; }
        public string CountryofOrigin             { get; set; }
        public bool IsActive                      { get; set; }
        public string Image                       { get; set; }
        public double Price                       { get; set; }
        public double Discount                    { get; set; }




        [Required]
        [Display(Name = "Color")]
        public int ColorId                        { get; set; }
        public virtual Color Color                { get; set; }

      
        [Display(Name = "ProductType")]
        public int ProducttypeId                  { get; set; }
        public virtual Producttype Producttype    { get; set; }

       
        [Display(Name = "Category")]
        public int CategoryId                     { get; set; }
        public virtual Category Category          { get; set; }
      
       
        public int GenderId                       { get; set; }

        [Display(Name = "Gender")]
        public virtual Gender Gender              { get; set; }

        
        [Display(Name = "Brand")]
        public int BrandId                        { get; set; }
        public virtual Brand Brand                { get; set; }

        public virtual ICollection<Order> orders  { get; set; }
       

       
    }
}