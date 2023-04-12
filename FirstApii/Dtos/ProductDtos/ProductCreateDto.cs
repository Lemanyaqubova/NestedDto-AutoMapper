using FluentValidation;

namespace FirstApii.Dtos.ProductDtos
{
    public class ProductCreateDto
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public double SalePrice { get; set; }
        public double CostPrice { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }

    }
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.CategoryId)
                .NotNull()
                .WithMessage("bosh olmaz")
                .GreaterThanOrEqualTo(1);
            RuleFor(p => p.Name)
                .MaximumLength(50).WithMessage("50den boyuk ola bilmez")
                .NotNull().WithMessage("bosh qoyma");

            RuleFor(p => p.SalePrice)
                .GreaterThanOrEqualTo(0).WithMessage("0dan boyuk olmalidir")
                .NotNull().WithMessage("bosh qoymag olmaz");

            RuleFor(p => p.CostPrice)
               .GreaterThanOrEqualTo(0).WithMessage("0dan boyuk olmalidir")
               .NotNull().WithMessage("bosh qoymag olmaz");

            RuleFor(p => p.IsActive)
               .Equal(true).WithMessage("true olmalidir")
               .NotNull().WithMessage("bosh qoymag olmaz");

            RuleFor(p => p)
                .Custom((p, context) =>
                {
                    if (p.SalePrice<p.CostPrice)
                    {
                        context.AddFailure("SalePrice", "SalePrice CostPrice dan kicik ola bilmez");
                    }
                });
               
        }
    }
}
