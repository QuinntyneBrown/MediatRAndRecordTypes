using System;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public record ConsultDto(Guid ConsultId, Guid CustomerId, DateTime StartDate, DateTime EndDate);
}
