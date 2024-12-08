using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Mo.Example.EventDriven.Common;
using Mo.Example.EventDriven.Common.Queue;

namespace Mo.Example.Eventdriven.Controllers;

[ApiController]
[Route("")]
public class TransactionController(
    IMessageHandler messageHandler) : ControllerBase
{
    public string Index()
    {
        return "got";
    }

    [Route("/Ticket/Create")]
    public IActionResult CreateTicket()
    {
        try
        {
            var message = new UserRequestTicketMessage
            {
                Id = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow,
                UserId = Guid.NewGuid().ToString(),
            };

            messageHandler.Publish("UserRequestTicket", message);

            return Ok();
        }
        catch (Exception ex)
        {

            return BadRequest();
        }

        
    }
}
