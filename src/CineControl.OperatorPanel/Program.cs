using CineControl.Common.Clients.IdentityService.ServiceExtension;
using CineControl.Common.Clients.TenantService.ServiceExtension;
using CineControl.Common.Clients.CinemaService.ServiceExtension;
using CineControl.Common.ServiceDefaults;
using CineControl.Common.Tenant;
using CineControl.OperatorPanel.Service;
using CineControl.OperatorPanel.Service.IService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddTenantProvider();

builder.Services.AddIdentityServiceClient(builder.Configuration)
                .AddTenantServiceClient(builder.Configuration)
                .AddCinemaServiceClient(builder.Configuration);

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ICinemaService, CinemaService>();


builder.AddLogger();

builder.Services.AddAuthentication()
    .AddCookie(options =>
    {
        options.LoginPath = "/Auth/Login";
    });


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
    pattern: "{controller=Auth}/{action=Login}");

app.Run();
