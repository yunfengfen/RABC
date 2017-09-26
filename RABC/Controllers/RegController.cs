using RABC.Filters;
using RABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABC.Controllers
{
    [CustomAuthorizationArrtibute(Type = AuthorizationType.None)]
    public class RegController : Controller
    {
        Rcba bd = new Rcba();
        // GET: Reg
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Reg(User reguser)
        {
            //模型验证
            if (!ModelState.IsValid)
            {
                return Json(new { code = 400 });
            }
            //设置默认的角色
            var role = bd.Roles.FirstOrDefault(r=>r.Id==3);
            reguser.Roles.Add(role);
            bd.Users.Add(reguser);
            bd.SaveChanges();
            return Json(new { code=200});
        }
    }
}