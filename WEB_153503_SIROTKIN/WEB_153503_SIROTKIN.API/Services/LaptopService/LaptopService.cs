using Microsoft.EntityFrameworkCore;
using WEB_153503_SIROTKIN.API.Data;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.API.Services.LaptopService
{
    public class LaptopService : ILaptopService
    {

        private readonly int _maxPageSize = 20;
        private readonly AppDbContext _dbContext;

        public LaptopService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseData<Laptop>> CreateLaptopAsync(Laptop laptop)
        {
            try
            {
                _dbContext.Laptops.Add(laptop);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new ResponseData<Laptop>
                {
                    Success = false,
                    ErrorMessage = ex.Message,
                };
            }

            return new ResponseData<Laptop>
            {
                Data = laptop,
                Success = true,
            };
        }

        public async Task DeleteLaptopAsync(int id)
        {
            var laptop = await _dbContext.Laptops.FindAsync(id);

            if (laptop is null)
                return;


            _dbContext.Laptops.Remove(laptop);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<ResponseData<Laptop>> GetLaptopByIdAsync(int id)
        {
            var laptop = await _dbContext.Laptops.FindAsync(id);

            if (laptop is null)
            {
                return new ResponseData<Laptop>
                {
                    Success = false,
                    ErrorMessage = "No laptops was found",
                };
            }

            return new ResponseData<Laptop>
            {
                Data = laptop,
                Success = true,
            };
        }

        public async Task<ResponseData<ListModel<Laptop>>> GetLaptopListAsync(string? categoryNormalizedName, int pageNo = 1, int pageSize = 3)
        {
            if (pageSize > _maxPageSize)
                pageSize = _maxPageSize;

            var query = _dbContext.Laptops.AsQueryable();
            var dataList = new ListModel<Laptop>();
            query = query.Where(d => categoryNormalizedName == null || d.Category!.NormalizedName.Equals(categoryNormalizedName));

            var count = await query.CountAsync();

            if (count == 0)
            {
                return new ResponseData<ListModel<Laptop>>
                {
                    Data = dataList,
                    Success = true,
                };
            }

            int totalPages = count % pageSize == 0 ?
                    count / pageSize :
                    count / pageSize + 1;

            if (pageNo > totalPages)
            {
                return new ResponseData<ListModel<Laptop>>
                {
                    Success = false,
                    ErrorMessage = "No such page"
                };
            }

            dataList.Items = await query.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync();
            dataList.CurrentPage = pageNo;
            dataList.TotalPages = totalPages;

            return new ResponseData<ListModel<Laptop>>
            {
                Data = dataList,
                Success = true,
            };
        }

        public Task<ResponseData<string>> SaveImageAsync(int id, IFormFile formFile)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateLaptopAsync(int id, Laptop laptop)
        {
            var updatingLaptop = await _dbContext.Laptops.FindAsync(id);

            if (updatingLaptop is null)
                return;

            updatingLaptop.Name = laptop.Name;
            updatingLaptop.Description = laptop.Description;
            updatingLaptop.Price = laptop.Price;
            updatingLaptop.Image = laptop.Image;
            updatingLaptop.Category = laptop.Category;
        }
    }
}
