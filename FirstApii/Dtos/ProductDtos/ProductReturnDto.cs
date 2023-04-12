namespace FirstApii.Dtos.ProductDtos
{
    public class ProductReturnDto
    {
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public CategoryInProductReturnDto Category { get; set; }  
    }
    public class CategoryInProductReturnDto
    {
        public int ?Id { get; set; }
        public string Name { get; set; }
    }
}
