using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Mvvm;
using Prism.Windows.Navigation;
using Soundboard.Dto;
using Soundboard.Services;

namespace Soundboard.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ObservableCollection<Sound> Sounds { get; private set; } 

        public MainPageViewModel(ISoundService soundService)
        {
            Sounds = new ObservableCollection<Sound>(soundService.GetAllSounds());
        }

    }
}
