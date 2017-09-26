using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RABC.Models;
using RABC.Filters;

namespace RABC.Controllers
{
     [CustomAuthorizationArrtibute(Type = AuthorizationType.None)]
    public class LoginController : Controller
    {
        Rcba db = new Rcba();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User loginUser)
        {
            if (!ModelState.IsValid)
            {
                //模型绑定验证失败
                return Json(new { code = 400 });
            }
            //查找用户(贪婪加载)
            var user = db.Users.Include("Roles").FirstOrDefault(u => u.UserName == loginUser.UserName && u.PassWord == loginUser.PassWord);
            //如果没找到就返回404
            if (user == null) return Json(new { code = 404 });
            //存入session，作为身份验证的标识
            Session["user"] = user;
            //获取所有的角色
            var roles = user.Roles.ToList();
            //存入Session，以便于后期的使用，不在查寻数据库
            Session["roles"] = roles;
             //获取当前用户的所有角色所有模版
             var roleModule=user.Roles.ToDictionary(r=>r.Id,r => r.Modules );
            //存入Session，以便于后期的使用，不在查寻数据库
            Session["roleModules"] = roleModule;

            //设定登录时的默认角色
            Session["role"] = roles[0];


            return Json(new { code = 200 });
            #region 没事找着错
            //    if (!ModelState.IsValid)
            //    {
            //        //模型绑定数据失败
            //        return Json(new { code = 400 });
            //    }
            //    //查找数据
            //    var users = rc.Users.FirstOrDefault(m => m.UserName == user.UserName && m.PassWord == user.PassWord);
            //    if (users == null) return Json(new { code = 404 });
            //    Session["username"] = users;
            //    return Json(new { code=200});
            //}
            #endregion

        }
    }
}