using RABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using RABC.ViewModel;

namespace RABC.Controllers
{
    public class RoleModuleController : Controller
    {
        Rcba bd = new Rcba();
        // GET: RoleModule
        public ActionResult Index()
        {
            var result = bd.Roles.Include(r=>r.Modules);
            return View(result);
        }
        public ActionResult Create()
        {
            //所有角色的下拉框
            ViewBag.RolesOptions = from r in bd.Roles
                                   select new SelectListItem { Text = r.RoleName, Value = r.Id.ToString() };
            //所有角色的下拉框
            ViewBag.ModuleOptions = from r in bd.Modules
                                   select new SelectListItem { Text = r.ModuleName, Value = r.Id.ToString() };
            return View();
        }
        public ActionResult Edit(RoleModuleViewModel roleModule)
        {
            //所有角色的下拉框
            ViewBag.ModuleOptions = from r in bd.Modules
                                    select new SelectListItem { Text = r.ModuleName, Value = r.Id.ToString() };
            roleModule.RoleName = bd.Roles.FirstOrDefault(r=>r.Id==roleModule.RoleId).RoleName;
            roleModule.ModuleName = bd.Modules.FirstOrDefault(r => r.Id == roleModule.ModuleId).ModuleName;
            return View();
        }
        public ActionResult Delete(RoleModuleViewModel roleModule)
        {
            var role = bd.Roles.FirstOrDefault(r => r.Id == roleModule.RoleId);
            var module = new Module { Id = roleModule.ModuleId };
            bd.Modules.Attach(module);
            role.Modules.Remove(module);
            if (bd.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
           
        }
        public ActionResult Insert(RoleModuleViewModel  roleModule)
        {
            if (!ModelState.IsValid)
            {
                return Json(new { code=400});
            }
            var role = bd.Roles.FirstOrDefault(r=>r.Id==roleModule.RoleId);
         //   var role = new Role { Id = roleModule.RoleId };
            var module = new Module {Id=roleModule.ModuleId };
            //伪装成从数据库读取数据一样
          //  bd.Roles.Attach(role);
            bd.Modules.Attach(module);
            //这一步是关联的关键，把module添加到role的module中
            role.Modules.Add(module);
            ////需要把这个角色天津爱就爱到实体集合中
            //bd.Roles.Add(role);
            if (bd.SaveChanges()==0)
            {
                return Json(new { code=400});
            }
            return Json(new { code=200});
        }
        public ActionResult Update(RoleModuleViewModel roleModule)
        {
            var role = bd.Roles.FirstOrDefault(r => r.Id == roleModule.RoleId);
            var module = new Module { Id = roleModule.ModuleId };
            var updateModule = new Module { Id = roleModule.ModuleId };
            bd.Modules.Attach(module);
            bd.Modules.Attach(updateModule);

            //把要修改的角色模块删除
            role.Modules.Remove(module);
            //这一步是关联的关键，把module添加到role的module中
            role.Modules.Add(module);
            role.Modules.Add(module);
            if (bd.SaveChanges() == 0)
            {
                return Json(new { code = 400 });
            }
            return Json(new { code = 200 });
        }

    }
}