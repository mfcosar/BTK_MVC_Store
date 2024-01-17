using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreApp.Infrastructure.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {

            CreateMap<ProductDtoForInsertion, Product>();
            CreateMap<ProductDtoForUpdate, Product>().ReverseMap(); //Hem Dto'dan product'a, hem product'tan Dto'a geçiş gerekli
            CreateMap<UserDtoForCreation, IdentityUser>();
            CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap();
            CreateMap<CategoryDtoForInsertion, Category>();
            CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();



        }
    }
}
