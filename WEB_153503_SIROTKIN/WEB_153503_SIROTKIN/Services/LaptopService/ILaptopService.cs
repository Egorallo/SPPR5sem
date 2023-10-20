using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.Services.LaptopService
{
    public interface ILaptopService
    {
        /// <summary>
        /// Получение списка всех объектов
        /// </summary>
        /// <param name="categoryNormalizedName">нормализованное имя категории для фильтрации</param>
        /// <param name="pageNo">номер страницы списка</param>
        /// <returns></returns>
        public Task<ResponseData<ListModel<Laptop>>> GetLaptopListAsync(string? categoryNormalizedName, int pageNo = 1);
        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Найденный объект или null, если объект не найден</returns>
        public Task<ResponseData<Laptop>> GetLaptopByIdAsync(int id);
        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="id">Id изменяемомго объекта</param>
        /// <param name="laptop">объект с новыми параметрами</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns></returns>
        public Task UpdateLaptopAsync(int id, Laptop laptop, IFormFile? formFile);
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id удаляемомго объекта</param>
        /// <returns></returns>
        public Task DeleteLaptopAsync(int id);
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="laptop">Новый объект</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns>Созданный объект</returns>
        public Task<ResponseData<Laptop>> CreateLaptopAsync(Laptop laptop, IFormFile? formFile);
    }
}
