using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Services
{
    public interface IAuthService
    {
        Task<JsonResponseDto<LoginResponseDto>> Login(LoginRequestDto userLogin);
        Task<JsonResponseDto<RegisterResponseDto>> Register(RegisterRequestDto userRegister);
    }
}