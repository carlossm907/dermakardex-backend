using Cortex.Mediator.Notifications;
using dermakardex_backend.Shared.Domain.Model.Events;

namespace dermakardex_backend.Shared.Application.Internal.EventHandlers;

public interface IEventHandler<in TEvent> : INotificationHandler<TEvent> where TEvent : IEvent
{
}