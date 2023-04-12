namespace FirstApii.Models
{
    public class Category:BaseEntity
    {
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public string Desc{ get; set; }
        public string ImageUrl{ get; set; }
        public List<Product> Products { get; set; }
    }
}
