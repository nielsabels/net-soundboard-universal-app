using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using Prism.Unity.Windows;
using Soundboard.Services;

namespace Soundboard
{
    public class Bootstrapper
    {
        public static void Configure(IUnityContainer unityContainer)
        {
            unityContainer.RegisterType<ISoundService, SoundService>();
        }
    }
}
