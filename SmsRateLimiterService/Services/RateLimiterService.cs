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
            // Check per-number limit
            _numberMessageCounts.AddOrUpdate(phoneNumber, 1, (key, count) => count < _perNumberLimit ? count + 1 : count);
            if (_numberMessageCounts[phoneNumber] > _perNumberLimit) return false;

            // Check account-wide limit
            if (_accountMessageCount >= _accountLimit) return false;

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
