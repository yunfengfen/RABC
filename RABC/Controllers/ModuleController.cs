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
    public class ModuleController : Controller
    {
        Rcba bd = new Rcba();
        // GET: Module
        public ActionResult Index()
        {
            return View(bd.Modules);
        }
        public ActionResult Edit(int id)
        {
            var module = bd.Modules.FirstOrDefault(r=>r.Id==id);
            return View(module);
        }
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult Save(Module module)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            bd.Modules.AddOrUpdate(module);
            bd.SaveChanges();

            return Json(new { code=200});
        }
        public ActionResult Delete(int id)
        {
            Module module = new Module { Id = id };
            bd.Modules.Attach(module);
            bd.Modules.Remove(module);
            bd.SaveChanges();
            return Json(new { code = 200 });
        }

    }
}