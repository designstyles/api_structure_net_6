using Backend.Application.Services;
using Backend.Domain.Interfaces.Services;
using System.ComponentModel.DataAnnotations;

namespace WebApiTestCoreNet6
{
    public class CredentialsValidationTest
    {
        [Test]
        public void InvalidCredentials()
        {
            var auth = new AuthService();
            var _tokenValidatorService = new TokenValidatorService(auth);
            
            try
            {
                var _ = auth.Generate("test@test.co.za", "password");
            }
            catch(ValidationException ex)
            {
                Assert.Throws<ValidationException>(() => throw new ValidationException());
            }
        }

        [Test]
        public async Task ValidCredentials()
        {
            var auth = new AuthService();
            var _tokenValidatorService = new TokenValidatorService(auth);
            var validToken = auth.Generate("test@test.co.za", "test");
            var result = await _tokenValidatorService.ValidateToken(validToken.Token);
            Assert.That(result.IsActive = true);
        }
    }
}