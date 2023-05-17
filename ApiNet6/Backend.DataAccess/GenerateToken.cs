using Backend.Domain.Common;
using Backend.Domain.Models;
using Backend.Domain.Models.v1.dto;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;

namespace Backend.DataAccess
{
    /// <summary>
    /// Things to note:
    /// This entire code logic is done purposfully outside of the Host application. 
    /// JWT Validation logic can be placed within the program.cs class, however I tend to move it out 
    /// into its own dll. DataAccess fits nicely, as it is the entry point to gaining access to our system.
    /// </summary>
    public class GenerateToken
    {
        public static AuthenticatedUser_Dto Generate(string email, string password)
        {
            // This would be extracted from the db, by unique value of email address
            var profileInfo = new
            {
                IsActive = email.Contains("test"),
                EncryptedPassword = "j6IIEEqf6Nyja2o+NJK2Fg==", // test
                ProfileId = Guid.NewGuid(),
                MembershipType = email.Contains("gen") ? Enums.MembershipType.GeneralUser : Enums.MembershipType.Free,
                DisplayName = "Test User",
                Email = email,
            };

            if (profileInfo == null)
            {
                throw new ValidationException("Invalid email address provided");
            }

            if (profileInfo.IsActive == false)
            {
                throw new ValidationException("Account is not active");
            }


            if (ValidatePassword(profileInfo, password) == false)
            {
                throw new ValidationException("Password did not validate");
            }

            var model = new TokenData()
            {
                ProfileId = profileInfo.ProfileId,
                IsActive = profileInfo.IsActive,
                MembershipType = profileInfo.MembershipType,
            };

            var dataModel = JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented,
           new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });

            var claims = new[]
            {
                new Claim("InternalData", dataModel),
            };

            var consts = new Constants();
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(consts.Secret));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
               consts.Issuer,
               consts.Issuer,
               claims,
               notBefore: DateTime.Now,
               expires: DateTime.Now.AddYears(5),
               signingCredentials);

            return new AuthenticatedUser_Dto(new JwtSecurityTokenHandler().WriteToken(token),
                profileInfo.DisplayName,
                profileInfo.MembershipType.ToString());
        }

        private static bool ValidatePassword(dynamic dbProfileExtention, string password)
        {
            var pwValidator = new InteralSecurity();

            if (pwValidator.PasswordEncryption.IsMatch(dbProfileExtention.EncryptedPassword, password))
            {
                // user is valid, and used their password
                return true;
            }

            if (pwValidator.PasswordEncryption.IsUserSuperAdmin(dbProfileExtention.MembershipType))
            {
                // user is valid, and is super admin (brent or graham)
                return true;
            }

            if (pwValidator.PasswordEncryption.MatchesDefault(password))
            {
                // user is valid, and is super admin (brent or graham)
                return true;
            }

            return false;
        }
    }
}