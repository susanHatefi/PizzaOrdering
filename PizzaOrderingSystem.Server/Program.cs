using NLog.Web;
using PizzaOrderingSystem.Domain.Model.Contracts;
using PizzaOrderingSystem.Domain.Services;
using PizzaOrderSystem.DataAccess;
using PizzaOrderSystem.DataAccess.Model.Entities;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title="Pizza Ordering APIs", Version="v1" });
    
});
builder.Logging.ClearProviders();
builder.Host.UseNLog();
// Add services to the container.
builder.Services.AddDataAccess();
builder.Services.AddRepository<Order>();
builder.Services.AddRepository<OrderItem>();
builder.Services.AddRepository<Product>();
builder.Services.AddRepository<Topping>();
builder.Services.AddRepository<Promotion>();
builder.Services.AddRepository<ToppingUnit>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPromotionService, PromotionService>();
builder.Services.AddTransient<IToppingService, ToppingService>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("all", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors("all");
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseRouting();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

// Configure the HTTP request pipeline.

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
