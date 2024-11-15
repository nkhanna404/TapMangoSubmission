namespace SmsRateLimiterService.RateLimiterService.Tests
{
    using Xunit;
    using SmsRateLimiterService.Services;
    public class RateLimiterServiceTests
    {
        private readonly RateLimiterService _rateLimiterService;

        public RateLimiterServiceTests()
        {
            // Initialize RateLimiterService with limits for testing
            _rateLimiterService = new RateLimiterService(5, 10); // Example: 5 per number, 10 account-wide
        }

        [Fact]
        public void CanSendMessage_WithinLimit_ShouldReturnTrue()
        {
            // Act
            bool result = _rateLimiterService.CanSendMessage("123-456-7890");

            // Assert
            Assert.True(result, "Expected message to be allowed within the rate limit.");
        }

        [Fact]
        public void CanSendMessage_ExceedsPerNumberLimit_ShouldReturnFalse()
        {
            // Act: Send messages up to the per-number limit
            for (int i = 0; i < 5; i++)
            {
                _rateLimiterService.CanSendMessage("123-456-7890");
            }

            // Attempt to send one more message, which should exceed the limit
            bool result = _rateLimiterService.CanSendMessage("123-456-7890");

            // Assert
            Assert.False(result, "Expected message to be blocked due to per-number limit exceeded.");
        }

        [Fact]
        public void CanSendMessage_ExceedsAccountWideLimit_ShouldReturnFalse()
        {
            // Act: Send messages across different numbers up to the account-wide limit
            for (int i = 0; i < 10; i++)
            {
                _rateLimiterService.CanSendMessage($"123-456-78{i}"); // Using different phone numbers
            }

            // Attempt to send one more message with a new phone number, which should exceed the account-wide limit
            bool result = _rateLimiterService.CanSendMessage("999-999-9999");

            // Assert
            Assert.False(result, "Expected message to be blocked due to account-wide limit exceeded.");
        }
    }
}
