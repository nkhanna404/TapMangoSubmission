namespace SmsRateLimiterService.Models
{
    public class MessageRequest
    {
        public string BusinessPhoneNumber { get; set; }
        public string MessageContent { get; set; }
    }
}
