using RABC.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RABC.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilter(GlobalFilterCollection filters)
        {
            //添加全局过滤器
            filters.Add( new CustomAuthorizationArrtibute());
        }

        internal static void RegisterGlobalFilter()
        {
            throw new NotImplementedException();
        }
    }
}