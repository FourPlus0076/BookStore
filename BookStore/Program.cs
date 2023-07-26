using BookStore.Data;
using BookStore.Models;
using BookStore.Repositories.Implementation;
using BookStore.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BookStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
//Add Identity Role
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<BookStoreDbContext>();
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

app.Run();
