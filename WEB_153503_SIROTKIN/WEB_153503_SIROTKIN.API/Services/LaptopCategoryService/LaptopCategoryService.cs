using WEB_153503_SIROTKIN.API.Data;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace WEB_153503_SIROTKIN.API.Services.LaptopCategoryService
{
    public class LaptopCategoryService : ILaptopCategoryService
    {
        private readonly AppDbContext _dbContext;

        public LaptopCategoryService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseData<List<Category>>> GetCategoryListAsync()
        {
            return new ResponseData<List<Category>>
            {
                Data = await _dbContext.Categories.ToListAsync(),
            };
        }
    }
}
