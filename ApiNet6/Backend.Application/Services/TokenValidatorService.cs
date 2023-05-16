using Backend.Domain.Common;
using Backend.Domain.Interfaces.Services;

namespace Backend.Application.Services
{
    public class TokenValidatorService : ITokenValidatorService
    {
        private readonly IAuthService _authService;

        public TokenValidatorService(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<TokenData> ValidateToken(string providedToken)
        {
            return await _authService.ValidateKey(providedToken);
        }

    }
}