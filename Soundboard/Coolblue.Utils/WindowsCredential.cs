using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CredentialManagement;

namespace Coolblue.Utils
{
    /// <summary>
    /// Interface for obtaining credentials (username and password) from credential managers
    /// </summary>
    public interface ICredential
    {
        string Username { get; }
        string Password { get; }
    }

    /// <summary>
    /// Credential (username and password) obtained from the Windows credential manager
    /// </summary>
    public class WindowsCredential : ICredential
    {
        /// <summary>
        /// Obtains the username and password for a credential with the given name
        /// </summary>
        /// <param name="credentialName">The name of the credential to obtain</param>
        public WindowsCredential(string credentialName)
        {
            var credential = new Credential { Target = credentialName };
            if (credential.Exists() && credential.Load())
            {
                Username = credential.Username;
                Password = credential.Password;
            }
            else
                throw new ArgumentException(string.Format("Credential with name {0} does not exist", credentialName));
        }

        /// <summary>
        /// The credential's username
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// The credential's password
        /// </summary>
        public string Password { get; private set; }
    }
}
