using AccsessLayer;
using BanDoWeb.Model.Models;
using BusinessLayer.Hubs;
using BusinessLayer.Repository;
using BusinessLayer.Repository.IRepository;
using DataLayer.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSignalR();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = $"/Identity/Account/Login";
    options.LogoutPath = $"/Identity/Account/Logout";
    options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
});
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder
            .AllowAnyOrigin() // Allow requests from any origin
            .AllowAnyMethod() // Allow any HTTP method
            .AllowAnyHeader(); // Allow any header
    });
});

builder.Services.AddDbContext<DBContextCV>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DbContext"));
});
builder.Services.AddIdentity<Tb_Nguoidung, IdentityRole>(/*options => options.SignIn.RequireConfirmedAccount = true*/)
    .AddDefaultUI()
    .AddDefaultTokenProviders()
    .AddEntityFrameworkStores<DBContextCV>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddTransient<Ikhanh, EmailSenderCV>();
builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseCookiePolicy();
app.UseStaticFiles();
app.MapRazorPages();
app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();
app.MapHub<Notihub>("/NotihubServer");
app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<Chathub>("/chatHub", option =>
    {
        option.LongPolling.PollTimeout = TimeSpan.FromSeconds(30);
    });
});
app.MapAreaControllerRoute(
    name: "Identity",
    areaName: "Identity",
    pattern: "Identity/{controller=Account}/{action=Login}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
