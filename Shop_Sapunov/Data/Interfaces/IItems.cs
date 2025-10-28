using Shop_Sapunov.Data.Models;

namespace Shop_Sapunov.Data.Interfaces
{
    public interface IItems
    {
        public IEnumerable<Items> AllItems { get; }
        public int Add(Items Item);
    }
}
