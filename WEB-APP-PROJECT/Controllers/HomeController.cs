
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WEB_APP_PROJECT.Data;
using WEB_APP_PROJECT.Models;

namespace WEB_APP_PROJECT.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<FoodCourt>? allFood = _db.FoodCourts;
            IEnumerable<UserState>? allNinja = _db.UserStates;
            IEnumerable<Order>? allOrder = _db.Orders;
            string userName = User.Identity.GetUserName();
            var obj = _db.UserStates.FirstOrDefault(n => n.UserName == userName);
            var obj2 = _db.Orders.FirstOrDefault(o => o.riderName == userName);
            var obj3 = _db.Orders.FirstOrDefault(o => o.userName == userName);
            if (obj == null)
            {
                var naruto = new UserState
                {
                    UserName = userName,
                    State = "Ninja"
                };
                _db.UserStates.Add(naruto);
                _db.SaveChanges();
            }
            else if (obj.State == "ผู้ค้า")
            {
                if (obj2 == null)
                {
                    obj.State = "Ninja";
                    _db.SaveChanges();
                    return View(allFood);
                }
                else
                {
                    return RedirectToAction("ChoosePage", new { FoodShopName = obj2.FoodShopName });
                }
            }
            else if(obj.State == "ผู้เสพ")
            {
                if (obj3 == null)
                {
                    obj.State = "Ninja";
                    _db.SaveChanges();
                    return View(allFood);
                }
            }
            return View(allFood);
        }
        public IActionResult Ask(string FoodShopName)
        {
            IEnumerable<UserState>? allNinja = _db.UserStates;
            var ninja = allNinja.SingleOrDefault(c => c.UserName == @User.Identity.Name);
            ViewData["UserState"] = ninja.State;
            ViewData["FoodShopName"] = FoodShopName;

            return View(allNinja);
        }

        public IActionResult UpdateState(string userName, string newState, string FoodShopName)
        {
            IEnumerable<FoodCourt>? allFood = _db.FoodCourts;
            IEnumerable<Order>? allOrder = _db.Orders;
            var obj = _db.UserStates.FirstOrDefault(n => n.UserName == userName);
            var objFNS = _db.FoodCourts.FirstOrDefault(o => o.FoodShopName == FoodShopName);
            if (obj == null)
            {
                return NotFound();
            }
            obj.State = newState;
            _db.SaveChanges();
            if (newState == "ผู้ค้า")
            {
                objFNS.RiderCount += 1;
                _db.SaveChanges();
                return RedirectToAction("ChoosePage", new { FoodShopName = FoodShopName });
            }
            return RedirectToAction("BuiltOrder", new { FoodShopName = FoodShopName });
        }

        public IActionResult UpdateRider(string count, string FoodShopName)
        {
            //IEnumerable<FoodCourt>? allFood = _db.FoodCourts;
            var obj = _db.FoodCourts.FirstOrDefault(f => f.FoodShopName == FoodShopName);

            if (obj == null)
            {
                return RedirectToAction("Privacy");
            }

            if (count == "minus")
            {
                obj.RiderCount -= 1;
                _db.SaveChanges();
                return RedirectToAction("Order", new { FoodShopName = FoodShopName });
            }
            else
            {
                obj.RiderCount += 1;
                _db.SaveChanges();
                return RedirectToAction("ChoosePage", new { FoodShopName = FoodShopName });
            }

        }


        public IActionResult BuiltOrder(string FoodShopName)
        {
            var obj = _db.FoodCourts.SingleOrDefault(f => f.FoodShopName == FoodShopName);
            if (obj != null)
            {
                ViewData["FoodShopName"] = FoodShopName;
                ViewData["ShopSrc"] = obj.FoodShopImg;
                return View();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult BuiltOrder(Order obj)
        {
            obj.userName = User.Identity.Name;
            Console.WriteLine(obj);
            _db.Orders.Add(obj);
            _db.SaveChanges();
            Console.WriteLine("Order has been created!");
            return RedirectToAction("Index");
        }

        public IActionResult Order(string FoodShopName)
        {
            var userS = _db.UserStates.SingleOrDefault(f => f.UserName == User.Identity.Name);
            ViewData["UserState"] = userS.State;
            ViewData["FoodShopName"] = FoodShopName;
            var orderList = _db.Orders.Where(c => (c.riderName == @User.Identity.Name || (c.userName == @User.Identity.Name ))).ToList();
            return View(orderList);
        }

        public IActionResult ConfirmOrder(int Orderid)
        {
            var order = _db.Orders.Find(Orderid);
            if (order == null)
            {
                return NotFound(); // หา order ไม่เจอ ส่ง response 404 Not Found
            }
            else
            {
                string user = order.userName;
                string rider = order.riderName;
                string FoodShop = order.FoodShopName;
                _db.Orders.Remove(order);
                _db.SaveChanges();

                // Check if the user object exists
                var objUser = _db.Orders.FirstOrDefault(u => u.userName == user);
                if (objUser == null)
                {
                    var objUserST = _db.UserStates.FirstOrDefault(e => e.UserName == user);
                    if (objUserST != null)
                    {
                        objUserST.State = "Ninja";
                        _db.SaveChanges();
                    }
                }

                // Check if the rider object exists
                var objRider = _db.Orders.FirstOrDefault(r => r.riderName == rider);
                if (objRider == null)
                {
                    var objRiderST = _db.UserStates.FirstOrDefault(h => h.UserName == rider);
                    if (objRiderST != null)
                    {
                        objRiderST.State = "Ninja";
                        _db.SaveChanges();
                    }
                }

                return RedirectToAction("Order", new { FoodShopName = FoodShop });
            }
        }


        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult StartPage()
        {
            return View();
        }

        public IActionResult LoginPage()
        {
            return Redirect("~/Identity/Account/Login");
        }

        public IActionResult ChoosePage(String FoodShopName)
        {
            var orderList = _db.Orders.Where(c => c.FoodShopName == FoodShopName && c.status == "wait").ToList();
            ViewData["FoodShopName"] = FoodShopName;

            return View(orderList);
        }


        public IActionResult AcceptOrder(int Orderid, string FoodShopName)
        {
            var order = _db.Orders.Find(Orderid);
            if (order != null)
            {
                order.status = "in process";
                order.riderName = @User.Identity.Name;
                _db.Orders.Update(order);
                _db.SaveChanges();
                return RedirectToAction("Order", new { FoodShopName = FoodShopName });
            }
            else
            {
                return RedirectToAction("Privacy");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}