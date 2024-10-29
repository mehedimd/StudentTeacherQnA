using AutoMapper;
using Microsoft.AspNetCore.Identity;
using STQnA.Core.Common;
using STQnA.Core.Models;
using STQnA.Infrastructure;
using STQnA.Infrastructure.ServiceExtension;
using STQnA.Service.Interfaces;
using STQnA.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// config for Railway.App deployment
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080"; // Use 8080 as default if PORT is not set
builder.WebHost.ConfigureKestrel(options => options.ListenAnyIP(int.Parse(port)));
// end config for Railway.App deployment

// Configure DbContext and register repository by Extension method AddDIService
builder.Services.AddDIServices(builder.Configuration);

// register service
builder.Services.AddScoped<IQuestionService, QuestionService>();
builder.Services.AddScoped<IAnswerService, AnswerService>();
builder.Services.AddScoped<IUserService, UserService>();

// Configure Identity
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    option.Password.RequiredLength = 4;
    option.Password.RequireUppercase = false;
    option.Password.RequireLowercase = false;
    option.Password.RequireDigit = false;
    option.Password.RequireNonAlphanumeric = false;
    option.User.RequireUniqueEmail = true;

})
    .AddEntityFrameworkStores<STQnAContext>()
    .AddDefaultTokenProviders();

// auto mapper
var mappingConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new AppAutoMapperProfile());
});
var mapper = mappingConfig.CreateMapper();

builder.Services.AddSingleton(mapper);

// for get current user credential
builder.Services.AddHttpContextAccessor();

// Add services to the container.
builder.Services.AddControllersWithViews();

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
    pattern: "{controller=Question}/{action=Index}/{id?}");

app.Run();
