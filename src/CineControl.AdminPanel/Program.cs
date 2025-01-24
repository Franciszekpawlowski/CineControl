using CineControl.Common.JWTProvider.Extension;
using CineControl.Common.Tenant;
using CineControl.Common.ServiceDefaults;
using Microsoft.AspNetCore.DataProtection;
using Serilog;
using CineControl.AdminPanel.Service.IService;
using CineControl.AdminPanel.Service;
using CineControl.Common.Clients.IdentityService.ServiceExtension;
using CineControl.Common.Clients.TenantService.ServiceExtension;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.AddLogger();
builder.Services.AddTenantProvider()
                .AddJWTProvider();

builder.Services.AddIdentityServiceClient(builder.Configuration)
                .AddTenantServiceClient(builder.Configuration);

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddDataProtection()
                .PersistKeysToFileSystem(new DirectoryInfo("./keys"));

builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
        options.ReturnUrlParameter = "ReturnUrl";
        options.SlidingExpiration = true;
    });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
