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
                new Sound() {DisplayName = "He meisje"},
                new Sound() {DisplayName = "Ik heb een mop"},
            };

            return sounds;
        }
    }
}
