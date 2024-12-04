using CineControl.Common.Clients.IdentityService;
using CineControl.Common.Clients.IdentityService.IClients;
using CineControl.Common.ServiceDefaults;
using CineControl.OperatorPanel.Service;
using CineControl.OperatorPanel.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<IIdentityServiceClient, IdentityServiceClient>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.AddLogger();
builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
    });
// builder.AddJwtAuthExtension();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Login}");

app.Run();
