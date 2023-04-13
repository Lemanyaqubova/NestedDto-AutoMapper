using AutoMapper;
using FirstApii.Dtos.CategoryDtos;
using FirstApii.Models;

namespace FirstApii.Profiles
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<Category, CategoryReturnDto>();
        }
    }
}
