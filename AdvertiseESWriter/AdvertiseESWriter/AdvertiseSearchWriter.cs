using System;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using Newtonsoft.Json;
using Nest;
using AdvertiseApi.Models;

[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]
namespace AdvertiseESWriter
{
    public class AdvertiseSearchWriter
    {
        public AdvertiseSearchWriter() : this(ElasticSearchWriter.GetInstance(ConfigHelper.Instance))
        {

        }

        private readonly IElasticClient _client;
        public AdvertiseSearchWriter(IElasticClient client)
        {
            _client = client;
        }
        public async Task Function(SNSEvent snsEvent, ILambdaContext context)
        {

            foreach (var record in snsEvent.Records)
            {
                context.Logger.LogLine(record.Sns.Message);

                var message = JsonConvert.DeserializeObject<ConfirmedAdvertisementMsg>(record.Sns.Message);
                var advertDocument = Mapper.Map(message);
                await _client.IndexDocumentAsync(advertDocument);

            }
        }
    }
}
