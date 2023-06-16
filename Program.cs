using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Helpers;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITallaRepository, TallaRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IPictureRepository, PictureRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IFormFileHelper, FormFileHelper>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

//conexion
builder.Services.AddDbContext<ShoppingDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DbShoppingConnection"]);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

DbInitializer.Seed(app);
app.Run();
