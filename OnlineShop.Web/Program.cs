//var builder = WebApplication.CreateBuilder(args);

//// Add services to the container.
//builder.Services.AddControllersWithViews();

//var app = builder.Build();

//// Configure the HTTP request pipeline.
//if (!app.Environment.IsDevelopment())
//{
//    app.UseExceptionHandler("/Home/Error");
//    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//    app.UseHsts();
//}

//app.UseHttpsRedirection();
//app.UseStaticFiles();

//app.UseRouting();

//app.UseAuthorization();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run();

using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Abstractions;
using OnlineShop.Domain.Services;
using OnlineShop.Infrastructure.Persistence;
using OnlineShop.Infrastructure.Repositories;


var builder = WebApplication.CreateBuilder(args);


// MVC
builder.Services.AddControllersWithViews();
builder.Services.AddSession();


// EF Core + SQL Server
builder.Services.AddDbContext<ShopDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// Repository
builder.Services.AddScoped<IProductRepository, ProductRepository>();


// Discount Policy from config
var cfg = builder.Configuration.GetSection("Discount");
var threshold = cfg.GetValue<decimal>("Threshold");
var percent = cfg.GetValue<decimal>("Percent");
var maxDiscount = cfg.GetValue<decimal>("MaxDiscount");
builder.Services.AddSingleton<IDiscountPolicy>(new ThresholdCappedDiscountPolicy(threshold, percent, maxDiscount));


// Cart Service (per-session)
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();
builder.Services.AddScoped<ICartService, CartService>();


var app = builder.Build();


// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();


app.MapControllerRoute(
name: "default",
pattern: "{controller=Catalog}/{action=Index}/{id?}");


// Migrate DB at startup (optional)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    db.Database.Migrate();
}


app.Run();
