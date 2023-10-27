using Microsoft.EntityFrameworkCore;
using WEB_153503_SIROTKIN.API.Data;
using WEB_153503_SIROTKIN.API.Services.LaptopCategoryService;
using WEB_153503_SIROTKIN.API.Services.LaptopService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILaptopCategoryService, LaptopCategoryService>();
builder.Services.AddScoped<ILaptopService, LaptopService>();



builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

//await DbInitializer.SeedData(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseStaticFiles();

app.Run();
