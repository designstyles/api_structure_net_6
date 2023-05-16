using Backend.Domain.Common;

namespace Backend.Domain.Models.v1.dto
{
    public class AuthenticatedUser_Dto
    {
        public AuthenticatedUser_Dto(string token, string displayName, string profileType)
        {
            Token = token;
            DisplayName = displayName;
            ProfileType = profileType;
        }

        public string Token { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string ProfileType { get; set; } = Enums.MembershipType.Free.ToString();
    }
}
