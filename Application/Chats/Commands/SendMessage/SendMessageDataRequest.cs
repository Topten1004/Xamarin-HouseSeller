namespace Immowert4You.Application.Chats.Commands.SendMessage
{
    public class SendMessageDataRequest
    {
        public SendMessageDataRequest(string text)
        {
            Text = text;
        }

        public string Text { get; }
    }
}
