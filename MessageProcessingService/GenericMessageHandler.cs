using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.CloudSearchDomain;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using JustSaying.Messaging.MessageHandling;
using Newtonsoft.Json;
using NLog;

namespace MessageProcessingService
{
    public class GenericMessageHandler : IHandlerAsync<GenericMessage>
    {
        private readonly Logger _logger;

        public GenericMessageHandler()
        {
            _logger = LogManager.GetCurrentClassLogger();
        }

        public async Task<bool> Handle(GenericMessage message)
        {
            _logger.Info($"Received message: {message}");
            try
            {
                var s3 = new AmazonS3Client(new BasicAWSCredentials("accessKey", "secretKey"), RegionEndpoint.EUWest1);
                var putRequest = new PutObjectRequest { ContentBody = JsonConvert.SerializeObject(message), BucketName = "kristianbrimbledev-genericbucket", Key = "latestMessage.json", ContentType = ContentType.ApplicationJson };
                await s3.PutObjectAsync(putRequest).ConfigureAwait(false);
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(e, "Failed to save message to S3");
                return false;
            }
        }
    }
}
