using System;

namespace MediatRAndRecordTypes.Api.Features
{
    public record ConsultDto(Guid ConsultId, Guid CustomerId, DateTime StartDate, DateTime EndDate);
}
