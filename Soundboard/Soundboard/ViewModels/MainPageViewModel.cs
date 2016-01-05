using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.System.Power.Diagnostics;
using Windows.UI.Xaml.Controls;
using NAudio.Wave;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Windows.Navigation;
using Soundboard.Dto;
using Soundboard.Services;

namespace Soundboard.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ObservableCollection<Sound> Sounds { get; private set; }

        public DelegateCommand<Sound> PlaySoundCommand { get; set; }

        private Sound _selectedSound;
        public Sound SelectedSound
        {
            get
            {
                return _selectedSound;
            }
            set
            {
                SetProperty(ref _selectedSound, value);
                PlaySound(SelectedSound);
            }
        }

        public MediaElement MePlayer { get; set; }

        public MainPageViewModel(ISoundService soundService)
        {
            Sounds = new ObservableCollection<Sound>(soundService.GetAllSounds());

            PlaySoundCommand = new DelegateCommand<Sound>(PlaySound);
        }

        public void PlaySound(Sound sound)
        {   
            MePlayer.Source = new Uri(sound.SoundUri);
        }
    }
}
