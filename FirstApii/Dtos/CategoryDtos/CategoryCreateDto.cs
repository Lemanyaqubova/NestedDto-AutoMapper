using FirstApii.Dtos.ProductDtos;
using FluentValidation;

namespace FirstApii.Dtos.CategoryDtos
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public IFormFile Photo{ get; set; }
       
        public bool IsActive { get; set; }
        
    }
    public class CategoryCreateDtoValidator : AbstractValidator<CategoryCreateDto>
    {
        public CategoryCreateDtoValidator()
        {
            RuleFor(p => p.Name)
                .MaximumLength(50).WithMessage("50den boyuk ola bilmez")
                .NotNull().WithMessage("bosh qoyma");
            RuleFor(p => p.Desc)
               .MaximumLength(300).WithMessage("50den boyuk ola bilmez")
               .NotNull().WithMessage("bosh qoyma");


            RuleFor(p => p.IsActive)
               .Equal(true).WithMessage("true olmalidir")
               .NotNull().WithMessage("bosh qoymag olmaz");

            //RuleFor(p => p)
            //    .Custom((p, context) =>
            //    {
            //        if (p.SalePrice < p.CostPrice)
            //        {
            //            context.AddFailure("SalePrice", "SalePrice CostPrice dan kicik ola bilmez");
            //        }
            //    });

        }
    }
}