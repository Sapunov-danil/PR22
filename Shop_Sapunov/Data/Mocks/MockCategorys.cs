using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Models;

namespace Shop_Sapunov.Data.Mocks
{
    public class MockCategorys : ICategorys
    {
        public IEnumerable<Categorys> AllCategorys
        {
            get
            {
                return new List<Categorys>
                {
                    new Categorys() {
                        Id = 0,
                        Name = "Микроволновка",
                        Description = "Микроволновки"
                    },
                    new Categorys() {
                        Id = 1,
                        Name = "Мультиварка",
                        Description = "Мультиварки"
                    },
                };
            }
        }
    }
}
