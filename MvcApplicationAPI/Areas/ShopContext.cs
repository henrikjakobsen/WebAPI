using System.Data.Entity;
using MvcApplicationAPI.Models;

namespace MvcApplicationAPI.Areas
{
    public class ShopContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
    }
}