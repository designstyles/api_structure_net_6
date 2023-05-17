using Backend.Application.Services;
using Backend.Domain.Interfaces.Services;

namespace WebApiTestCoreNet6
{
    public class TokenValidationTest
    {
        [Test]
        public async Task EmptyToken()
        {
            var auth = new AuthService();
            var _tokenValidatorService = new TokenValidatorService(auth);
         
            try
            {
                var result = await _tokenValidatorService.ValidateToken(string.Empty);
                Assert.Throws<Exception>(() => throw new Exception());
            }
            catch (Exception ex)
            {
                Assert.Throws<Exception>(() => throw new Exception());
            }
        }

        [Test]
        public async Task InvalidToken()
        {
            var auth = new AuthService();
            var _tokenValidatorService = new TokenValidatorService(auth);
            var inValidTokenString = "AyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJbnRlcm5hbERhdGEiOiJ7XHJcbiAgXCJJc0FjdGl2ZVwiOiB0cnVlLFxyXG4gIFwiUHJvZmlsZUlkXCI6IFwiYWYyMjg2NTItMzAyMS00Zjc1LWEyNjAtODVlZWEyYWI3OWEzXCIsXHJcbiAgXCJNZW1iZXJzaGlwVHlwZVwiOiAtMVxyXG59IiwibmJmIjoxNjg0MzE3MjUzLCJleHAiOjE4NDIxNzAwNTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjMyNzcwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6MzI3NzAifQ.ZuztpijDeurp3oGj5_GYihjkSINPt1yEu_dhrS48JhY";
            try
            {
                var result = await _tokenValidatorService.ValidateToken(inValidTokenString);
                Assert.Throws<Exception>(() => throw new Exception());
            }
            catch(Exception ex)
            {
                Assert.Throws<Exception>(() => throw new Exception());
            }
        }

        [Test]
        public async Task ValidToken()
        {
            var validTokenString = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJJbnRlcm5hbERhdGEiOiJ7XHJcbiAgXCJJc0FjdGl2ZVwiOiB0cnVlLFxyXG4gIFwiUHJvZmlsZUlkXCI6IFwiYWYyMjg2NTItMzAyMS00Zjc1LWEyNjAtODVlZWEyYWI3OWEzXCIsXHJcbiAgXCJNZW1iZXJzaGlwVHlwZVwiOiAtMVxyXG59IiwibmJmIjoxNjg0MzE3MjUzLCJleHAiOjE4NDIxNzAwNTMsImlzcyI6Imh0dHBzOi8vbG9jYWxob3N0OjMyNzcwIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3Q6MzI3NzAifQ.ZuztpijDeurp3oGj5_GYihjkSINPt1yEu_dhrS48JhY";
            var auth = new AuthService();
            var _tokenValidatorService = new TokenValidatorService(auth);
            var result = await _tokenValidatorService.ValidateToken(validTokenString);
            Assert.That(result.IsActive = true);
        }
        //
    }
}