using System.Web;
using System.Web.Mvc;
using ThesisSelectSystem.Filter;
namespace ThesisSelectSystem.App_Start
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyExceptionFilterAttribute());

        }
    }
}

