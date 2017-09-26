using RABC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABC.Filters
{
    /// <summary>
    /// 授权过滤器认证类型属性
    /// None，无限制，不认证，比如登录或者是注册
    /// Identity，仅身份认证，比如首页，导航，头部
    /// Authorize，授权认证
    /// </summary>
    public enum AuthorizationType { None,Identity,Authorize}
    public class CustomAuthorizationArrtibute : FilterAttribute, IAuthorizationFilter
    {
        /// <summary>
        /// 授权过滤器认证类型属性，默认是授权认证
        /// </summary>
        public AuthorizationType Type = AuthorizationType.Authorize;
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            //1.无限制
            if (Type == AuthorizationType.None) return;
            //2.身份认证
            if (filterContext.HttpContext.Session["user"] == null)
            {
                RedirectToLogin(filterContext);
                return;
            }
            if (Type == AuthorizationType.Identity) return;
            //3.授权认证
            //获取当前控制器的名称
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //获取当前角色的所有模块，并与控制器进行对比
            var role = filterContext.HttpContext.Session["role"] as Role;
          


            var module = role.Modules.FirstOrDefault(m => m.ControllerName == controller);
            if (module == null)
            {
                RedirectToLogin(filterContext);
            }
        }
        /// <summary>
        /// 重定向的登录页
        /// </summary>
        /// <param name="filterContent"></param>
        public void RedirectToLogin(AuthorizationContext filterContent)
        {
            //实例化一个UrlHelper对象
            var url = new UrlHelper(filterContent.RequestContext);
            //设置返回结果  重定向的登录页
            filterContent.Result = new RedirectResult(url.Action("Index","Login"));
        }
    }
}