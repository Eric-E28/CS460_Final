using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using HWK6.Models;
using HWK6.DAL.Abstract;
using HWK6.DAL.Concrete;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddUserSecrets<Program>();

        var connectionString = builder.Configuration["CoffeeShop:AzureCS"];
        var dbPassword = builder.Configuration["CoffeeShop:DBPassword"];

        var sqlBuilder = new SqlConnectionStringBuilder(connectionString);
        sqlBuilder.Password = dbPassword;

        var finalConnectionString = sqlBuilder.ConnectionString;

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<CoffeeShopDbContext>(options => options
            .UseLazyLoadingProxies()
            .UseSqlServer(finalConnectionString));

        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IOrderGeneratorService, OrderGeneratorService>();
        builder.Services.AddScoped<IOrderItemRepository, OrderItemRepository>();
        builder.Services.AddScoped<IOrderRepository, OrderRepository>();

        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseHttpsRedirection();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}
