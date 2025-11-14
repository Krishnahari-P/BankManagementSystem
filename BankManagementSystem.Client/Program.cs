using BankManagementSystem.Client.HttpClients;
using Microsoft.AspNetCore.Authentication.Cookies;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IGenericHttpClient, GenericHttpClient>();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation().AddNToastNotifyToastr(new NToastNotify.ToastrOptions
{
    CloseButton = true,
    CloseDuration = true,
    TimeOut = 5000,
    PositionClass = ToastPositions.TopRight
});
builder.Services.AddHttpClient();
//builder.Services.AddAuthentication().AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        //options.AccessDeniedPath = "/Account/AccessDenied";
    });

builder.Services.AddAuthorization();

var app = builder.Build();
app.UseAuthentication();
app.UseAuthorization();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
