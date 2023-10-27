using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.API.Services.LaptopCategoryService
{
    public interface ILaptopCategoryService
    {
        /// <summary>
        /// Получение списка всех категорий
        /// </summary>
        /// <returns></returns>

        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
