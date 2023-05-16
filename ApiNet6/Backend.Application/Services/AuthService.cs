using Backend.DataAccess;
using Backend.Domain.Common;
using Backend.Domain.Interfaces.Services;
using Backend.Domain.Models.v1.dto;

namespace Backend.Application.Services
{
    public class AuthService : IAuthService
    {
        public async Task<TokenData?> ValidateKey(string tokenModel)
        {
            return await new ValidateToken().ValidateKey(tokenModel);
        }

        /// <summary>
        /// Dto object with validated token and some additional properies displayed to a client
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public AuthenticatedUser_Dto Generate(string email, string password)
        {
            return GenerateToken.Generate(email, password);
        }

    }
}
