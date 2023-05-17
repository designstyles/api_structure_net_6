using Backend.Domain.Common;
using Backend.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Converters;
using System.IdentityModel.Tokens.Jwt;

namespace Backend.DataAccess
{
    /// <summary>
    /// Things to note:
    /// This entire code logic is done purposfully outside of the Host application. 
    /// JWT Validation logic can be placed within the program.cs class, however I tend to move it out 
    /// into its own dll. DataAccess fits nicely, as it is the entry point to gaining access to our system.
    /// </summary>
    public class ValidateToken
    {
        public async Task<TokenData?> ValidateKey(string tokenModel)
        {
            var constants = new Constants();
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(constants.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var keys = new List<SecurityKey>() { securityKey };

            //http://stackoverflow.com/questions/18223868/how-to-encrypt-jwt-security-token
            var tokenHandler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters()
            {
                ValidIssuer = constants.Issuer,
                ValidAudiences = new[] { constants.Issuer, },
                IssuerSigningKeys = keys
            };

            try
            {
                var tokenValidationResult = await tokenHandler.ValidateTokenAsync(tokenModel, parameters);

                if (tokenValidationResult.IsValid)
                {
                    var claim = tokenValidationResult.Claims.FirstOrDefault(x => x.Key == "InternalData");
                    if (claim.Value != null)
                    {
                        var settings = new Newtonsoft.Json.JsonSerializerSettings()
                        {
                            Converters =
                            {
                                new StringEnumConverter()
                            },
                            NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                            TypeNameHandling = Newtonsoft.Json.TypeNameHandling.All,
                        };
                        var tokenData = Newtonsoft.Json.JsonConvert.DeserializeObject<TokenData>(claim!.Value!.ToString(), settings);
                        if (tokenData != null && tokenData?.ProfileId.ToString() != "00000000-0000-0000-0000-000000000000")
                        {
                            return new TokenData()
                            {
                                IsActive = tokenData!.IsActive,
                                ProfileId = tokenData.ProfileId,
                                MembershipType = tokenData.MembershipType,
                            };
                        }
                    }
                }
                throw new Exception("Token validation failed");
                return null;
            }
            catch (Exception ex)
            {
                // log error
                //return null;
                throw new Exception("Token validation failed");
            }
        }


    }
}