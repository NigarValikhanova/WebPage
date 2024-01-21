using MainWebApp.DAL;
using MainWebApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MainWebApp.StaticData;
using MainWebApp.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IEmailService, EmailSender>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = true; //R?q?m t?l?b edin
    options.Password.RequireLowercase = false; //Kiçik h?rf t?l?b edin
    options.Password.RequireUppercase = false; //Böyük h?rf t?l?b edin
    options.Password.RequiredLength = 6; //T?l?b olunan uzunluq...
    options.Password.RequireNonAlphanumeric = false; //@ * ! ve.s kimi simvollar olmalidi

    options.Lockout.MaxFailedAccessAttempts = 2; //2 giri?ten sonra bloklanir 
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMilliseconds(5); //bloklamndiqdan 5deq sonra acilir
    options.Lockout.AllowedForNewUsers = true; //yeni qeydiyyat userdirse passwordu unuda biler.bir nece yazdiqda bloklamaya bilersiz

    options.User.AllowedUserNameCharacters =
 "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";//olmas?n? istediyiniz vacib karaterleri yazin

    options.User.RequireUniqueEmail = true; //unique emaail adresleri olsun (1emaille bir qeydiyyat)

    options.SignIn.RequireConfirmedEmail = false; //qeydiyyat etdikden sonra email ile token gönderecek 
    options.SignIn.RequireConfirmedPhoneNumber = false; //telefon do?rulamas?

});


var app = builder.Build();



using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync(UserRole.Admin.ToString()))
    {
        await roleManager.CreateAsync(new IdentityRole(UserRole.Admin.ToString()));
    }
    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
    if (!await roleManager.RoleExistsAsync("Moderator"))
    {
        await roleManager.CreateAsync(new IdentityRole("Moderator"));
    }
}




if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/Error", "?code={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "error",
    pattern: "/Home/Error/{code?}", // Adjust the route pattern
    defaults: new { controller = "Home", action = "Error" }
);

app.Run();
