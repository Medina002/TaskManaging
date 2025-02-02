using AuthenticationProject.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using AuthenticationProject.Data;
using AuthenticationProject.Repositories;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("AuthenticationProjectDbContextConnection")
    ?? throw new InvalidOperationException("Connection string 'AuthenticationProjectDbContextConnection' not found.");

builder.Services.AddDbContext<AuthenticationProjectDbContext>(options =>
    options.UseSqlServer(connectionString));


builder.Services.AddDefaultIdentity<AuthenticationProjectUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false; 
})
.AddEntityFrameworkStores<AuthenticationProjectDbContext>(); 


builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; 
    options.AccessDeniedPath = "/Identity/Account/AccessDenied"; 
    options.SlidingExpiration = true;
});


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();


builder.Services.AddScoped<ITaskRepository, TaskRepository>();


builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireUppercase = false; 
});


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication(); 
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Task}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
