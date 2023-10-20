using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.Services.LaptopCategoryService
{
    public class MemoryLaptopCategoryService : ILaptopCategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            var categories = new List<Category>
                {
                new Category {Id=1, Name="Ультрабук",NormalizedName="ultrabook"},
                new Category {Id=2, Name="Игровой", NormalizedName="gaming"},
                new Category {Id=3, Name="Рабочая станция", NormalizedName="workstation"},
                new Category {Id=4, Name="Хромбук",NormalizedName="chromebook" }
                };
            var result = new ResponseData<List<Category>>();
            result.Data = categories;
            return Task.FromResult(result);
        }
    }
}
