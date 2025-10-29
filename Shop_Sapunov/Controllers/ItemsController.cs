using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Models;
using Shop_Sapunov.Data.ViewModell;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Shop_Sapunov.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private IItems IAllItems;
        private ICategorys IAllCategorys;
        VMItems VMItems = new VMItems();

        public ItemsController(IItems IAllItems, ICategorys IAllCategorys, IHostingEnvironment hostingEnvironment) 
        {
            this.IAllItems = IAllItems;
            this.IAllCategorys = IAllCategorys;
            this.hostingEnvironment = hostingEnvironment;
        }

        public ViewResult List(int id = 0) 
        {
            ViewBag.Title = "Страница с предметами";
            VMItems.Items = IAllItems.AllItems;
            VMItems.Categorys = IAllCategorys.AllCategorys;
            VMItems.SelectCategory = id;
            return View(VMItems);
        }

        [HttpGet]
        public ViewResult Add() 
        {
            IEnumerable<Categorys> Categorys = IAllCategorys.AllCategorys;

            return View(Categorys);
        }

        [HttpPost]
        public RedirectResult Add(string name, string description, IFormFile files, float price, int idCategory) 
        {
            if (files != null)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                var filePath = Path.Combine(uploads, files.FileName);
                files.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            Items newItems = new Items();
            newItems.Name = name;
            newItems.Description = description;
            newItems.Img = "/img/" + Path.GetFileName(files.FileName);
            newItems.Price = Convert.ToInt32(price);
            newItems.Category = new Categorys() { Id = idCategory };

            int id = IAllItems.Add(newItems);
            return Redirect("/Items/Update?id=" + id);
        }
        // Метод для отображения страницы редактирования существующего предмета
        [HttpGet]
        public IActionResult Update(int id)
        {
            // Находим предмет по id из хранилища
            var item = IAllItems.AllItems.FirstOrDefault(x => x.Id == id);
            // Передаём список категорий в ViewBag для выбора в форме редактирования
            ViewBag.Categorys = IAllCategorys.AllCategorys;
            // Возвращаем найденный предмет в представление для редактирования
            return View(item);
        }

        // Метод обработки формы обновления предмета
        [HttpPost]
        public IActionResult Update(int id, string name, string description, IFormFile file, float price, int idCategory)
        {
            // Находим существующий предмет по id
            var existingItem = IAllItems.AllItems.FirstOrDefault(x => x.Id == id);
            if (existingItem == null) return NotFound();

            // Если загружен новый файл изображения и он не пустой
            if (file != null && file.Length > 0)
            {
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "img");
                Directory.CreateDirectory(uploads);
                var fileName = Path.GetFileName(file.FileName);
                var filePath = Path.Combine(uploads, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                existingItem.Img = "/img/" + fileName;
            }

            // Обновляем остальные свойства предмета значениями из формы
            existingItem.Name = name;
            existingItem.Description = description;
            existingItem.Price = Convert.ToInt32(price);
            existingItem.Category = new Categorys() { Id = idCategory };

            // Сохраняем изменения
            IAllItems.Update(existingItem);

            // После обновления перенаправляем на страницу списка предметов
            return RedirectToAction("List");
        }

        // Метод для удаления предмета по id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            // Вызываем удаление
            IAllItems.Delete(id);
            // Перенаправляем обратно к списку предметов после удаления
            return RedirectToAction("List");
        }

        public ActionResult Basket(int idItem = -1)
        {
            if (idItem != -1)
            {
                // добавляем в корзину предмет
                UserBasket.BasketItem.Add(new ItemsBasket(1, IAllItems.AllItems.Where(x => x.Id == idItem).First()));
            }
            // Возвращаем списком всю корзину
            return Json(UserBasket.BasketItem);
        }

        public ActionResult BasketCount(int idItem = -1, int count = -1)
        {
            if (idItem != -1)
            {
                if (count == 0)
                    UserBasket.BasketItem.Remove(UserBasket.BasketItem.Find(x => x.Id == idItem));
                else
                    UserBasket.BasketItem.Find(x => x.Id == idItem).Count = count;
            }
            return Json(UserBasket.BasketItem);
        }
    }
}
