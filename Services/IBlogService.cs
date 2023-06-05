namespace BlogAPI.Services
{
    public interface IBlogService
    {
        Task<JsonResponseDto<BlogResponseDto>> CreateBlog(BlogRequestDto blogRequest, HttpContext httpContext);
        Task<JsonResponseDto<List<BlogResponseDto>>> GetAllBlogs();
        Task<JsonResponseDto<BlogResponseDto>> GetBlogById(Guid id);
        Task<JsonResponseDto<BlogResponseDto>> UpdateBlogById(Guid id, BlogRequestDto blogRequest, HttpContext httpContext);
        Task<JsonResponseDto<BlogResponseDto>> DeleteBlogById(Guid id, HttpContext httpContext);
    }
}