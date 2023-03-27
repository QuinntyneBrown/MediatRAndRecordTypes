// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatRAndRecordTypes.Api.Data;

namespace MediatRAndRecordTypes.Api.Models;

public class Consult
{
    public Guid ConsultId { get; init; }
    public Guid ClientId { get; private set; }
    public DateRange DateRange { get; private set; }

    public Consult(Guid clientId, DateTime startDate, DateTime endDate)
    {
        ConsultId = Guid.NewGuid();
        ClientId = clientId;
        DateRange = DateRange.Create(startDate, endDate).Value;
    }

    private Consult()
    {

    }

    public void Reschedule(DateTime startDate, DateTime endDate)
    {
        DateRange = DateRange.Create(startDate, endDate).Value;
    }

    public void EnsureAvailability(IMediatRAndRecordTypesDbContext context)
    {
        if (context.Consults.Any(x => x.ConsultId != ConsultId
        && DateRange.StartDate < x.DateRange.EndDate && x.DateRange.StartDate < DateRange.EndDate))
        {
            throw new Exception("Overlap");
        }
    }
}

