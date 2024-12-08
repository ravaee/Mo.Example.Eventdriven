using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo.Example.EventDriven.Common.Queue;
public interface IMessage
{
    Guid Id { get; }
    DateTime Timestamp { get; }
}

public class UserRequestTicketMessage : IMessage
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string UserId { get; set; }
    // Other properties
}

public class TicketCreatedMessage : IMessage
{
    public Guid Id { get; set; }
    public DateTime Timestamp { get; set; }
    public string TicketId { get; set; }
    // Other properties
}

public class EmailSentMessage : IMessage
{
    public Guid Id { get; }
    public DateTime Timestamp { get; }
}