using OnlineCassino.Domain;
using OnlineCassino.WebApi.DTOs;

namespace OnlineCassino.WebApi
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((config) =>
            {
                config.CreateMap<Game, GameDto>().ReverseMap();
                config.CreateMap<GameCollection, GameCollectionDto>().ReverseMap();
                config.CreateMap<DeviceType, DeviceTypeDto>().ReverseMap();
            });
        }
    }
}