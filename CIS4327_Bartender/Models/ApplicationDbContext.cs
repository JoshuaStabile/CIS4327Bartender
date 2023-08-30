using CIS4327_Bartender.Models.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CIS4327_Bartender.Models
{
    public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }
        public DbSet<Data.AppUser> AppUsers { get; set; }
        public DbSet<Data.Cocktail> Cocktails { get; set; }
        public DbSet<Data.Order> Orders { get; set; }
        public DbSet<Cart.CartLine> CartLines { get; set; }
    }
}
