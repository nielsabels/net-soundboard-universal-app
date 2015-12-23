using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Windows.Navigation;

namespace Soundboard.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {
        public void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatingFrom(NavigatingFromEventArgs e, Dictionary<string, object> viewModelState, bool suspending)
        {
            throw new NotImplementedException();
        }
    }
}
