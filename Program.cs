using NSwag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebApplication2.Data;
using WebApplication2.Services;
using Microsoft.AspNetCore.Identity;


var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddOpenApiDocument();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql("Host = localhost;Database=postDB;Username=postgres;Password=ma4421131679a;")); 
builder.Services.AddScoped<IPostService,PostService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSwaggerUi3();

app.UseOpenApi(settings =>
{
    settings.PostProcess = (document, request) =>
    {
      /*  document.Info.Version = "v1";
        document.Info.Title = "Stop Web Crawlers API";
        document.Info.Description =
            "Stop Web Crawlers Update API to enable the update of Referer Spammer Lists";
        document.Info.TermsOfService = "Coming Soon";
        document.Info.Contact = new OpenApiContact
            {Name = "threenine.co.uk", Email = "support@threenine.co.uk", Url = "https://threenine.co.uk"};*/
                    
    };
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();