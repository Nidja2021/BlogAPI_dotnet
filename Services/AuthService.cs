namespace BlogAPI.Services
{
    public class AuthService : IAuthService
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AuthService(DataContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<JsonResponseDto<RegisterResponseDto>> Register(RegisterRequestDto userRegister)
        {
            var jsonResponse = new JsonResponseDto<RegisterResponseDto>();

            var user = _mapper.Map<User>(userRegister);

            var checkUser = await _context.Users.FindAsync(user.Id);

            if (checkUser != null) {
                jsonResponse.Success = false;
                jsonResponse.Message = "User already exists.";
                return jsonResponse;
            }

            
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            jsonResponse.Data = _mapper.Map<RegisterResponseDto>(user);
            jsonResponse.Message = "You have registered successufully.";

            return jsonResponse;
        }

        public async Task<JsonResponseDto<LoginResponseDto>> Login(LoginRequestDto userLogin)
        {
            var jsonResponse = new JsonResponseDto<LoginResponseDto>();

            var user = await _context.Users.FirstOrDefaultAsync(user => user.Email == userLogin.Email);

            if (user is null) {
                jsonResponse.Success = false;
                jsonResponse.Message = "Wrong credentials.";
                return jsonResponse;
            }

            var checkPassword = BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password);

            if (!checkPassword) {
                jsonResponse.Success = false;
                jsonResponse.Message = "Wrong credentials.";
                return jsonResponse;
            }

            var loginResponseDto = new LoginResponseDto();
            loginResponseDto.AccessToken = GenerateJwtToken(user);


            jsonResponse.Data = loginResponseDto;
            jsonResponse.Message = "You have logged in successfully.";
            return jsonResponse;
        }

        public string GenerateJwtToken(User user) {

            var claims = new List<Claim> {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.roles.ToString())
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:SecretKey").Value!
            ));

            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256Signature
            );

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                issuer: _configuration.GetSection("Jwt:Issuer").Value!,
                audience: _configuration.GetSection("Jwt:Audience").Value!,
                signingCredentials: signingCredentials
            );

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }
    }
}