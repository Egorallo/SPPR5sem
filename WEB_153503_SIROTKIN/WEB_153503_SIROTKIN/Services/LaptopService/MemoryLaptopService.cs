using Microsoft.AspNetCore.Mvc;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;
using WEB_153503_SIROTKIN.Services.LaptopCategoryService;

namespace WEB_153503_SIROTKIN.Services.LaptopService
{
    public class MemoryLaptopService : ILaptopService
    {

        private List<Laptop> _laptops = new();
        private List<Category> _categories = new();
        private IConfiguration _config;
        public MemoryLaptopService([FromServices] IConfiguration config, ILaptopCategoryService laptopCategoryService)
        {
            _config = config;

            SetupData();
        }

        public Task<ResponseData<Laptop>> CreateLaptopAsync(Laptop laptop, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteLaptopAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<Laptop>> GetLaptopByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<ListModel<Laptop>>> GetLaptopListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            var filteredLaptops =
               categoryNormalizedName != null ?
               _laptops.Where(laptop => laptop.Category?.NormalizedName == categoryNormalizedName).ToList() :
               _laptops;

            int itemsPerPage = _config.GetValue<int>("ItemsPerPage");

            int totalPages =
                filteredLaptops.Count() % itemsPerPage == 0 ?
                filteredLaptops.Count() / itemsPerPage :
                filteredLaptops.Count() / itemsPerPage + 1;


            var responseData = new ResponseData<ListModel<Laptop>>
            {
                Data = new ListModel<Laptop>
                {
                    Items = filteredLaptops.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
                    CurrentPage = pageNo,
                    TotalPages = totalPages,
                }
            };

            return Task.FromResult(responseData);
        }

        private void SetupData()
        {
            _laptops = new List<Laptop>
            {
                new Laptop
                {
                    Id = 1,
                    Name = "Apple M2 Max",
                    Description = "Флагман Apple в мире ноутбуков",
                    Category = new Category { Id = 1, Name = "Ультрабук", NormalizedName = "ultrabook" },
                    Price = 1600,
                    Image = "/images/apple.jpg"
                },
                new Laptop
                {
                    Id = 2,
                    Name = "MSI TITAN GT",
                    Description = "Флагман игровых ноутбуков MSI.",
                    Category = new Category { Id = 2, Name = "Игровой", NormalizedName = "gaming" },
                    Price = 3000,
                    Image = "/images/msi.jpg"
                },
                new Laptop
                {
                    Id = 3,
                    Name = "ASUS Zenbook Pro 16X",
                    Description = "Бескомпромиссный ноутбук для творчества с множеством инновационных конструктивных особенностей и совершенно новым цельнометаллическим дизайном.",
                    Category = new Category { Id = 3, Name = "Рабочая станция", NormalizedName = "workstation" },
                    Price = 3400,
                    Image = "/images/asus.jpg"
                },
                new Laptop
                {
                    Id = 4,
                    Name = "Chromebook Plus",
                    Description = "Делайте больше, чем вы думали, что можете",
                    Category = new Category { Id = 4, Name = "Хромбук", NormalizedName = "chromebook" },
                    Price = 900,
                    Image = "/images/chromebook.jpg"
                },
                new Laptop
                {
                    Id = 5,
                    Name = "Lenovo ThinkPad X1 Carbon Gen",
                    Description = "Самый продаваемый бизнес-ноутбук с длительным сроком службы и возможностью работы в течение всего дня.",
                    Category = new Category { Id = 1, Name = "Ультрабук", NormalizedName = "ultrabook" },
                    Price = 1300,
                    Image = "/images/thinkpad.jpg"
                }

            };
        }

            public Task UpdateLaptopAsync(int id, Laptop laptop, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
