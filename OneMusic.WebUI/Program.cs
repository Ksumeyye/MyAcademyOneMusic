using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
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
// Programin katmanlar arasinda tanimladigimiz metotlari islemleri calistirabilmesi icin her bir katman nereden miras almiyorsa onu burda mutlaka tanimlamamiz gerekir.
// Proje WebUI katmanindan calisiyordu ve program cs de bu katmaninin icerisindedir.

builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<OneMusicContext>().AddErrorDescriber<CustomErrorDescriber>();

builder.Services.AddScoped<IAboutDal, EfAboutDal>(); //IAboutDal icerisindeki methodlar EfAboutDal icerisine yazilmistir anlamina gelir Registration islemidir bu
builder.Services.AddScoped<IAboutService,AboutManager>();

builder.Services.AddScoped<IAlbumDal,EfAlbumDal>();
builder.Services.AddScoped<IAlbumService,AlbumManager>(); //BusinessLayer IAlbumService icerisindeki metotlar AlbumManager da yazilmistir (registiration islemi)

builder.Services.AddScoped<IBannerDal, EfBannerDal>();
builder.Services.AddScoped<IBannerService,BannerManager>();

builder.Services.AddScoped<IContactDal, EfContactDal>();
builder.Services.AddScoped<IContactService, ContactManager>();

builder.Services.AddScoped<IMessageDal,EfMessageDal>();
builder.Services.AddScoped<IMessageService,MessageManager>();

builder.Services.AddScoped<ISingerDal,EfSingerDal>();
builder.Services.AddScoped<ISingerService,SingerManager>();

builder.Services.AddScoped<ISongDal,EfSongDal>();
builder.Services.AddScoped<ISongService,SongManager>();

builder.Services.AddScoped<ICategoryDal,EfCategoryDal>();
builder.Services.AddScoped<ICategoryService,CategoryManager>();

builder.Services.AddValidatorsFromAssemblyContaining<SingerValidator>();

builder.Services.AddDbContext<OneMusicContext>();// DbContextimiz de OneMusicContext olarak belirtiriz Bunu vermezsek proje calismaz.

builder.Services.AddControllersWithViews(option=>
{
    var authorizePolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build(); //otantike olan bir kullanici gerekiyor authorize icin
    option.Filters.Add(new AuthorizeFilter(authorizePolicy)); //Bu sekilde proje seviyesinde butun controllerlarimiza authorize eklemis olduk 
    // fakat sadece burasiyla birakirsak loginle kayit olan tum sayfalara erisim kesilmis olur. Bu sorunun giderilmesi icin login ve register
    // controllerlarina AllowAnonymous ekleriz ki onlara erisim saglayalim.
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login/Index"; // bu islemi mvc de webconfig de yapiyorduk net core da ise program cs de yapiyoruz
    options.AccessDeniedPath = "/ErrorPage/AccessDenied"; //yetkisi olmayan kisi bu sayfaya erisir
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
app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404/", "?code{0}"); // olmayan bir sayfaya erismeye calisinca
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}"); //controller="Default" yaptik cunku giris yapildiktan sonra ilk bu sayfaya gidilsin diye

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});
app.Run();
