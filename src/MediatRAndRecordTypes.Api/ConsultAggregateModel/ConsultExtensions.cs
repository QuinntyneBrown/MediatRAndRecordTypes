// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

namespace MediatRAndRecordTypes.Api.ConsultAggregateModel;

public static class ConsultExtensions
{
    public static ConsultDto ToDto(this Consult consult)
        => new(consult.ConsultId, consult.ClientId, consult.DateRange.StartDate, consult.DateRange.EndDate);
}

