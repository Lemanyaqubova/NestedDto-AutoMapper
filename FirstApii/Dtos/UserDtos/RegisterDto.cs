using FluentValidation;

namespace FirstApii.Dtos.UserDtos
{
    public class RegisterDto
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
    }
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.FullName).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(r => r.UserName).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6).MaximumLength(15);
            RuleFor(r => r.RePassword).NotEmpty().MinimumLength(6).MaximumLength(15);
            RuleFor(u => u).Custom((u, context) =>
            {
                if (u.Password!=u.RePassword)
                {
                    context.AddFailure("Password", "Does not match");
                }
            });
        }
    }
}
