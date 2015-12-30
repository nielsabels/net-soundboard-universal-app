using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Power.Diagnostics;
using Prism.Mvvm;
using Prism.Windows.Navigation;
using Soundboard.Dto;
using Soundboard.Services;

namespace Soundboard.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ObservableCollection<Sound> Sounds { get; private set; }

        private Sound _selectedSound;
        public Sound SelectedSound
        {
            get { return _selectedSound; }
            set
            {
                SetProperty(ref _selectedSound, value);
                PlaySound(SelectedSound);
            }
        }

        public MainPageViewModel(ISoundService soundService)
        {
            Sounds = new ObservableCollection<Sound>(soundService.GetAllSounds());
        }

        public void PlaySound(Sound sound)
        {
           
        }
    }
}
