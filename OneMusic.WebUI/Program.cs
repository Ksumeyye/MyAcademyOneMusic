using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using OneMusic.BusinessLayer.Abstract;
using OneMusic.BusinessLayer.Concrete;
using OneMusic.BusinessLayer.Validators;
using OneMusic.DataAccessLayer.Abstract;
using OneMusic.DataAccessLayer.Concrete;
using OneMusic.DataAccessLayer.Context;
using OneMusic.EntityLayer.Entities;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>();

builder.Services.AddScoped<IAboutDal, EfAboutDal>(); //IAboutDal içerisindeki methodlar EfAboutDal içerisine yazýlmýþtýr anlamýna gelir.(Registration iþlemidir bu.)
builder.Services.AddScoped<IAboutService,AboutManager>();

builder.Services.AddScoped<IAlbumDal,EfAlbumDal>();
builder.Services.AddScoped<IAlbumService,AlbumManager>();

builder.Services.AddScoped<IBannerDal,EfBannerDal>();
builder.Services.AddScoped<IBannerService,BannerManager>();

builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IMessageDal,EfMessageDal>();
builder.Services.AddScoped<IMessageService,MessageManager>();

builder.Services.AddScoped<ISingerDal,EfSingerDal>();
builder.Services.AddScoped<ISingerService,SingerManager>();

builder.Services.AddScoped<ISongDal,EfSongDal>();
builder.Services.AddScoped<ISongService,SongManager>();

builder.Services.AddValidatorsFromAssemblyContaining<SingerValidator>();

builder.Services.AddDbContext<OneMusicContext>();
builder.Services.AddControllersWithViews(option=>
{
    var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    option.Filters.Add(new AuthorizeFilter(authorizePolicy));
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index";
    options.AccessDeniedPath = "/ErrorPage/AccessDenied";
    options.LogoutPath= "/Login/Logout";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/", "?code{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}"); //controller="Default" yaptýk. Çünkü giriþ yapýldýktan sonra ilk bu sayfaya gidilsin diye

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
