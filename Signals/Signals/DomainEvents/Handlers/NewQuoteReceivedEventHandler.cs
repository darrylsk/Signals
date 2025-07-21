using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Signals.DomainEvents.Events;

namespace Signals.DomainEvents.Handlers;

public class NewQuoteReceivedEventHandler : INotificationHandler<NewQuoteReceivedEvent>
{
    public async Task Handle(NewQuoteReceivedEvent notification, CancellationToken cancellationToken)
    {
        if (cancellationToken.IsCancellationRequested)
        {
            Console.WriteLine(Resources.Resources.NewQuoteReceivedEventHandler_Handle_New_quote_operation_cancelled);
        }

        Console.WriteLine(Resources.Resources.NewQuoteReceivedEventHandler_Handle_New_quote_received);
    }
}