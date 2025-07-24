using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Signals.DomainEvents.Abstract;

public interface IHandle<in TEvent> : INotificationHandler<TEvent> where TEvent : IDomainEvent
{
    Task Handle(INotification notification, CancellationToken cancellationToken);
}