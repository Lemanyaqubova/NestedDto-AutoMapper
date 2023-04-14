using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace FirstApii.Dtos.UserDtos
{
    public class LoginDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(r => r.UserName).NotEmpty().MinimumLength(5).MaximumLength(20);
            RuleFor(r => r.Password).NotEmpty().MinimumLength(6).MaximumLength(15);
        }
    }
  
   
}
