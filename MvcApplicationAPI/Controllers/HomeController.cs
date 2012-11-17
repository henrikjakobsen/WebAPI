using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplicationAPI.Models;
using MvcApplicationAPI.Repositories;

namespace MvcApplicationAPI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var repo = new ProductRepository();
            repo.Add(new Product() {Name = "Test"});
            

            return View();
        }
    }
}
