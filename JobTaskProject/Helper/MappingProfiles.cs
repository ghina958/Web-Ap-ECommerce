using AutoMapper;
using JobTaskProject.Dto;
using JobTaskProject.Models;

namespace JobTaskProject.Helper
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<Category, CategoryDto>();
            CreateMap<CategoryDto, Category>();

            CreateMap<Product, ProductDto>();
            CreateMap<ProductDto, Product>();
        
        
        
        
        }
    }
}
