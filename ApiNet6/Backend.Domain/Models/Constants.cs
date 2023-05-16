namespace Backend.Domain.Models
{
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
            var audiance = "live_url_value";
#if DEBUG
            audiance = "https://localhost:32770";
#endif
            return audiance;
        }

        private String GetSecret()
        {
            var secret = "Some_RE@lly_RAndom_loooooong_q3ry_secrid_key";
#if DEBUG
            secret = "DEBUG_ONLY_Some_RE@lly_RAndom_loooooong_q3ry_secrid_key";
#endif
            return secret;
        }

        private String GetDefaultPassword()
        {
            var password = "8Oay5yvWoOs4ha4woQKKdQ==";
#if DEBUG
            password = "8Oay5yvWoOs4ha4woQKKasd==";
#endif
            return password;
        }
    }
}
