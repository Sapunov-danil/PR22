using MySql.Data.MySqlClient;
using Shop_Sapunov.Data.Common;
using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Models;

namespace Shop_Sapunov.Data.DataBase
{
    public class DBItems : IItems
    {
        public IEnumerable<Categorys> Categorys = new DBCategory().AllCategorys;

        public IEnumerable<Items> AllItems
        {
            get
            {
                List<Items> items = new List<Items>();
                MySqlConnection MySqlConnection = Connection.MySqlOpen();
                MySqlDataReader ItemsData = Connection.MySqlQuery("SELECT * FROM Shop.Items ORDER BY `Name`;", MySqlConnection);
                while (ItemsData.Read())
                {
                    items.Add(new Items()
                    {
                        Id = ItemsData.IsDBNull(0) ? -1 : ItemsData.GetInt32(0),
                        Name = ItemsData.IsDBNull(1) ? null : ItemsData.GetString(1),
                        Description = ItemsData.IsDBNull(2) ? null : ItemsData.GetString(2),
                        Img = ItemsData.IsDBNull(3) ? null : ItemsData.GetString(3),
                        Price = ItemsData.IsDBNull(4) ? -1 : ItemsData.GetInt32(4),
                        Category = ItemsData.IsDBNull(5) ? null : Categorys.Where(x => x.Id == ItemsData.GetInt32(5)).First()
                    });
                }
                MySqlConnection.Close();
                return items;
            }
        }

        public int Add(Items Item)
        {
            MySqlConnection MySqlConnection = Connection.MySqlOpen();
            Connection.MySqlQuery($"INSERT INTO `Items` (`Name`, `Description`, `Img`, `Price`, `IdCategory`) VALUES ('{Item.Name}', '{Item.Description}', '{Item.Img}', {Item.Price}, {Item.Category.Id});", MySqlConnection);
            MySqlConnection.Close();

            int IdItem = -1;

            MySqlConnection = Connection.MySqlOpen();
            MySqlDataReader mySqlDataReaderItem = Connection.MySqlQuery($"SELECT `Id` FROM `Items` WHERE `Name` = '{Item.Name}' AND `Description` = '{Item.Description}' AND `Price` = {Item.Price} AND `IdCategory` = {Item.Category.Id};", MySqlConnection);

            if (mySqlDataReaderItem.HasRows)
            {
                mySqlDataReaderItem.Read();
                IdItem = mySqlDataReaderItem.GetInt32(0);
            }
            MySqlConnection.Close();
            return IdItem;
        }
    }
}
