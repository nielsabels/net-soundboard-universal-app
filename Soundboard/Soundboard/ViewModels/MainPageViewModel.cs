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
                Task.Run(() => PlaySound(SelectedSound));
            }
        }

        public MainPageViewModel(ISoundService soundService)
        {
            Sounds = new ObservableCollection<Sound>(soundService.GetAllSounds());

            PlaySoundCommand = DelegateCommand<Sound>.FromAsyncHandler(PlaySound);
        }

        public async Task PlaySound(Sound sound)
        {
            //StorageFile file3 = await StorageFile.GetFileFromPathAsync(@"C:\Users\Niels\AppData\Local\Packages\cfbc173b-fc00-49bc-9872-222ec4118afa_y3255gwp3wtsr\LocalState\test2.mp3");
            StorageFile file3 = await StorageFile.GetFileFromPathAsync(@"http://hcmaslov.d-real.sci-nnov.ru/public/mp3/Elvis%20Presley/Elvis%20Presley%20'A%20Big%20Hunk%20Of%20Love'.mp3");
            
            BackgroundMediaPlayer.Current.SetFileSource(file3);
            BackgroundMediaPlayer.Current.Play();
        }
    }
}
