using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PrimeCafeManagement.Models;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Azure.Core;

//ACCOUNT CONTROLLER CONSIST LANDING, LOGIN-LOGOUT, USER REGISTRATION WITH CRUD, PRIVACY, TERMS AND CONDITION HELP
namespace PrimeCafeManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly PrimeCafeContext _context;

        public AccountController(PrimeCafeContext context)
        {
            _context = context;
        }
        public IActionResult Landing()
        {
            return View();
        }

        //LOGIN-LOGOUT STARTS /////////////////////////////////
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(User user, Role role)
        {
            User dbUser = _context.Users.Where(x => x.Email.ToLower() == user.Email.ToLower() && x.Password == user.Password).Include(u => u.Role).FirstOrDefault();
            if (dbUser == null)
            {
                ViewBag.Error = "Email or Password is invalid";
                return View(user);
            }
            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTime.Now.AddDays(3);
            Response.Cookies.Append("user-access-token", dbUser.AccessToken, cookieOptions);


            if (dbUser.Role != null)
            {
                if (dbUser.Role.Name == "Admin")
                {
                    return Redirect("/Admin/Users");
                }

                else if (dbUser.Role.Name == "Manager")
                {
                    return Redirect("/Manager/Dashboards");
                }

                else if (dbUser.Role.Name == "Customer")
                {
                    return Redirect("/Customer/CurrentOrder");
                }

            }
           
            return Redirect("/Account/Register");
        }

        public IActionResult Logout()
        {
            var accessToken = Request.Cookies["user-access-token"];
            User user = _context.Users.Where(x => x.AccessToken == accessToken).FirstOrDefault();
            Response.Cookies.Delete("user-access-token");
            _context.SaveChanges();
            return Redirect("/Account/Login");
        }
        //LOGIN-LOGOUT ENDS ///////////////

        //REGISTER CRUD STARTS//////////////////
        public IActionResult Users()
        {
            List<User> user = _context.Users.Include(x => x.Role).ToList();
            return View(user);
        }

        [HttpGet]
        public IActionResult AddUpdateUser(int id = 0)
        {
            ViewBag.Roles = _context.Roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
            if (id == 0)
            {
                return View();
            }
            else
            {
                User user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
                return View(user);
            }
        }

        [HttpPost]
        public IActionResult AddUpdateUser(User user, IFormFile file)
        {
            List<string> allowedExtension = new List<string> { "jpg", "png" };
            int maxSize = 1024 * 1024 * 4;
            if (file != null && file.Length > 0)
            {
                var fileExtension = file.FileName.Split('.').LastOrDefault();
                if (!allowedExtension.Contains(fileExtension))
                {
                    ViewBag.Roles = _context.Roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
                    ViewBag.Error = "Only JPG or PNG images allwoerd";
                    return View(user);
                }
                else if (file.Length > maxSize)
                {
                    ViewBag.Roles = _context.Roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
                    ViewBag.Error = "Allowed Size is 4MB";
                    return View(user);
                }
                else
                {
                    user.Image = $"{DateTime.UtcNow.Ticks.ToString()}.jpg";
                    string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot/image/{user.Image}");
                    using (FileStream stream = new FileStream(path, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                }
            }
            user.AccessToken = DateTime.UtcNow.Ticks.ToString();
            user.CreatedOn = DateTime.UtcNow.AddHours(5);
           

            user.RoleId = _context.Roles.Where(x => x.Name == "Customer").Select(x => x.Id).FirstOrDefault();
            _context.Users.Update(user);
            _context.SaveChanges();
            return Redirect("/Account/Login");

        }

        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            User user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Redirect("/Account/Users");

        }

        //REGISTER CRUD ENDS //////////////////

        //OTHER PAGES STARTS HERE //////////////////
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Terms()
        {
            return View();
        }
        public IActionResult AboutUs()
        {
            return View();
        }
        public IActionResult Help()
        {
            return View();
        }
        //OTHER PAGES ENDS HERE //////////////////
    }
}
