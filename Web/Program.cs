using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;
using Web;
using Web.Data;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddLocalization(option => option.ResourcesPath = "Resources");

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(option=>
{

    var type = typeof(SharedResource);
    var assemblyName = new AssemblyName(type.GetTypeInfo
        ().Assembly.FullName);
    //var factory = services.BuildServiceProvider

    //().GetService<IStringLocalizerFactory>();
    //var localizer = factory.Create("SharedResource",
    //    assemblyName.Name);
    //option.DataAnnotationLocalizerProvider = (t, f) => localizer;
});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("fr-FR"),
    new CultureInfo("de-DE")
};
app.UseRequestLocalization(new RequestLocalizationOptions{
    DefaultRequestCulture=new RequestCulture("en-US"),
    SupportedCultures=supportedCultures,
    SupportedUICultures=supportedCultures
});






app.UseStaticFiles();






app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action}/{id?}");
app.MapRazorPages();

app.Run();
