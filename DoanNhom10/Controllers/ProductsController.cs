using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using DoanNhom10.Models;
using Microsoft.AspNet.Identity;

namespace DoanNhom10.Controllers
{
    public class ProductsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Products
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.CategoryObj);
            return View(products.ToList());
        }


        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,Description,Price,Picture,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                product.Picture = new byte[product.PictureUpload.ContentLength];
                product.PictureUpload.InputStream.Read(product.Picture, 0,
                product.PictureUpload.ContentLength);
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }


        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Name,Description,Price,Picture,CategoryID")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "ID", "Name", product.CategoryID);
            return View(product);
        }



        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddToCart(int id)
        {
            //Kiem tra Id movie ton tai hay khong
            var product = db.Products.Where(x => x.ID == id).FirstOrDefault();
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            var order = this.Session["Order"] as Order;
            if (order == null)
            {
                order = new Order();
                order.CreateAt = DateTime.Now;
                order.OrderDetails = new List<OrderDetail>();
                this.Session["Order"] = order;
                db.Orders.Add(order);
            }
            //Kiem tra don hang da co truoc do
            var orderDetail = order.OrderDetails.Where(x => x.ProductObj.ID ==
           id).FirstOrDefault();

            if (orderDetail == null)
            {
                orderDetail = new OrderDetail();
                orderDetail.ProductID = id;
                orderDetail.ProductObj = product;
                orderDetail.OrderObj = order;
                orderDetail.Amount = 1;
                order.OrderDetails.Add(orderDetail);
            }
            else
            {
                orderDetail.Amount++;
            }
            db.SaveChanges();
            return View(order);
        }

        public ActionResult RemoveFromCart(int productID)
        {
            var order = this.Session["Order"] as Order;
            var orderDetail = order.OrderDetails.FirstOrDefault(x => x.ProductObj.ID ==
productID);

            if (orderDetail != null)
            {
                order.OrderDetails.Remove(orderDetail);
                db.SaveChanges();
            }    


            return View("AddToCart", order);
        }

        public PartialViewResult Total()
        {
            var order = this.Session["Order"] as Order;
            if (order == null)
            {
                return null;
            }
            return PartialView(order);
        }

        public ActionResult Checkout()
        {
            return View(new ShippingDetail());
        }
        [HttpPost]
        public ActionResult Checkout(ShippingDetail detail)
        {
            var order = this.Session["Order"] as Order;
            if (order.OrderDetails.Count() == 0)
            {
                ModelState.AddModelError("", "Sorry, your cart is empty!");
            }
            if (ModelState.IsValid)
            {
                StringBuilder body = new StringBuilder()
                .AppendLine("A new order has been submitted")
                .AppendLine("---")
                .AppendLine("Items:");
                foreach (var orderDetail in order.OrderDetails)
                {
                    var subtotal = orderDetail.ProductObj.Price * orderDetail.Amount;
                    body.AppendFormat("{0} x {1} (subtotal: {2:c}",
                   orderDetail.Amount,
                    orderDetail.ProductObj.Name,
                   subtotal);
                }
                body.AppendFormat("Total order value: {0:c}", order.Total)
                .AppendLine("---")
                .AppendLine("Ship to:")
               .AppendLine(detail.Name)
               .AppendLine(detail.Address)
               .AppendLine(detail.Phone.ToString());
                _ = EmailServiceNew.SendEmail(new IdentityMessage()
                {
                    Destination = detail.Email,
                    Subject = "New order submitted!",
                    Body = body.ToString()
                });
                this.Session["Order"] = null;
                return View("CheckoutCompleted");
            }
            else
            {
                return View(new ShippingDetail());
            }
        }
    }
}
