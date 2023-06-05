namespace BlogAPI.Services
{

    public class BlogService : IBlogService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        public BlogService(DataContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;

        }

        public async Task<JsonResponseDto<List<BlogResponseDto>>> GetAllBlogs()
        {
            var jsonResponse = new JsonResponseDto<List<BlogResponseDto>>();

            var blogs = await _context.Blogs.ToListAsync();
            var blogResponseDto = _mapper.Map<List<BlogResponseDto>>(blogs);


            jsonResponse.Data = blogResponseDto;
            jsonResponse.Message = "Blogs retrieved successfully.";


            return jsonResponse;
        }

        public async Task<JsonResponseDto<BlogResponseDto>> CreateBlog(BlogRequestDto blogRequest, HttpContext httpContext)
        {
            var jsonResponse = new JsonResponseDto<BlogResponseDto>();

            var blogMapper = _mapper.Map<Blog>(blogRequest);

            blogMapper.UserId = GetCurrentUser(httpContext);

            var blog = await _context.Blogs.AddAsync(blogMapper);
            await _context.SaveChangesAsync();

            jsonResponse.Data = _mapper.Map<BlogResponseDto>(blog.Entity);
            jsonResponse.Message = "You created blog successfully.";

            return jsonResponse;
        }

        public async Task<JsonResponseDto<BlogResponseDto>> GetBlogById(Guid id)
        {
            var jsonResponse = new JsonResponseDto<BlogResponseDto>();

            var blog = await _context.Blogs.FindAsync(id);

            if (blog is null) {
                jsonResponse.Message = "Blog does not exists.";
                return jsonResponse;
            }

            jsonResponse.Data = _mapper.Map<BlogResponseDto>(blog);
            jsonResponse.Message = "Blog retrieved successfully.";

            return jsonResponse;
        }

        public async Task<JsonResponseDto<BlogResponseDto>> UpdateBlogById(Guid id, BlogRequestDto blogRequest, HttpContext httpContext)
        {
            var jsonResponse = new JsonResponseDto<BlogResponseDto>();

            var checkedBlog = await _context.Blogs.FindAsync(id);

            if (checkedBlog is null) {
                jsonResponse.Message = "Blog does not exists.";
                return jsonResponse;
            } 

            var currentUser = GetCurrentUser(httpContext);

            if (currentUser != checkedBlog.UserId) {
                jsonResponse.Message = "You do not have an access to edit this blog.";
                return jsonResponse;
            }

            checkedBlog.Title = blogRequest.Title;
            checkedBlog.Content = blogRequest.Content;
            checkedBlog.UpdatedAt = DateTime.Now;

            jsonResponse.Data = _mapper.Map<BlogResponseDto>(checkedBlog);
            jsonResponse.Message = "You have updated a blog successfully.";

            return jsonResponse;
        }

        public async Task<JsonResponseDto<BlogResponseDto>> DeleteBlogById(Guid id, HttpContext httpContext)
        {
            var jsonResponse = new JsonResponseDto<BlogResponseDto>();

            var checkedBlog = await _context.Blogs.FindAsync(id);

            if (checkedBlog is null) {
                jsonResponse.Message = "Blog does not exists.";
                return jsonResponse;
            } 

            var currentUser = GetCurrentUser(httpContext);

            if (currentUser != checkedBlog.UserId) {
                jsonResponse.Message = "You do not have an access to delete this blog.";
                return jsonResponse;
            }

            jsonResponse.Message = "You have deleted a blog successfully.";

            return jsonResponse;
        }

        private Guid GetCurrentUser(HttpContext httpContext) {
            var userId = httpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? string.Empty;
            Guid guid;
            Guid.TryParse(userId, out guid);
            return guid;
        }

        private bool ValidateBlogRequest(BlogRequestDto blogRequest)
        {
            var validationContext = new ValidationContext(blogRequest);
            var validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(blogRequest, validationContext, validationResults, true);
            return isValid;
        }

        
    }
}