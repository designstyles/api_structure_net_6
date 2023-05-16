using Backend.Domain.Common;
using Backend.Domain.Models;
using CoreCrpytoNet6.Public;
using static Backend.Domain.Common.Enums;

namespace Backend.DataAccess
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
        /// <summary>
        /// Used by admin to "fake"log in as user, and verify a reported bug 
        /// Used by admin to Migrate data from old system without knowing user passwords
        /// </summary>
        /// <param name="vmPassword"></param>
        /// <returns></returns>
        public bool MatchesDefault(string vmPassword)
        {
            return _crypto.EncryptString(vmPassword.ToLowerInvariant()) == _constants.DefaultPassword;
        }
        /// <summary>
        /// validate users password against saved one
        /// </summary>
        /// <param name="vmPassword"></param>
        /// <param name="dbPassword"></param>
        /// <returns></returns>
        public bool IsMatch(string dbPassword, string vmPassword)
        {
            return _crypto.EncryptString(vmPassword.ToLowerInvariant()) == dbPassword;
        }
        /// <summary>
        /// If user is super admin, then bypass validation (only for brent, to log in with elevated privs)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsUserSuperAdmin(Enums.MembershipType model)
        {
            return model == MembershipType.SysAdmin;
        }

    }

}
