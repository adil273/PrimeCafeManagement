using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeCafeManagement.Models;

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

       
        [HttpGet]
        public IActionResult AddUpdateOrder(int id = 0)
        {
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.FirstOrDefault(x => x.AccessToken == accessToken);

            ViewBag.Users = _context.Users.Where(x => x.AccessToken == accessToken)
                .Select(x => new SelectListItem { Value = x.Id.ToString(), Text = x.Name }).ToList();

            List<Order> meals = new List<Order>
            {
                new Order { Title = "Today Meal", Price = 600, Image = "meal.jpg" },
                new Order { Title = "Rice", Price = 200, Image = "rice.jpg" },
                new Order { Title = "Chicken", Price = 350, Image = "chicken.jpg" },
                new Order { Title = "Cup Cake", Price = 280, Image = "cupcake.jpg" },
                new Order { Title = "Coffee", Price = 150, Image = "coffee.jpg" },
                new Order { Title = "Tea", Price = 120, Image = "tea.jpg" },
                new Order { Title = "Chips", Price = 80, Image = "chips.jpg" },
                new Order { Title = "Water", Price = 70, Image = "water.jpg" }
            };

            if (id == 0)
            {
                return View(meals);
            }
            else
            {
                Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
                
                return View(new List<Order> { order });
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
            return Redirect("/Customer/CurrentOrder");
        }

        [HttpGet]
        public IActionResult DeleteOrder(int id)
        {
            Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
            _context.Orders.Remove(order);
            _context.SaveChanges();
            return Redirect("/Customer/CurrentOrder");
        }

        public IActionResult CurrentOrder()
        {
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();

            List<Order> order = _context.Orders.Where(x => x.UserId == user.Id && x.Status == "Pending").Include(x => x.User).ToList();

            return View(order);
        }
        //ORDER CRUD ENDS HERE//////////


    }
}