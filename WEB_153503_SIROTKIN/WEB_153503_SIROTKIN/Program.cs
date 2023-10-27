using WEB_153503_SIROTKIN.Models;
using WEB_153503_SIROTKIN.Services.LaptopCategoryService;
using WEB_153503_SIROTKIN.Services.LaptopService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<ILaptopCategoryService, MemoryLaptopCategoryService>();
//builder.Services.AddScoped<ILaptopService, MemoryLaptopService>();


UriData uriData = builder.Configuration.GetSection("UriData").Get<UriData>()!;

builder.Services
.AddHttpClient<ILaptopCategoryService, ApiLaptopCategoryService>(opt =>
opt.BaseAddress = new Uri(uriData.ApiUri));

builder.Services
.AddHttpClient<ILaptopService, ApiLaptopService>(opt =>
opt.BaseAddress = new Uri(uriData.ApiUri));

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
