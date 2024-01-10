using StoreApp.Infrastructure.Extensions;
using StoreApp.Models;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers(); // this means API will be supported
builder.Services.AddControllers().AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);


builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); //Razor page kullanýmý için gerekli Servis tanýmý

//added part: to resolve error: View ".." not found  - VS 2022 Bug
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();

builder.Services.ConfigureDbContext(builder.Configuration);
builder.Services.ConfigureIdentity();
builder.Services.ConfigureSession();
builder.Services.ConfigureRepositoryRegistration();
builder.Services.ConfigureServiceRegistration();
builder.Services.ConfigureRouting();
builder.Services.ConfigureApplicationCookie();

//builder.Services.AddSingleton<Cart>(); //herkes ayný Cart'ý kullanacak
//builder.Services.AddScoped<Cart>(c => SessionCart.GetCart(c));     //her kullanýcý için ayrý cart

builder.Services.AddAutoMapper(typeof(Program));

var app = builder.Build();

app.UseStaticFiles(); //  ==> for including wwwroot folder
app.UseSession();

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication(); // 1  Burasý Routing ve Endpoint'ler arasýna yazýlmalý
app.UseAuthorization();  // 2 --   sýra önemli


app.UseEndpoints(endpoints =>
{
    endpoints.MapAreaControllerRoute(
        name:"Admin", 
        areaName:"Admin",
        pattern: "Admin/{controller=Dashboard}/{action=Index}/{id?}");

    endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapRazorPages(); //Razor page kullanýmý için gerekli

    endpoints.MapControllers(); //API desteðinde endpointleri çözümlemek içn gerekli
});


app.ConfigureAndCheckMigration();
app.ConfigureLocalization();
app.ConfigureDefaultAdminUser();
app.Run();

// to pass command prompt: chdir c:\users\samsung\mvc\store\storeapp
// Admin Admin+123456;    Filiz: pwd : filiz+12345;   Ali: pwd: ali+123458.
// 8.1.2024'te orijinal halind branch açarak çalýþýlmaya baþlandý.
