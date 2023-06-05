namespace BlogAPI.Profiles
{
    public class BlogProfile : Profile
    {
        public BlogProfile()
        {
            CreateMap<Blog, BlogResponseDto>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => src.Id)
                )
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content)
                )
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.UserId)
                )
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => src.CreatedAt)
                )
                .ForMember(
                    dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now)
                );

            CreateMap<BlogRequestDto, Blog>()
                .ForMember(
                    dest => dest.Id,
                    opt => opt.MapFrom(src => Guid.NewGuid())
                )
                .ForMember(
                    dest => dest.Title,
                    opt => opt.MapFrom(src => src.Title)
                )
                .ForMember(
                    dest => dest.Content,
                    opt => opt.MapFrom(src => src.Content)
                )
                .ForMember(
                    dest => dest.CreatedAt,
                    opt => opt.MapFrom(src => DateTime.Now)
                )
                .ForMember(
                    dest => dest.UpdatedAt,
                    opt => opt.MapFrom(src => DateTime.Now)
                );
            
        }
    }
}