using Amazon.Runtime;
using JustSaying;
using JustSaying.AwsTools;
using NLog;

namespace MessageProcessingService
{
    public class GenericMessageService
    {
        private readonly IHaveFulfilledSubscriptionRequirements _bus;
        private readonly Logger _logger;

        public GenericMessageService()
        {
            _logger = LogManager.GetCurrentClassLogger();

            CreateMeABus.DefaultClientFactory = () =>
                new DefaultAwsClientFactory(new BasicAWSCredentials("accessKey", "secretKey"));

            _bus = CreateMeABus
                .InRegion("eu-west-1")
                .WithSnsMessagePublisher<GenericMessage>()
                .WithSqsTopicSubscriber("generic-message-topic")
                .IntoQueue("generic-message-queue")
                .WithMessageHandler(new GenericMessageHandler());

            _logger.Info("Bus created");
        }

        public void Start()
        {
            _bus.StartListening();
            _logger.Info("Now listening");
            PublishTestMessage();
        }

        private void PublishTestMessage()
        {
            _logger.Info("Publishing test message");
            _bus.Publish(new GenericMessage("Test message"));
        }

        public void Stop()
        {
            _bus.StopListening();
            _logger.Info("Stopped listening");
        }
    }
}