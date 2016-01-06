using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Soundboard.AzureApiApp.Dto;

namespace Soundboard.AzureApiApp.Repositories
{
    public interface ISoundboardRepository
    {
        Task<List<Sound>> GetAllSounds();
    }

    public class SoundboardRepository : ISoundboardRepository 
    {
        private readonly DocumentClient _documentClient;

        public SoundboardRepository(DocumentClient documentClient)
        {
            _documentClient = documentClient;
        }

        public Task<List<Sound>> GetAllSounds()
        {
            List<Sound> sounds = new List<Sound>();

            var db = _documentClient.CreateDatabaseQuery()
                .Where(d => d.Id == "Sounds")
                .AsEnumerable()
                .FirstOrDefault();

            var col = _documentClient.CreateDocumentCollectionQuery(db.SelfLink)
                  .Where(c => c.Id == "Documents")
                  .AsEnumerable()
                  .FirstOrDefault();

            var feed = _documentClient
                .CreateDocumentQuery<Sound>(col.SelfLink, new FeedOptions() { MaxItemCount = 10 })
                .AsDocumentQuery();

            while (feed.HasMoreResults)
            {
                foreach (Sound sound in feed.ExecuteNextAsync().Result)
                {
                    Console.WriteLine("5. Sound Name is - {0}", sound.Name);
                    sounds.Add(sound);
                }
            }

            return Task.FromResult(sounds);
        }
    }
}
