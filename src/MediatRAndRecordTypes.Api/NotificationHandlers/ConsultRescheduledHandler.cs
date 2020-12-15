using MediatR;
using MediatRAndRecordTypes.Api.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.NotificationHandlers
{
    internal class ConsultRescheduledHandler : INotificationHandler<ConsultRescheduled>
    {

        public ConsultRescheduledHandler()
        {

        }

        public async Task Handle(ConsultRescheduled notification, CancellationToken cancellationToken)
        {

        }
    }
}
