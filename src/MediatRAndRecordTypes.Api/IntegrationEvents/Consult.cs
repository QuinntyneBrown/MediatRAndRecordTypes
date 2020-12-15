using MediatR;
using MediatRAndRecordTypes.Api.Models;

namespace MediatRAndRecordTypes.Api.IntegrationEvents
{
    public record ConsultRescheduled(Consult Consult):INotification;
}
