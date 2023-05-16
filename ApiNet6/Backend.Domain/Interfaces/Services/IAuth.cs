using Backend.Domain.Common;
using Backend.Domain.Models.v1.dto;

namespace Backend.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task<TokenData?> ValidateKey(string tokenModel);
        AuthenticatedUser_Dto Generate(string email, string password);
    }
}
