using FirstApii.Data.DAL;
using FirstApii.Dtos.CategoryDtos;
using FirstApii.Dtos.ProductDtos;
using FirstApii.Extentions;
using FirstApii.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FirstApii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(AppDbContext appDbContext, IWebHostEnvironment webHostEnvironment)
        {
            _appDbContext = appDbContext;
            this._webHostEnvironment = webHostEnvironment;
        }
        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            var category = _appDbContext.Categories
                 .Where(c => !c.IsDeleted)
                 .FirstOrDefault(c => c.Id == id);
         
            if (category == null) return StatusCode(StatusCodes.Status404NotFound);
            CategoryReturnDto categoryReturnDto = new();
            categoryReturnDto.Name = category.Name;
            categoryReturnDto.Desc = category.Desc;
            categoryReturnDto.ImageUrl= "https://localhost:7110/img/" + category.ImageUrl;
            categoryReturnDto.UpdateDate = category.UpdateDate;
            categoryReturnDto.CreateDate = category.CreateDate;
            return Ok(category);
        }
        [HttpGet]
        public IActionResult GetAll([FromQuery]int page, string search)
        {
            var query = _appDbContext.Categories
                .Where(c => !c.IsDeleted);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(c=> c.Name.Contains(search));

            }

            CategoryListDto categoryListDto = new();
            categoryListDto.TotalCount = query.Count();
            categoryListDto.CurrentPage = page;

            categoryListDto.Items = query.Skip((page - 1) * 2)
                .Take(2)
                .Select(c=> new CategoryListItemDto
                {

                    Name= c.Name,
                    Desc= c.Desc,
                    ImageUrl= "https://localhost:7110/img/" + c.ImageUrl,
            CreateDate = c.CreateDate,
                    UpdateDate= c.UpdateDate,


                }).ToList();

            List<CategoryListItemDto> listItemDtos = new();


            return StatusCode(200, categoryListDto);
           
        }
        [HttpPost]
        public IActionResult AddCategory([FromForm]CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto.Photo == null) return StatusCode(409);
            if (!categoryCreateDto.Photo.IsImage()) return BadRequest("photo type deil");
            if(categoryCreateDto.Photo.CheckImageSize(10)) return BadRequest("size duzgun deil");

            Category newCategory = new()
            {
                Name = categoryCreateDto.Name,
                Desc = categoryCreateDto.Desc,
                ImageUrl = categoryCreateDto.Photo.SaveImage(_webHostEnvironment,"img",categoryCreateDto.Photo.FileName)
            
            };

            _appDbContext.Categories.Add(newCategory); 
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created, newCategory);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var category = _appDbContext.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null) return NotFound();
            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        public IActionResult Update(int id,string name)
        {
            var category=_appDbContext.Categories.FirstOrDefault(c=>c.Id == id);
            if (category == null) return BadRequest("yoxdu");
            bool result = _appDbContext.Categories.Any(c => c.Name == name && c.Id != category.Id);
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}