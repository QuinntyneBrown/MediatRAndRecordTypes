using MediatRAndRecordTypes.Api.Features.Consults;
using MediatRAndRecordTypes.Api.Models;

namespace MediatRAndRecordTypes.Api.Features
{
    public static class ConsultExtensions
    {
        public static ConsultDto ToDto(this Consult consult)
            => new(consult.ConsultId, consult.ClientId, consult.DateRange.StartDate, consult.DateRange.EndDate);
    }
}
