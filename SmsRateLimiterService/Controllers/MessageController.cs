using Microsoft.AspNetCore.Mvc;
using SmsRateLimiterService.Models;

namespace SmsRateLimiterService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessageController : ControllerBase
    {
        private readonly SmsRateLimiterService.Services.RateLimiterService _rateLimiterService;

        public MessageController(SmsRateLimiterService.Services.RateLimiterService rateLimiterService)
        {
            _rateLimiterService = rateLimiterService;
        }

        [HttpPost("check-eligibility")]
        public IActionResult CheckMessageEligibility([FromBody] MessageRequest request)
        {
            if (_rateLimiterService.CanSendMessage(request.BusinessPhoneNumber))
            {
                return Ok(new { Message = "Message can be sent." });
            }
            return BadRequest(new { Message = "Message limit exceeded. Cannot send message." });
        }
    }
}
