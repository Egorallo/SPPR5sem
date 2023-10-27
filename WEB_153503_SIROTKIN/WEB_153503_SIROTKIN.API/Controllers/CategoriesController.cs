using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_153503_SIROTKIN.API.Data;
using WEB_153503_SIROTKIN.API.Services.LaptopCategoryService;
using WEB_153503_SIROTKIN.Domain.Entities;
using WEB_153503_SIROTKIN.Domain.Models;

namespace WEB_153503_SIROTKIN.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ILaptopCategoryService _laptopCategoryService;

        public CategoriesController(ILaptopCategoryService laptopCategoryService)
        {
            _laptopCategoryService = laptopCategoryService;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<ResponseData<Category>>> GetCategories()
        {
            var responce = await _laptopCategoryService.GetCategoryListAsync();
            return responce.Success ? Ok(responce) : BadRequest(responce);
        }

      
    }
}
