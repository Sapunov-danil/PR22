using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Models;

namespace Shop_Sapunov.Data.Mocks
{
    public class MockItems : IItems
    {
        public ICategorys _category = new MockCategorys();

        public IEnumerable<Items> AllItems
        {
            get
            {
                return new List<Items>()
                {
                    new Items() {
                        Id = 0,
                        Name = "DEXP MB-70",
                        Description = "Микроволновая печь DEXP MB-70 представлена в лаконичном черном корпусе с навесной дверцей и кнопкой. Модель с внутренним объемом 20 л оснащена поворотным столом с диаметром 25.5 см. Эмалированные стенки облегчают очистку от остатков пищи и запахов еды.",
                        Img = "https://c.dns-shop.ru/thumb/st4/fit/500/500/8632723b1befd4d685eff221552f2dd0/ca67c28e665850249f587a8b32ed2b21fb23fd80af1571cd49469273c1b0d138.jpg.webp",
                        Price = 3499,
                        Category = _category.AllCategorys.Where(x => x.Id == 0).First()
                    },
                    new Items() {
                        Id = 1,
                        Name = "Redmond RMC-P350",
                        Description = "Мультиварка-скороварка Redmond RMC-P350 - настоящая помощника любой хозяйки, которая позволит вам освоить приготовление разнообразных блюд и побаловать ими всю вашу семью. Представленную модель отличает богатый функционал, который приятно удивит даже самого требовательного пользователя.",
                        Img = "https://c.dns-shop.ru/thumb/st1/fit/500/500/4ad548fb18ac703c848a02977c37c1c1/c9813207d7790db07894a7dc9908b996f73266eda457c1569dc9c03cb9cd8f0d.jpg.webp",
                        Price = 13899,
                        Category = _category.AllCategorys.Where(x => x.Id == 1).First()
                    },

                };
            }
        }
    }
}
