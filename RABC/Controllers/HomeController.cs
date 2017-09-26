
using RABC.Filters;
using RABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABC.Controllers
{
    [CustomAuthorizationArrtibute(Type = AuthorizationType.Identity)]
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// 头部的分部视图Action
        /// </summary>
        /// <returns></returns>
        public ActionResult Header()
        {
            //获取登录时的用户名
            var user = Session["user"] as User;
           //获取此时的用户名的所有角色
            var role = Session["role"] as Role;

            var roles = Session["roles"] as List<Role>;
            //声明一个下拉列表框的集合
            var rolesSelect = new List<SelectListItem>();
            foreach (var item in roles)
            {
                rolesSelect.Add( new SelectListItem { Text = item.RoleName, Value = item.Id.ToString(), Selected = item.Id == role.Id });
            }


            ViewBag.RoleSelect = rolesSelect;
            return PartialView(user);
        }
       /// <summary>
       /// 导航栏的分部视图Action
       /// </summary>
       /// <returns></returns>
        public ActionResult Nav(int roleId=0)
        {
            //从session中拿到所有用户的角色模版
            var roleModules = Session["roleModules"] as Dictionary<int, ICollection<Module>>;
            //从session拿到所有用户的角色
            var roles = Session["roles"] as List<Role>;

            ICollection<Module> modules;
            if (roleModules.ContainsKey(roleId))
            {
                //如果参数的id存在
                modules = roleModules[roleId];
                //设定当前的角色为roleid参数指定的角色
                Session["role"] = roles.FirstOrDefault(r => r.Id == roleId);

            }
            else
            {
                //从session拿到登录时设定的角色
                var role = Session["role"] as Role;
                //从当前角色拿到所有模块
                modules = role.Modules;
            }
            //Modules作为强类型，传入nav的分布视图
            return PartialView(modules);

        }
        public ActionResult Logout()
        {
            //清除session，注销
            Session.Clear();
            return RedirectToAction("Index","Login");
        }
    }
}