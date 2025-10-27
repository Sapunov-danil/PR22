using Microsoft.AspNetCore.Mvc;
using Shop_Sapunov.Data.Interfaces;

namespace Shop_Sapunov.Controllers
{
    public class ItemsController : Controller
    {
        private IItems IAllItems;
        private ICategorys IAllCategorys;

        public ItemsController(IItems IAllItems, ICategorys IAllCategorys) 
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
        }

        public ViewResult List() 
        {
            ViewBag.Title = "Страница с предметами";

            var cars = IAllItems.AllItems;
            return View(cars);
        }
    }
}
