namespace SmsRateLimiterService.Services
{
    using System.Collections.Concurrent;

    public class RateLimiterService
    {
        private readonly int _perNumberLimit;
        private readonly int _accountLimit;
        private readonly ConcurrentDictionary<string, int> _numberMessageCounts = new();
        private int _accountMessageCount;

        public RateLimiterService(int perNumberLimit, int accountLimit)
        {
            _perNumberLimit = perNumberLimit;
            _accountLimit = accountLimit;
        }

        public bool CanSendMessage(string phoneNumber)
        {
            // Track how many messages have been sent by the phone number
            int currentCount = _numberMessageCounts.AddOrUpdate(phoneNumber, 1, (key, count) => count + 1);

            // Log the current count for the phone number
            Console.WriteLine($"Phone Number: {phoneNumber}, Current Count: {currentCount}, Per-Number Limit: {_perNumberLimit}");

            // Check if the phone number has exceeded the limit
            if (currentCount > _perNumberLimit)
            {
                Console.WriteLine($"Message limit exceeded for phone number: {phoneNumber}");
                return false;
            }

            // Log account-wide message count
            Console.WriteLine($"Account Message Count: {_accountMessageCount}, Account Limit: {_accountLimit}");

            // Check if the account-wide message count has exceeded the limit
            if (_accountMessageCount >= _accountLimit)
            {
                Console.WriteLine("Account-wide message limit exceeded.");
                return false;
            }

            // Increment account-wide count
            Interlocked.Increment(ref _accountMessageCount);
            return true;
        }

        public void ResetLimits()
        {
            _numberMessageCounts.Clear();
            _accountMessageCount = 0;
        }
    }
}
