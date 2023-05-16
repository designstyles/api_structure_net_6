using CoreCrpytoNet6.Public;

namespace Password_Tester.Test_Helpers
{

    internal class InteralSecurity
    {
        public InteralSecurity()
        {
            PasswordEncryption = new Password();
        }
        public Password PasswordEncryption { get; private set; }

    }
    internal class Password
    {
        private readonly Crypto _crypto;
        private readonly Constants _constants;
        public Password()
        {
            _constants = new Constants();
            _crypto = new Crypto(new CryptoParams() { PassPhrase = _constants.CrpytoKey });
        }

        public string EncryptPassword(string newPassword)
        {
            return _crypto.EncryptString(newPassword.ToLowerInvariant());
        }
    }

}
