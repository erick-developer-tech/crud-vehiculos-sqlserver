using System.Web;
using System.Web.Mvc;

namespace crud_vehiculos_sqlserver
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
