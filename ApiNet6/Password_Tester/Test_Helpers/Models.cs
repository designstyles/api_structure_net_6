namespace Password_Tester.Test_Helpers
{
    public static class Enums
    {
        public enum MembershipType
        {
            Free = -1,
            GeneralUser = 1,
            SysAdmin = 2,
        }
    }

    public class Constants
    {
        public Constants()
        {
            Issuer = GetAudiance();
            Secret = GetSecret();
            DefaultPassword = GetDefaultPassword();
        }
        public String Issuer { get; private set; } = string.Empty;
        public String Secret { get; private set; } = string.Empty;
        public String DefaultPassword { get; private set; } = string.Empty;

        public String CrpytoKey { get; private set; } = "This!!!_cryptokey-H@Sh_for-Salting";

        private String GetAudiance()
        {
            return "https://localhost:32770";
        }

        private String GetSecret()
        {
            return "DEBUG_ONLY_Some_RE@lly_RAndom_loooooong_q3ry_secrid_key";

        }

        private String GetDefaultPassword()
        {
            return "8Oay5yvWoOs4ha4woQKKasd==";

        }
    }

}
