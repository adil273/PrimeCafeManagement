using System;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeCafeManagement.Models;
using System.Linq;
using System.Threading;
using Microsoft.Extensions.Options;
using Azure.Core;

// CUSTOMER CONTROLLER CONSIST OF ORDERS CRUD.
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

        //ORDER CRUD STARTS HERE//////////
        
        public IActionResult Orders()
        {
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();
            List<Order> order = _context.Orders.Where(x => x.UserId == user.Id).ToList();
            return View(order);
        }

        [HttpGet]public IActionResult AddUpdateOrder(int id = 0)
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
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();

            order.UserId = user.Id;
            order.OrderDate = DateTime.UtcNow.AddHours(5);
            order.OrderNumber = DateTime.UtcNow.AddHours(5).ToString("hhmmss");
            order.Status = "Pending";

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
        //ORDER CRUD ENDS HERE//////////
    }
}