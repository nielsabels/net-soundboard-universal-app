using Microsoft.Practices.Unity;
using System.Web.Http;
using Unity.WebApi;

namespace Soundboard.AzureApiApp
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();
            
            Bootstrapper.Configure(container);
            
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}