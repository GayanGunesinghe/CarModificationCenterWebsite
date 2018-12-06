/*
 * FilterConfig.cs specifies access to a controller or action method
 * that is restricted to users with authorization.  
 */

using System.Web.Mvc;

namespace CarModificationCenter.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());    //handles exceptions thrown by action method.
            filters.Add(new AuthorizeAttribute());  //specifies access to controller/action method.
        }
    }
}