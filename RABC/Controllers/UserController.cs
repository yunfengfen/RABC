using RABC.Filters;
using RABC.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABC.Controllers
{
    [CustomAuthorizationArrtibute(Type = AuthorizationType.Authorize)]
    public class UserController : Controller
    {
        Rcba bd = new Rcba();
        // GET: User
        public ActionResult Index()
        {
            return View(bd.Users);
        }
        public ActionResult Edit(int id)
        {
            var module = bd.Users.FirstOrDefault(r => r.Id == id);
            return View(module);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(User role)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            bd.Users.AddOrUpdate(role);
            bd.SaveChanges();

            return Json(new { code = 200 });
        }
        public ActionResult Delete(int id)
        {
          
            User user = new User { Id = id };
            bd.Users.Attach(user);
            bd.Users.Remove(user);
            bd.SaveChanges();
            return Json(new { code = 200 });
        }
    }
}