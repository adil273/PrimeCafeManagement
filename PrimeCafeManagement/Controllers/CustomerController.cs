using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeCafeManagement.Models;

namespace PrimeCafeManagement.Controllers
{
    [AuthorizeCustomer]
    public class CustomerController : Controller
    {
        private readonly PrimeCafeContext _context;
        public CustomerController(PrimeCafeContext context)
        {
            _context = context;
        }

        public IActionResult Orders()
        {


            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();
            DateTime today = DateTime.UtcNow.Date;
            List<Order> order = _context.Orders.Where(x => x.UserId == user.Id && x.OrderDate.Date == today).Include(x => x.User).ToList();
            return View(order);
        }
        [HttpGet]
        public IActionResult AddUpdateOrder(int id = 0)
        {

            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();

            ViewBag.Users = _context.Users.Where(x => x.AccessToken == accessToken).Select(
                x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();



            if (id == 0)
            {
                return View();
            }
            else
            {
                Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
                return View(order);
            }

        }

        [HttpPost]
        public IActionResult AddUpdateOrder(Order order)
        {

            order.OrderDate = DateTime.UtcNow.AddHours(5);
            _context.Orders.Update(order);
            _context.SaveChanges();
            return Redirect("/Customer/Orders");
        }


        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Redirect("/Customer/Orders");
        }
        //RESET ORDER
        [HttpGet]
        public IActionResult ResetOrders(int id)
        {
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();
            var order = _context.Orders.Where(x => x.UserId == user.Id);
            _context.Orders.RemoveRange(order);
            _context.SaveChanges();
            return Redirect("/Customer/Orders");
        }

        //ORDER STTUS STARTS
        [HttpPost]
        public IActionResult OrderStatuses(Order order, int TotalPrice)
        {
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();

            ViewBag.user = user.Name;
            ViewBag.OrderDate = DateTime.UtcNow.AddHours(5).ToString("dd-MMM-yy");

            var orderNumber = "PCM-" + user.Name + "-" + DateTime.UtcNow.AddHours(5).ToString("hhmm");

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddHours(1);
            Response.Cookies.Append("order-number", orderNumber, cookieOptions);
            ViewBag.OrderNumber = orderNumber;
            ViewBag.TotalPrice = TotalPrice;
            return View();
        }
    }
}
