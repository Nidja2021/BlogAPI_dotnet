namespace BlogAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BlogController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBlogs() {
            return Ok(await _blogService.GetAllBlogs());
        }

        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateBlog([FromBody] BlogRequestDto blogRequest) {
            return Ok(await _blogService.CreateBlog(blogRequest, HttpContext));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBlog(Guid id) {
            return Ok(await _blogService.GetBlogById(id));
        }

        [HttpPatch("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBlog(Guid id, [FromBody] BlogRequestDto blogRequest) {
            return Ok(await _blogService.UpdateBlogById(id, blogRequest, HttpContext));
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlog(Guid id) {
            return Ok(await _blogService.DeleteBlogById(id, HttpContext));
        }
    }
}