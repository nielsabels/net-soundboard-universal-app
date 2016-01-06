using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Soundboard.AzureApiApp.Repositories;
using Soundboard.AzureApiApp.Dto;

namespace Soundboard.AzureApiApp.Controllers
{
    public class SoundsController : ApiController
    {
        private readonly SoundboardRepository _soundboardRepository;

        public SoundsController(SoundboardRepository soundboardRepository)
        {
            _soundboardRepository = soundboardRepository;
        }

        public async Task<List<Sound>> Get()
        {
            return await _soundboardRepository.GetAllSounds();
        }
    }
}
