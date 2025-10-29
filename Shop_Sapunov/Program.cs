using Shop_Sapunov.Data.DataBase;
using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ICategorys, DBCategory>();
builder.Services.AddTransient<IItems, DBItems>();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.Run();

public class UserBasket 
{
    // Данные о корзине пользователя 
    public static List<ItemsBasket> BasketItem = new List<ItemsBasket>();
}