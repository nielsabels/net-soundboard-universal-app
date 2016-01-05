using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;

namespace Soundboard.AzureApiApp.Repositories
{
    public interface ISoundboardRepository
    {
        
    }

    public class SoundboardRepository : ISoundboardRepository 
    {
        public SoundboardRepository(DocumentClient documentClient)
        {

        }
    }
}
