using MySql.Data.MySqlClient;
using Shop_Sapunov.Data.Common;
using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Models;

namespace Shop_Sapunov.Data.DataBase
{
    public class DBCategory : ICategorys
    {
        public IEnumerable<Categorys> AllCategorys
        {
            get
            {
                List<Categorys> categories = new List<Categorys>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader CategorysData = Connection.MySqlQuery("SELECT * FROM Shop.Categorys ORDER BY `Name`;", MySqlConnection);
                while (CategorysData.Read())
                {
                    categories.Add(new Categorys()
                    {
                        Id = CategorysData.IsDBNull(0) ? -1 : CategorysData.GetInt32(0),
                        Name = CategorysData.IsDBNull(1) ? null : CategorysData.GetString(1),
                        Description = CategorysData.IsDBNull(2) ? null : CategorysData.GetString(2)
                    });
                }
                return categories;
            }
        }
    }
}
