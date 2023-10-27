using Microsoft.EntityFrameworkCore;
using WEB_153503_SIROTKIN.Domain.Entities;

namespace WEB_153503_SIROTKIN.API.Data
{
    public class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Выполнение миграций
            await context.Database.MigrateAsync();


            var categories = new List<Category>
                {
                new Category {Name="Ультрабук",NormalizedName="ultrabook"},
                new Category {Name="Игровой", NormalizedName="gaming"},
                new Category {Name="Рабочая станция", NormalizedName="workstation"},
                new Category {Name="Хромбук",NormalizedName="chromebook" }
                };

            await context.Categories.AddRangeAsync(categories);
            await context.SaveChangesAsync();

            string imageRoot = $"{app.Configuration["AppUrl"]!}/images";
            var laptops = new List<Laptop>
            {
                new Laptop
                {
                    
                    Name = "Apple M2 Max",
                    Description = "Флагман Apple в мире ноутбуков",
                    Category = await context.Categories.SingleAsync(g => g.NormalizedName.Equals("ultrabook")),
                    Price = 1600,
                    Image = $"{imageRoot}/apple.jpg"
                },
                new Laptop
                {
                    
                    Name = "MSI TITAN GT",
                    Description = "Флагман игровых ноутбуков MSI.",
                    Category = await context.Categories.SingleAsync(g => g.NormalizedName.Equals("gaming")),
                    Price = 3000,
                    Image = $"{imageRoot}/msi.jpg"
                },
                new Laptop
                {
                   
                    Name = "ASUS Zenbook Pro 16X",
                    Description = "Бескомпромиссный ноутбук для творчества с множеством инновационных конструктивных особенностей и совершенно новым цельнометаллическим дизайном.",
                    Category = await context.Categories.SingleAsync(g => g.NormalizedName.Equals("workstation")),
                    Price = 3400,
                    Image = $"{imageRoot}/asus.jpg"
                },
                new Laptop
                {
                 
                    Name = "Chromebook Plus",
                    Description = "Делайте больше, чем вы думали, что можете",
                    Category = await context.Categories.SingleAsync(g => g.NormalizedName.Equals("chromebook")),
                    Price = 900,
                    Image = $"{imageRoot}/chromebook.jpg"
                },
                new Laptop
                {
                   
                    Name = "Lenovo ThinkPad X1 Carbon Gen",
                    Description = "Самый продаваемый бизнес-ноутбук с длительным сроком службы и возможностью работы в течение всего дня.",
                    Category = await context.Categories.SingleAsync(g => g.NormalizedName.Equals("ultrabook")),
                    Price = 1300,
                    Image = $"{imageRoot}/thinkpad.jpg"
                }

            };

            await context.Laptops.AddRangeAsync(laptops);
            await context.SaveChangesAsync();
        }
    }
}
