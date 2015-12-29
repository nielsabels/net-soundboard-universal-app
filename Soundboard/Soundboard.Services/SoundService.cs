using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Soundboard.Dto;

namespace Soundboard.Services
{
    public interface ISoundService
    {
        List<Sound> GetAllSounds();
    }

    public class SoundService : ISoundService
    {
        public List<Sound> GetAllSounds()
        {
            var sounds = new List<Sound>()
            {
                new Sound()
                {
                    DisplayName = "He meisje",
                    PictureUri = "http://cbsnews2.cbsistatic.com/hub/i/r/2011/07/25/df3a8530-a644-11e2-a3f0-029118418759/thumbnail/620x350/0cb41f489b87587b34042d9793a7cab7/red_kangaroo.jpg"
                },
                new Sound()
                {
                    DisplayName = "Ik heb een mop",
                    PictureUri = "http://strengthandconditioningfitness.com/wp-content/uploads/2011/04/Kangaroos-jump-high-1.jpg"
                },
            };

            return sounds;
        }
    }
}
