using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153503_SIROTKIN.API.Data;
using WEB_153503_SIROTKIN.API.Services.LaptopService;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaptopsController : ControllerBase
    {
        private readonly ILaptopService _laptopService;

        public LaptopsController(ILaptopService laptopService)
        {
            _laptopService= laptopService;
        }

        // GET: api/Laptops
        [HttpGet("{pageNo:int}")]
        [HttpGet("{category?}/{pageNo:int?}")]
        public async Task<ActionResult<IEnumerable<Laptop>>> GetLaptops(string? category, int pageNo = 1, int pageSize = 3)
        {
            var response = await _laptopService.GetLaptopListAsync(category, pageNo, pageSize);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // GET: api/Laptops/laptop-5
        [HttpGet("laptop-{id}")]
        public async Task<ActionResult<Laptop>> GetLaptop(int id)
        {
            var response = await _laptopService.GetLaptopByIdAsync(id);
            return response.Success ? Ok(response) : NotFound(response);
        }

        // PUT: api/Laptops/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLaptop(int id, Laptop laptop)
        {

            try
            {
                await _laptopService.UpdateLaptopAsync(id, laptop);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseData<Laptop>()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return Ok(new ResponseData<Laptop>()
            {
                Data = laptop,
                Success = true,
            });
        }

        // POST: api/Laptops
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Laptop>> PostLaptop(Laptop laptop)
        {
            var response = await _laptopService.CreateLaptopAsync(laptop);
            return response.Success ? Ok(response) : BadRequest(response);
        }

        // DELETE: api/Laptops/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLaptop(int id)
        {
            try
            {
                await _laptopService.DeleteLaptopAsync(id);
            }
            catch (Exception ex)
            {
                return NotFound(new ResponseData<Laptop>()
                {
                    Success = false,
                    ErrorMessage = ex.Message
                });
            }

            return NoContent();
        }

        // POST: api/Tools/5
        [HttpPost("{id}")]
        public async Task<ActionResult<ResponseData<string>>> PostImage(int id, IFormFile formFile)
        {
            var response = await _laptopService.SaveImageAsync(id, formFile);
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response);
        }

        private async Task<bool> LaptopExists(int id)
        {
            return (await _laptopService.GetLaptopByIdAsync(id)).Success;
        }
    }
}
