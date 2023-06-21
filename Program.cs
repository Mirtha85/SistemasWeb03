using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SistemasWeb01.DataAccess;
using SistemasWeb01.Helpers;
using SistemasWeb01.Models;
using SistemasWeb01.Repository.Implementations;
using SistemasWeb01.Repository.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//conexion
builder.Services.AddDbContext<ShoppingDbContext>(options => {
    options.UseSqlite(
        builder.Configuration["ConnectionStrings:DbShoppingConnection"]);
});

builder.Services.AddTransient<DatabaseInitializer>();

//TODO: Make strongest password
builder.Services.AddIdentity<User, IdentityRole>(cfg =>
{
    //
    cfg.User.RequireUniqueEmail = true;
    cfg.Password.RequireDigit = false; //no requiere numeros
    cfg.Password.RequiredUniqueChars = 0; //no requiere caracteres únicos
    cfg.Password.RequireLowercase = false; //no requiere al menos una minuscula
    cfg.Password.RequireNonAlphanumeric = false; // no requiere al menos un caracter alfanumerico
    cfg.Password.RequireUppercase = false; // no requiere al menos una minuscula
    cfg.Password.RequiredLength = 4; // longitud minima 6 caracteres
}).AddEntityFrameworkStores<ShoppingDbContext>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IStateRepository, StateRepository>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ITallaRepository, TallaRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductSizeRepository, ProductSizeRepository>();
builder.Services.AddScoped<IPictureRepository, PictureRepository>();
builder.Services.AddScoped<IBrandRepository, BrandRepository>();
builder.Services.AddScoped<IFormFileHelper, FormFileHelper>();
builder.Services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();

var app = builder.Build();


//manual injection DbInitializer
SeedData(app);
void SeedData(WebApplication app)
{
    IServiceScopeFactory? scopedFactory = app.Services.GetService<IServiceScopeFactory>();

    using (IServiceScope? scope = scopedFactory.CreateScope())
    {
        DatabaseInitializer? service = scope.ServiceProvider.GetService<DatabaseInitializer>();
        service.SeedAsync().Wait();
    }
}


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

//
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//DbInitializer.Seed(app);
app.Run();
