using BigIron.RoutePlanner.Application.Services;
using BigIron.RoutePlanner.Domain.Repositories;
using BigIron.RoutePlanner.Domain.Services;
using BigIron.RoutePlanner.Infraestructure.Repositories;
using BigIron.RoutePlanner.Infraestructure.Routing;
using BigIron.RoutePlanner.Infraestructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddSingleton<ILeadRepository, InMemoryLeadRepository>();
builder.Services.AddScoped<ILeadService, LeadServices>();
builder.Services.AddScoped<IRouteOptimizer, RouteOptimizer>();
builder.Services.AddScoped<ICsvLeadParser, CsvLeadParser>();
builder.Services.AddScoped<IExport, CsvExportService>();


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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Leads}/{action=Index}/{id?}");

app.Run();
