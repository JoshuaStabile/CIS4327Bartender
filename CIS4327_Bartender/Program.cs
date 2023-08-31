using CIS4327_Bartender.Models.Data;
using CIS4327_Bartender.Models;
using CIS4327_Bartender.Services;
using CIS4327_Bartender.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using CIS4327_Bartender.Models.Cart;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddTransient<ICocktailService, CocktailService>();
builder.Services.AddTransient<IOrderService, OrderService>();
builder.Services.AddTransient<IServiceProvider, ServiceProvider>();
builder.Services.AddScoped<Cart>(sp => SessionCartModel.GetCart(sp));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddSession(); // ADDED
builder.Services.AddMvc();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole<Guid>>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

var app = builder.Build();

// var service = app.Services.GetService<IServiceProvider>();
var service = builder.Services.BuildServiceProvider();

if (service != null)
{
    await CreateRoles(service);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseSession(); // ADDED

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

async Task CreateRoles(IServiceProvider serviceProvider)
{ 
    //initializing custom roles 
    RoleManager<IdentityRole<Guid>> roleManager;
    UserManager<AppUser> userManager;
    roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
    userManager = serviceProvider.GetRequiredService<UserManager<AppUser>>();
    string[] roleNames = { "Admin", "Employee", "Customer" };
    IdentityResult roleResult;

    foreach (var name in roleNames)
    {
        var roleExist = await roleManager.RoleExistsAsync(name);
        // ensure that the role does not exist
        if (!roleExist)
        {
            roleResult = await roleManager.CreateAsync(new IdentityRole<Guid>(name));
        }
    }

    // find the user with the admin email 
    var _adminUser = await userManager.FindByEmailAsync("admin@unf.edu");

    // check if the user exists
    if (_adminUser == null)
    {
        //Here you could create the super admin who will maintain the web app
        var adminUser = new AppUser
        {
            UserName = "admin",
            Email = "admin@unf.edu",
        };
        string adminPassword = "Snakeo99*";


        var createPowerUser = await userManager.CreateAsync(adminUser, adminPassword);
        if (createPowerUser.Succeeded)
        {
            //here we tie the new user to the role
            await userManager.AddToRoleAsync(adminUser, "Admin");

        }
    }
}