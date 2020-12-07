using MediatRAndRecordTypes.Api.Features.Consults;
using MediatRAndRecordTypes.Api.Models;

namespace MediatRAndRecordTypes.Api.Features
{
    public static class ConsultExtensions
    {
        public static ConsultDto ToDto(this Consult consult)
            => new ConsultDto(consult.ConsultId, consult.CustomerId, consult.DateRange.StartDate, consult.DateRange.EndDate);
    }
}
