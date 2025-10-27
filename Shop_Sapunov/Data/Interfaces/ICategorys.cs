using Shop_Sapunov.Data.Models;

namespace Shop_Sapunov.Data.Interfaces
{
    public interface ICategorys
    {
        public IEnumerable<Categorys> AllCategorys { get; }
    }
}
