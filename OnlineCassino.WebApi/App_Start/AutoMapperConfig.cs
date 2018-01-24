namespace OnlineCassino.WebApi.App_Start
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((config) =>
            {
                //config.CreateMap<Topic, TopicViewModel>().ReverseMap();
                //config.CreateMap<Post, PostViewModel>().ReverseMap();
                //config.CreateMap<User, UserViewModel>().ReverseMap();
            });
        }
    }
}