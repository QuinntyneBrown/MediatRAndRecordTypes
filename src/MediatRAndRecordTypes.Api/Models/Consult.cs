using MediatRAndRecordTypes.Api.Data;
using System;
using System.Linq;

namespace MediatRAndRecordTypes.Api.Models
{
    public class Consult
    {
        public Consult(Guid clientId, DateTime startDate, DateTime endDate)
        {
            ConsultId = Guid.NewGuid();
            ClientId = clientId;
            DateRange = DateRange.Create(startDate, endDate).Value;
        }

        private Consult() { }

        public Guid ConsultId { get; private set; }
        public Guid ClientId { get; private set; }
        public DateRange DateRange { get; private set; }

        public void Reschedule(DateTime startDate, DateTime endDate)
        {
            DateRange = DateRange.Create(startDate, endDate).Value;
        }

        public void EnsureAvailability(IAppDbContext context)
        {
            if (context.Consults.Any(x => x.ConsultId != ConsultId 
            && DateRange.StartDate < x.DateRange.EndDate && x.DateRange.StartDate < DateRange.EndDate))
            {
                throw new Exception("Overlap");
            }
        }
    }
}
