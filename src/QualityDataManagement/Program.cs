using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using QualityData.DbAccess.Extensions;
using QualityDataManagement.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();
// 增加 BootstrapBlazor 组件
builder.Services.AddBootstrapBlazor();
// 增加 EFCore 数据服务
builder.Services.AddEFCoreDataAccessServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
