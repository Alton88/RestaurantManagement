using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RestaurantManagement.Models;

namespace RestaurantManagement.Controllers
{
    public class OrderItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderItems
        public ActionResult Index()
        {
            //var orderItem = db.OrderItem.Include(o => o.Menu).Include(o => o.Order);
            //return View(orderItem.ToList());

            var orderItem = db.MenuItems.Include(m => m.Menu).ToList();

            return View(orderItem);
        }

        public ActionResult CheckOut()
        {
            var menuItems = db.MenuItems.ToList();
            var makeOrder = new Order { Total = 0 };
            var order = db.Order.Add(makeOrder);
            db.SaveChanges();
            var orderItem = db.OrderItem;
            foreach (var items in menuItems)
            {
                var ot = new OrderItem
                {
                    Quantity = 1,
                    MenuId = items.MenuId,
                    OrderId = order.Id
                };
                db.OrderItem.Add(ot);
                db.SaveChanges();
                db.MenuItems.Remove(items);
            }

            var finalOrder = db.OrderItem.Where(o => o.OrderId == order.Id);

            return Content(order.Id.ToString());
            //return View();

            //var orderItems = db.OrderItem.Where(o => o.OrderId == 1).ToList();
            //var addOrder = new Order {
            //    Total = 0
            //};
            //var order = db.Order.Add(addOrder);
            //db.SaveChanges();

            //foreach (var items in orderItems) {
            //    items.OrderId = order.Id;
            //}

            //return Content(order.Id.ToString());
            //return View();
        }




        // GET: OrderItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItem.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        public ActionResult Create()
        {
            //ViewBag.MenusId = new SelectList(db.Menus, "Id", "Name");
            //ViewBag.OrderId = new SelectList(db.Order, "Id", "Id");
            //ViewBag.OrderId = db.Order.;
            //var orderItem = db.OrderItem.Include(o => o.Menu).ToList();
            //var menuItems = db.Menus.ToList();

            //var orderTotal = new Order
            //{
            //    Total = 0
            //};
            //var order = db.Order.Add(orderTotal);
            //db.SaveChanges();
            //orderItem.OrderId = order.Id;

            //OrderingViewModel menuItems = new OrderingViewModel {
            //    MenusList = db.Menus.ToList(),
            //    OrderId = 10
            //};



            return View(db.Menu.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Quantity,OrderId,MenusId")] OrderItem orderItem)
        {
            var menuItem = db.Menu.SingleOrDefault(m => m.Id == orderItem.MenuId);
            var price = menuItem.Price;
            decimal total = +price * orderItem.Quantity;
            //return Content(total.ToString());
            var orderTotal = new Order
            {
                Total = total
            };

            var order = db.Order.Add(orderTotal);
            orderItem.OrderId = order.Id;

            if (ModelState.IsValid)
            {

                db.OrderItem.Add(orderItem);
                db.SaveChanges();
                return RedirectToAction("Details");
            }

            ViewBag.MenuId = new SelectList(db.Menu, "Id", "Name", orderItem.MenuId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Id", orderItem.OrderId);
            return View(orderItem);
        }

        public ActionResult AddToOrder(Menu menu)
        {

            var addToOrder = new MenuItems
            {
                MenuId = menu.Id
            };
            db.MenuItems.Add(addToOrder);
            db.SaveChanges();

            //var orderItem = new OrderItem
            //{
            //    OrderId = 8,
            //    MenuId = menu.Id,
            //    Quantity = 1
            //    //DateCreated = DateTime.Now
            //};
            //db.OrderItem.Add(orderItem);
            //db.SaveChanges();


            //var orderList = db.OrderItem.ToList();
            //db.OrderItem.RemoveRange(orderList);


            //return Content(menu.Price.ToString());


            //db.OrderItem.Add(orderItem);

            //db.SaveChanges();


            return RedirectToAction("Index");
        }

        // GET: OrderItems/Create
        //public ActionResult Create()
        //{
        //    ViewBag.MenuId = new SelectList(db.Menu, "Id", "Name");
        //    ViewBag.OrderId = new SelectList(db.Order, "Id", "Id");
        //    return View();
        //}

        // POST: OrderItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Id,Quantity,OrderId,MenuId")] OrderItem orderItem)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.OrderItem.Add(orderItem);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    ViewBag.MenuId = new SelectList(db.Menu, "Id", "Name", orderItem.MenuId);
        //    ViewBag.OrderId = new SelectList(db.Order, "Id", "Id", orderItem.OrderId);
        //    return View(orderItem);
        //}

        // GET: OrderItems/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItem.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.MenuId = new SelectList(db.Menu, "Id", "Name", orderItem.MenuId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Id", orderItem.OrderId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Quantity,OrderId,MenuId")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.MenuId = new SelectList(db.Menu, "Id", "Name", orderItem.MenuId);
            ViewBag.OrderId = new SelectList(db.Order, "Id", "Id", orderItem.OrderId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItem.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderItem = db.OrderItem.Find(id);
            db.OrderItem.Remove(orderItem);
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
    }
}
