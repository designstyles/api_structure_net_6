using Backend.Domain.Common;

namespace Backend.Domain.Interfaces.Services
{
    public interface ITokenValidatorService
    {
        Task<TokenData> ValidateToken(string providedToken);
    }
}
