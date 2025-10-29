using Microsoft.AspNetCore.Mvc;

namespace Shop_Sapunov.Controllers
{
    public class HomeController : Controller
    {
        public RedirectResult Index() 
        {
            return Redirect("/Items/List");
        }
    }
}
