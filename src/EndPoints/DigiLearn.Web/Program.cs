using BannerModule;
using BlogModule;
using Common.Application.FileUtil.Interfaces;
using Common.Application.FileUtil.Services;
using CoreModule.Config;
using DigiLearn.Web.Infrastructure;
using DigiLearn.Web.Infrastructure.JwtUtil;
using TicketModule;
using TransactionModule;
using UserModule.Core;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<ILocalFileService, LocalFileService>();
builder.Services.AddScoped<IFtpFileService, FtpFileService>();

builder.Services.AddTransient<TeacherActionFilter>();
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages()
    .AddRazorRuntimeCompilation();

builder.Services
    .InitUserModule(builder.Configuration)
    .InitTicketModule(builder.Configuration)
    .InitCoreModule(builder.Configuration)
    .InitBlogModule(builder.Configuration)
    .InitCommentModule(builder.Configuration)
    .InitTransactionModule(builder.Configuration)
    .InitBannerModule(builder.Configuration)
    .RegisterWebDependencies();





builder.Services.AddJwtAuthentication(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["digi-token"]?.ToString();
    if (string.IsNullOrWhiteSpace(token) == false)
    {
        context.Request.Headers.Append("Authorization", $"Bearer {token}");
    }
    await next();
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapRazorPages();
app.MapDefaultControllerRoute();

app.Run();
