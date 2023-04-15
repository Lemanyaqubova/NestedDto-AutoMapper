using AutoMapper;
using FirstApii.Data.DAL;
using FirstApii.Dtos.ProductDtos;
using FirstApii.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FirstApii.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public ProductController(AppDbContext appDbContext, IMapper mapper = null)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAll(int page,string?  search)
        {
            var query = _appDbContext.Products
                .Where(p => !p.IsDelete);
            if (!string.IsNullOrWhiteSpace(search))
            {
                query = query.Where(p => p.Name.Contains(search));

            }

            ProductListDto productListDto = new();
            productListDto.TotalCount = query.Count();
            productListDto.CurrentPage= page;
               
            productListDto.Items =query.Skip((page-1)*2)
                .Take(2)
                .Select(p => new ProductListItemDto
            {

                Name = p.Name,
                CostPrice = p.CostPrice,
                SalePrice = p.SalePrice,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate,
                    Category = new()
                    {
                        Name= p.Category.Name,
                        Id= p.CategoryId,
                        ProductCount=p.Category.Products.Count(),

                    }


            }).ToList();

            List<ProductListItemDto> listItemDtos = new();


            return StatusCode(200, productListDto);
            //return Ok(products);
        }

        [HttpGet("{id}")]
        public IActionResult GetOne(int id)
        {
            Product product = _appDbContext.Products
                .Include(p=>p.Category)
                 .Where(p => !p.IsDelete)
                 .FirstOrDefault(p => p.Id == id);
            if (product == null) return StatusCode(StatusCodes.Status404NotFound);
        

            return Ok(MapToProductReturnDto(product));
            //return StatusCode(200, products[0]);
        }

        private static ProductReturnDto MapToProductReturnDto(Product product)
        {
            ProductReturnDto productReturnDto = new()
            {
                Name = product.Name,
                SalePrice = product.SalePrice,
                CostPrice = product.CostPrice,
                CreateDate = product.CreateDate,
                UpdateDate = product.UpdateDate,
                Category = new()
                {
                    Id = product.CategoryId,
                    Name = product.Category.Name
                }

            };
            return productReturnDto;
        }
      
        [HttpPost]
        public IActionResult AddProduct(ProductCreateDto productCreateDto)
        {
            var category=_appDbContext.Categories
                .Where(c=>!c.IsDeleted).FirstOrDefault(c=>c.Id==productCreateDto.CategoryId);
            if (category == null) return StatusCode(StatusCodes.Status404NotFound);
           
            Product newProduct = new()
            {
                Name = productCreateDto.Name,
                SalePrice = productCreateDto.SalePrice,
                CostPrice = productCreateDto.CostPrice,
                IsActive = productCreateDto.IsActive,
                IsDelete= productCreateDto.IsDelete,
                CategoryId= category.Id,
            };

            _appDbContext.Products.Add(newProduct);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var product = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            _appDbContext.Products.Remove(product);
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);

        }
        [HttpPut("{id}")]
        public IActionResult Update(int id, ProductUpdateDto productUpdate)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.Name = productUpdate.Name;
            existProduct.SalePrice = productUpdate.SalePrice;
            existProduct.CostPrice = productUpdate.CostPrice;
            existProduct.IsActive = productUpdate.IsActive;
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }
        [HttpPatch]
        public IActionResult ChangeStatus(int id, bool IsActive)
        {
            var existProduct = _appDbContext.Products.FirstOrDefault(p => p.Id == id);
            if (existProduct == null) return NotFound();
            existProduct.IsActive = IsActive;
            _appDbContext.SaveChanges();
            return StatusCode(StatusCodes.Status204NoContent);
        }

    }
}
