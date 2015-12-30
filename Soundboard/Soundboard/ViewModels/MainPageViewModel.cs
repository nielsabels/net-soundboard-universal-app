using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Media.Playback;
using Windows.Storage;
using Windows.System.Power.Diagnostics;
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
        private readonly IAudioPlayer _audioPlayer;
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

        public MainPageViewModel(ISoundService soundService, IAudioPlayer audioPlayer)
        {
            _audioPlayer = audioPlayer;
            Sounds = new ObservableCollection<Sound>(soundService.GetAllSounds());

            PlaySoundCommand = DelegateCommand<Sound>.FromAsyncHandler(PlaySound);
        }

        public async Task PlaySound(Sound sound)
        {
            StorageFile file3 = await StorageFile.GetFileFromPathAsync(@"C:\Users\Niels\AppData\Local\Packages\cfbc173b-fc00-49bc-9872-222ec4118afa_y3255gwp3wtsr\LocalState\test2.mp3");
            BackgroundMediaPlayer.Current.SetFileSource(file3);
            BackgroundMediaPlayer.Current.Play();
           
        }
    }
}
