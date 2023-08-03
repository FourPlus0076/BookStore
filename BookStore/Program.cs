using BookStore.Data;
using BookStore.Helpers;
using BookStore.Models;
using BookStore.Repositories.Implementation;
using BookStore.Repositories.Interface;
using BookStore.Service.Implementation;
using BookStore.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Add Identity Role
builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<BookStoreDbContext>().AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options => {
    options.Password.RequiredLength = 5;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireDigit= false;
    options.Password.RequireLowercase= false;
    options.Password.RequireUppercase= false;
    options.Password.RequireNonAlphanumeric= false;

});
builder.Services.ConfigureApplicationCookie(config => {
    config.LoginPath = builder.Configuration["Application:LoginPath"];
});
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();


#if DEBUG
builder.Services.AddRazorPages().AddRazorRuntimeCompilation().AddViewOptions(option => {
    option.HtmlHelperOptions.ClientValidationEnabled = false;
});

#endif
builder.Services.AddScoped<IBookRepository , BookRepository>();
builder.Services.AddScoped<ILanguageRepository, LanguageRepository>();
builder.Services.AddSingleton<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>,ApplicationUserClaimsPrincipalFactory>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.Configure<SMTPConfigModel>(builder.Configuration.GetSection("SMTPConfig"));
builder.Services.Configure<AlertBookModel>("InternalBook",builder.Configuration.GetSection("AlertBook"));
builder.Services.Configure<AlertBookModel>("ThirdPartyBook", builder.Configuration.GetSection("ThirdPartyBook"));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
      name: "areas",
      pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
    );
});

app.Run();
