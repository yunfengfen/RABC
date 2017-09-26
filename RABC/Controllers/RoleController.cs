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
    public class RoleController : Controller
    {
        Rcba bd = new Rcba();
        // GET: Role
        public ActionResult Index()
        {
            return View(bd.Roles);
        }
        public ActionResult Edit(int id)
        {
            var module = bd.Roles.FirstOrDefault(r => r.Id == id);
            return View(module);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(Role role)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            bd.Roles.AddOrUpdate(role);
            bd.SaveChanges();

            return Json(new { code = 200 });
        }
        public ActionResult Delete(int id)
        {
            Role role = new Role { Id = id };
            Module module = new Module { Id = id };
            bd.Roles.Attach(role);
            bd.Roles.Remove(role);
            bd.SaveChanges();
            return Json(new { code = 200 });
        }
    }
}