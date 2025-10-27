using Shop_Sapunov.Data.Interfaces;
using Shop_Sapunov.Data.Mocks;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<ICategorys, MockCategorys>();
builder.Services.AddTransient<IItems, MockItems>();
builder.Services.AddMvc(option => option.EnableEndpointRouting = false);
var app = builder.Build();

app.UseDeveloperExceptionPage();
app.UseStatusCodePages();
app.UseStaticFiles();
app.UseMvcWithDefaultRoute();
app.Run();