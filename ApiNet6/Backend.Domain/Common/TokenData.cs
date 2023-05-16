namespace Backend.Domain.Common
{
    public class TokenData
    {
        public bool IsActive { get; set; }
        public Guid ProfileId { get; set; }
        //public string MembershipType { get; set; }  
        public Enums.MembershipType MembershipType { get; set; }
    }

}