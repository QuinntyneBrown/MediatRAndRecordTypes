using MediatRAndRecordTypes.Api.ValueObjects;
using System;

namespace MediatRAndRecordTypes.Api.Models
{
    public class Consult
    {
        public Consult(Guid customerId, DateTime startDate, DateTime endDate)
        {
            CustomerId = customerId;
            DateRange = DateRange.Create(startDate,endDate).Value;
        }

        private Consult() { }

        public Guid ConsultId { get; private set; }
        public Guid CustomerId { get; private set; }
        public DateRange DateRange { get; private set; }

        public void ChangeDateRange(DateTime startDate, DateTime endDate)
        {
            DateRange = DateRange.Create(startDate, endDate).Value;
        }
    }
}
