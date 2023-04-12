using FirstApii.Models;

namespace FirstApii.Dtos.ProductDtos
{
    public class ProductListDto
    {
        public int TotalCount { get; set; }
        public int CurrentPage{ get; set; }
        public List<ProductListItemDto> Items { get; set; }
    }
}
