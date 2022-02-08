using AutoMapper;

namespace ShopManager5.Api.Data
{
    public class MappingProfile :Profile 
    {
        public MappingProfile()
        {
            CreateMap<Models.Employee, Controllers.GuiDtoModels.DtoEmployee>();
            CreateMap<Controllers.GuiDtoModels.DtoEmployee, Models.Employee>();
        }
    }
}
