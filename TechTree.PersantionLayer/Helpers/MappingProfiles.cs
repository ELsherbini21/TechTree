using AutoMapper;
using TechTree.DAL.Models;
using TechTree.PersantionLayer.ViewModels;

namespace TechTree.PersantionLayer.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            CreateMap<CategoryItem, CategoryItemViewModel>().ReverseMap();

            CreateMap<Content, ContentViewModel>().ReverseMap();

            CreateMap<MediaType, MediaTypeViewModel>().ReverseMap();

            CreateMap<UserCategory, UserCategoryViewModel>().ReverseMap();

            CreateMap<Email, EmailViewModel>().ReverseMap();
        }
    }
}
