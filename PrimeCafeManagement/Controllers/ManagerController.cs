using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PrimeCafeManagement.Models;
using System.Linq;
using System.Threading;
using static System.Net.Mime.MediaTypeNames;

//MANAGER CONTROLLER CONSIST OF MENU CRUD, MENU TITLE CRUD, MENU PRICE AND DASHBAORD
namespace PrimeCafeManagement.Controllers
{
    [AuthorizeManager]
    public class ManagerController : Controller
    {
        private readonly PrimeCafeContext _context;
        public ManagerController(PrimeCafeContext context)
        {
            _context = context;
        }

        public IActionResult Dashboards()
        {
            List<Order> order = _context.Orders.Where(x => x.Status == "Pending").Include(x => x.User).ToList();

            return View(order);
        }
        [HttpGet]
        public IActionResult CompleteOrder(int id)
        {
            Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
            order.Status = "Confirm"; 
            _context.SaveChanges();
            return Redirect("/Manager/Dashboards");
        }
        [HttpGet]
        public IActionResult CancelOrder(int id)
        {
            Order order = _context.Orders.Where(x => x.Id == id).FirstOrDefault();
            order.Status = "Cancel";
            _context.SaveChanges();
            return Redirect("/Manager/Dashboards"); 
        }
        public IActionResult Menus()
        {
            List<Menu> menu = _context.Menus.Include(x => x.MenuTitle).Include(x => x.MenuPrice).ToList();
            return View(menu);
        }
        [HttpGet]
        public IActionResult AddUpdateMenu(int id = 0)
        {
            ViewBag.MenuTitles = _context.MenuTitles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
            ViewBag.MenuPrices = _context.MenuPrices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();

            if (id == 0)
            {
                return View();
            }
            else
            {
                Menu menu = _context.Menus.Where(x => x.Id == id).FirstOrDefault();
                return View(menu);
            }
        }
        [HttpPost]
        public IActionResult AddUpdateMenu(Menu menu, IFormFile file)
        {


            List<string> allowedExtension = new List<string> { "jpg", "png" };
            int maxSize = 1024 * 1024 * 4;
            if (file != null && file.Length > 0)
            {
                var fileExtension = file.FileName.Split('.').LastOrDefault();
                if (!allowedExtension.Contains(fileExtension))
                {
                    ViewBag.MenuTitles = _context.MenuTitles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
                    ViewBag.MenuPrices = _context.MenuPrices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();

                    ViewBag.Error = "Only JPG or PNG images allwoerd";
                    return View(menu);
                }
                else if (file.Length > maxSize)
                {
                    ViewBag.MenuTitles = _context.MenuTitles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
                    ViewBag.MenuPrices = _context.MenuPrices.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();

                    ViewBag.Error = "Allowed Size is 1MB";
                    return View(menu);
                }
                else
                {
                    menu.Image = $"{DateTime.UtcNow.Ticks.ToString()}.jpg";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/image/{menu.Image}");
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }

            menu.AccessToken = DateTime.UtcNow.Ticks.ToString();
            menu.CreatedOn = DateTime.UtcNow.AddHours(5);


            _context.Menus.Update(menu);
            _context.SaveChanges();
            return Redirect("/Manager/Menus");
        }
        [HttpGet]
        public IActionResult DeleteMenu(int id)
        {
            Menu menu = _context.Menus.Where(x => x.Id == id).FirstOrDefault();
            _context.Menus.Remove(menu);
            _context.SaveChanges();
            return Redirect("/Manager/Menus");
        }


        // ADD MENU TITLE
        public IActionResult MenuTitles()
        {
            List<MenuTitle> menuTitle = _context.MenuTitles.ToList();
            return View(menuTitle);
        }

        [HttpGet]
        public IActionResult AddUpdateMenuTitle(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                MenuTitle menuTitle = _context.MenuTitles.Where(x => x.Id == id).FirstOrDefault();
                return View(menuTitle);
            }

        }
        [HttpPost]
        public IActionResult AddUpdateMenuTitle(MenuTitle menuTitle)
        {
            _context.MenuTitles.Update(menuTitle);
            _context.SaveChanges();
            return Redirect("/Manager/MenuTitles");

        }
        [HttpGet]
        public IActionResult DeleteMenuTitle(int id)
        {
            MenuTitle menuTitle = _context.MenuTitles.Where(x => x.Id == id).FirstOrDefault();
            _context.MenuTitles.Remove(menuTitle);
            _context.SaveChanges();
            return Redirect("/Manager/MenuTitles");

        }
        // MENU PRICE
        public IActionResult MenuPrices()
        {
            List<MenuPrice> menuPrice = _context.MenuPrices.ToList();
            return View(menuPrice);
        }

        [HttpGet]
        public IActionResult AddUpdateMenuPrice(int id = 0)
        {
            if (id == 0)
            {
                return View();
            }
            else
            {
                MenuPrice menuPrice = _context.MenuPrices.Where(x => x.Id == id).FirstOrDefault();
                return View(menuPrice);
            }

        }
        [HttpPost]
        public IActionResult AddUpdateMenuPrice(MenuPrice menuPrice)
        {
            _context.MenuPrices.Update(menuPrice);
            _context.SaveChanges();
            return Redirect("/Manager/MenuPrices");

        }
        [HttpGet]
        public IActionResult DeleteMenuPrice(int id)
        {
            MenuPrice menuPrice = _context.MenuPrices.Where(x => x.Id == id).FirstOrDefault();
            _context.MenuPrices.Remove(menuPrice);
            _context.SaveChanges();
            return Redirect("/Manager/MenuPrices");

        }

    }
}
