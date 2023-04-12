namespace FirstApii.Dtos.ProductDtos
{
    public class ProductListItemDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public CategoryInProductListItemDto Category { get; set; }
    }
    public class CategoryInProductListItemDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int ProductCount { get; set; }
    }
}
