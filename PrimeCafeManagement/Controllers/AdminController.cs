using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using PrimeCafeManagement.Models;

namespace PrimeCafeManagement.Controllers
{

    [AuthorizeAdmin]
    public class AdminController : Controller
    {
        private readonly PrimeCafeContext _context;
        public AdminController(PrimeCafeContext context)
        {
            _context = context;
        }
        //CRUD FOR USER STARTS HERE
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
            int maxSize = 1024 * 1024 * 2;
            if (file != null && file.Length > 0)
            {
                var fileExtension = file.FileName.Split('.').LastOrDefault();
                if (!allowedExtension.Contains(fileExtension))
                {
                    ViewBag.Roles = _context.Roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
                    ViewBag.Error = "Only JPG or PNG Images Allwoed";
                    return View(user);
                }
                else if (file.Length > maxSize)
                {
                    ViewBag.Roles = _context.Roles.Select(x => new SelectListItem { Value = x.Id.ToString(), Text = $"{x.Name}" }).ToList();
                    ViewBag.Error = "Allowed Image Size is 2MB";
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
            //define Access Token and created On
            user.AccessToken = DateTime.UtcNow.Ticks.ToString();
            user.CreatedOn = DateTime.UtcNow.AddHours(5);
            _context.Users.Update(user);
            _context.SaveChanges();
            return Redirect("/Admin/Users");
        
        }
        
        [HttpGet]
        public IActionResult DeleteUser(int id)
        {
            User user = _context.Users.Where(x => x.Id == id).FirstOrDefault();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return Redirect("/Admin/Users");

        }
        //CRUD FOR USER ENDS HERE

        //CRUD FOR ROLES STARTS HERE
        public IActionResult Roles()
        {
            List<Role> role = _context.Roles.ToList();
            return View(role);
        }

        [HttpGet]
        public IActionResult AddUpdateRole(int id = 0)
        {          
            if (id == 0)
            {
                return View();
            }
            else
            {
                Role role = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
                return View(role);
            }

        }
        [HttpPost]
        public IActionResult AddUpdateRole(Role role)
        {
            _context.Roles.Update(role);
            _context.SaveChanges();
            return Redirect("/Admin/Roles");

        }
        [HttpGet]
        public IActionResult DeleteRole(int id)
        {
            Role role = _context.Roles.Where(x => x.Id == id).FirstOrDefault();
            _context.Roles.Remove(role);
            _context.SaveChanges();
            return Redirect("/Admin/Roles");

        }
        //CRUD FOR ROLES ENDS HERE

    }
}
