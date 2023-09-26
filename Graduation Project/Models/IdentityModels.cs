using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Graduation_Project.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Product> Products          { get; set; }
        public DbSet<Customer> Customers        { get; set; }
        public DbSet<Gender> Genders            { get; set; }
        public DbSet<Brand> Brands              { get; set; }
        public DbSet<Producttype> producttypes { get; set; }
        public DbSet<Color> Colors              { get; set; }
        public DbSet<Order> orders              { get; set; }
        public DbSet<Category> Categoerys       { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Graduation_Project.Models.OrderItem> OrderItems { get; set; }

        public System.Data.Entity.DbSet<Graduation_Project.Models.Size> Sizes { get; set; }

        public System.Data.Entity.DbSet<Graduation_Project.Models.SizeGroup> SizeGroups { get; set; }

        public System.Data.Entity.DbSet<Graduation_Project.Models.Country> Countries { get; set; }

        public System.Data.Entity.DbSet<Graduation_Project.Models.City> Cities { get; set; }

        public System.Data.Entity.DbSet<Graduation_Project.Models.Admin> Admins { get; set; }
    }

    
}