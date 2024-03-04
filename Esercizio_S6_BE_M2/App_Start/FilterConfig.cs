using System.Web;
using System.Web.Mvc;

namespace Esercizio_S6_BE_M2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
