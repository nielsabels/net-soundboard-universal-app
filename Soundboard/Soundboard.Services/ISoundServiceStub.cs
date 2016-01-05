using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundboard.Dto;

namespace Soundboard.Services
{
    public interface ISoundServiceStub
    {
        List<Sound> GetAllSounds();
    }

    public class SoundServiceStub : ISoundServiceStub
    {
        public List<Sound> GetAllSounds()
        {
            var sounds = new List<Sound>()
            {
                new Sound()
                {
                    DisplayName = "He meisje",
                    PictureUri = "http://cbsnews2.cbsistatic.com/hub/i/r/2011/07/25/df3a8530-a644-11e2-a3f0-029118418759/thumbnail/620x350/0cb41f489b87587b34042d9793a7cab7/red_kangaroo.jpg",
                    SoundUri = "https://dl.dropboxusercontent.com/u/256165/Pat/pat%20-%20hoi%20meisje.mp3",
                },
                new Sound()
                {
                    DisplayName = "Ik heb een mop",
                    PictureUri = "http://strengthandconditioningfitness.com/wp-content/uploads/2011/04/Kangaroos-jump-high-1.jpg",
                    SoundUri = "https://dl.dropboxusercontent.com/u/256165/Pat/pat%20-%20twee%20tieten%20in%20een%20envelop.mp3"
                },
                new Sound()
                {
                    DisplayName = "Godverdomme man",
                    PictureUri = "http://wdtprs.com/blog/wp-content/uploads/2015/06/KOALA.jpg",
                    SoundUri = "https://dl.dropboxusercontent.com/u/256165/Pat/pat%20-%20godverdomme%20man.mp3",
                }

            };

            return sounds;
        }
    }
}
