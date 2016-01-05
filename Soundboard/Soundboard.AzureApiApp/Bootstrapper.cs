using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Coolblue.Utils;
using Microsoft.Azure.Documents.Client;
using Microsoft.Practices.Unity;

namespace Soundboard.AzureApiApp
{
    public class Bootstrapper
    {
        public static void Configure(IUnityContainer unityContainer)
        {
            string serviceEndpoint = new WindowsCredential("soundboard_serviceendpoint").Password;
            string authKey = new WindowsCredential("soundboard_authkey").Password;

            unityContainer.RegisterInstance<DocumentClient>(
                new DocumentClient(
                    new Uri(serviceEndpoint), authKey)
                );
        }
    }
}