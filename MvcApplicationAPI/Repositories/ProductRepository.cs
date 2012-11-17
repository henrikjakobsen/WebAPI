using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using MvcApplicationAPI.Areas;
using MvcApplicationAPI.Models;

namespace MvcApplicationAPI.Repositories
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetAll();
        Product Get(int id);
        Product Add(Product item);
        void Remove(int id);
        bool Update(Product item);
    }

    public class ProductRepository : IProductRepository
    {
        private readonly ShopContext _db;

        public ProductRepository()
        {
            _db = new ShopContext();
        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }

        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }

        public Product Add(Product item)
        {
            _db.Products.Add(item);
            _db.SaveChanges();
            return item;
        }

        public void Remove(int id)
        {
            Product product = _db.Products.Find(id);
            _db.Products.Remove(product);
            _db.SaveChanges();
        }

        public bool Update(Product item)
        {
            _db.Entry(item).State = EntityState.Modified;
            _db.SaveChanges();
            return true;
        }
    }
}