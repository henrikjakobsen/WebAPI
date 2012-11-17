using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using MvcApplicationAPI.Models;
using MvcApplicationAPI.Repositories;

namespace MvcApplicationAPI.Controllers
{
    public class ProductsController : ApiController
    {
        private ProductRepository _repository;

        public ProductsController()
        {
            _repository = new ProductRepository();
        }

        private static IEnumerable<Product> _products = new List<Product>()
        { 
            new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 }, 
            new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M }, 
            new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M } 
        };

        public IQueryable<Product> Get()
        {
            return _products.AsQueryable();
        }

        public Product GetProductById(int id)
        {
            var product = _products.FirstOrDefault((p) => p.Id == id);
            if (product == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return product;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return _products.Where((p) => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        public HttpResponseMessage Post(Product product)
        {
            _repository.Add(product);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, product);
            var uri = Url.Route(null, new { id = product.Id });

            response.Headers.Location = new Uri(Request.RequestUri, uri);
            return response;
        }

        public void Put(int id, Product product)
        {
            product.Id = id;

            if (!_repository.Update(product))
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }
        }

        public HttpResponseMessage Delete(int id)
        {
            _repository.Remove(id);
            return new HttpResponseMessage(HttpStatusCode.NoContent);
        }
    }
}
