using jQueryDatatableServerSideNetCore.Data;
using jQueryDatatableServerSideNetCore.Services.CsvService;
using jQueryDatatableServerSideNetCore.Services.ExcelService;
using jQueryDatatableServerSideNetCore.Services.ExportService;
using jQueryDatatableServerSideNetCore.Services.HtmlService;
using jQueryDatatableServerSideNetCore.Services.JsonService;
using jQueryDatatableServerSideNetCore.Services.XmlService;
using jQueryDatatableServerSideNetCore.Services.YamlService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddScoped<IExportService, ExportService>();
builder.Services.AddScoped<IExcelService, ExcelService>();
builder.Services.AddScoped<ICsvService, CsvService>();
builder.Services.AddScoped<IHtmlService, HtmlService>();
builder.Services.AddScoped<IJsonService, JsonService>();
builder.Services.AddScoped<IXmlService, XmlService>();
builder.Services.AddScoped<IYamlService, YamlService>();

// Register the Swagger generator, defining 1 or more Swagger documents
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "jQueryDatatableServerSideNetCore", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.FirstOrDefault());

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
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

app.UseHttpsRedirection();
app.UseStaticFiles();

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "docs";
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "jQueryDatatableServerSideNetCore Docs v1");
});

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=TestRegisters}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
