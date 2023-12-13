using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Aram.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AramContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AramContext") ?? throw new InvalidOperationException("Connection string 'AramContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(24);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=CuaHang}/{action=Index}/{id?}");

app.Run();
