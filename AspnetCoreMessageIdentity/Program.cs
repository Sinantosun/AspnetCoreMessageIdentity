using AspnetCoreMessageIdentity.DAL;
using AspnetCoreMessageIdentity.DAL.Context;
using AspnetCoreMessageIdentity.DAL.Entities;
using AspnetCoreMessageIdentity.Hubs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(opt =>
{
    opt.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyHeader().
        AllowAnyMethod().
        SetIsOriginAllowed((host) => true)
        .AllowCredentials();
    });
});

builder.Services.AddSignalR();


builder.Services.AddIdentity<AppUser, AppRole>(opts =>
{
    opts.User.RequireUniqueEmail = true;
    opts.User.AllowedUserNameCharacters = "abcçdefghiýjklmnoöpqrsþtuüvwxyzABCÇDEFGHIÝJKLMNOÖPQRSÞTUÜVWXYZ0123456789._";
    opts.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);


    opts.Lockout.MaxFailedAccessAttempts = 5;
    opts.Lockout.AllowedForNewUsers = true;
}).AddErrorDescriber<CustomErrorDescriber>().AddDefaultTokenProviders().AddEntityFrameworkStores<MailContext>();

builder.Services.AddDbContext<MailContext>();
builder.Services.AddControllersWithViews(opts =>
{
    opts.Filters.Add(new AuthorizeFilter());

});

builder.Services.ConfigureApplicationCookie(otpions =>
{
    otpions.LoginPath = "/Login/Index";
    otpions.Cookie.Name = Guid.NewGuid().ToString();
    otpions.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    otpions.SlidingExpiration = true;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("CorsPolicy");
app.MapHub<SignalRMessageHub>("/signalrhub");


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
