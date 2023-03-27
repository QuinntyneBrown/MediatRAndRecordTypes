// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;

namespace MediatRAndRecordTypes.Api.Features;

public record CreateConsultRequest(ConsultDto Consult) : IRequest<CreateConsultResponse>;

public record CreateConsultResponse(ConsultDto Consult);

public class CreateConsultHandler : IRequestHandler<CreateConsultRequest, CreateConsultResponse>
{
    private readonly IMediatRAndRecordTypesDbContext _context;

    public CreateConsultHandler(IMediatRAndRecordTypesDbContext context) => _context = context;

    public async Task<CreateConsultResponse> Handle(CreateConsultRequest request, CancellationToken cancellationToken)
    {
        var consult = new Consult(request.Consult.CustomerId, request.Consult.StartDate, request.Consult.EndDate);

        consult.EnsureAvailability(_context);

        _context.Add(consult);

        await _context.SaveChangesAsync(cancellationToken);

        return new(consult.ToDto());
    }
}

