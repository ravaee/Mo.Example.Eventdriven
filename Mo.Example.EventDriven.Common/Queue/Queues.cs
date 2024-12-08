using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mo.Example.EventDriven.Common.Queue;
public static class Queues
{
    public static string UserRequestTicket => nameof(UserRequestTicket);
    public static string TicketCreated => nameof(TicketCreated);
    public static string TicketJobDone => nameof(TicketJobDone);

}
