using JustSaying.Models;

namespace MessageProcessingService
{
    public class GenericMessage : Message
    {
        public GenericMessage(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
