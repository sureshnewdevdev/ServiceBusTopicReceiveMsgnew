using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MessageReceiverController : ControllerBase
{
    private readonly ServiceBusTopicReceiverService _receiverService;

    public MessageReceiverController(ServiceBusTopicReceiverService receiverService)
    {
        _receiverService = receiverService;
    }

    // Endpoint to receive morning news messages
    [HttpGet("receive/morning")]
    public async Task<IActionResult> ReceiveMorningNews()
    {
        var result = await _receiverService.ReceiveMorningNewsMessageAsync();
        return Ok(result);
    }

    // Endpoint to receive evening news messages
    [HttpGet("receive/evening")]
    public async Task<IActionResult> ReceiveEveningNews()
    {
        var result = await _receiverService.ReceiveEveningNewsMessageAsync();
        return Ok(result);
    }
}
