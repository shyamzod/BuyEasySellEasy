using BuyEasySellEasy.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace BuyEasySellEasy.Controllers
{

    public class HomeController : Controller
    {
        ApplicationDbContext _context = new ApplicationDbContext();
        public ActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpPost]
        public ActionResult AddProduct(Products product)
        {
            List<string> photobase24strings = new List<string>();
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var photo1 = Request.Files[i];
                    var imgbytes = new Byte[photo1.ContentLength];
                    photo1.InputStream.Read(imgbytes, 0, photo1.ContentLength);
                    var base64string = Convert.ToBase64String(imgbytes, 0, imgbytes.Length);
                    photobase24strings.Add(base64string);
                }
            }
            product.Photo1 = photobase24strings[0];
            product.Photo2 = photobase24strings[1];
            product.Photo3 = photobase24strings[2];
            _context.Products.Add(product);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult AddProduct1()
        {
            return View("~/Views/Admin/AddProduct.cshtml");
        }
        public ActionResult Sort(string item)
        {
            List<Products> products = new List<Products>();
            if (item == "lowtohigh")
            {
                products = _context.Products.OrderBy(p => p.Price).ToList();
            }
            else if (item == "hightolow")
            {
                products = _context.Products.OrderByDescending(p => p.Price).ToList();
            }
            return View("Index", products);
        }
        [HttpPost]
        public ActionResult Search(string searchtext)
        {
            var products = _context.Products.Where(p => p.ProductName.Contains(searchtext)).ToList();
            return View("Index", products);
        }
        public ActionResult ViewProduct(long id)
        {
            var product = _context.Products.Where(p => p.ProductId == id).FirstOrDefault();
            return View(product);
        }
        public ActionResult AddItemToCart(long productid)
        {
            CartItems item = new CartItems();
            var userid = User.Identity.GetUserId();
            if(userid!=null)
            {
                item.UserId = userid;
                item.ProductId = productid;
                _context.CartItems.Add(item);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
        public ActionResult CartItems()
        {
            var userid = User.Identity.GetUserId();
            List<CartItems> cartItems = _context.CartItems.Where(c => c.UserId == userid).ToList();
            var products = (from c in cartItems
            join p in _context.Products
            on c.ProductId equals p.ProductId
            select p).ToList();
            return View(products);
        }
        public ActionResult Delete(long productid)
        {
            var userid = User.Identity.GetUserId();
            var product = _context.CartItems.Where(p => p.ProductId == productid && p.UserId == userid).FirstOrDefault();
            _context.CartItems.Remove(product);
            _context.SaveChanges();
            return RedirectToAction("CartItems");
        }
    }
}