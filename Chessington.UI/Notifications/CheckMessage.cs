namespace Chessington.UI.Notifications
{
    public class CheckMessage
    {
        public CheckMessage(string message)
        {
            Message = message;
        }
        
        public string Message { get; set; }
    }
}