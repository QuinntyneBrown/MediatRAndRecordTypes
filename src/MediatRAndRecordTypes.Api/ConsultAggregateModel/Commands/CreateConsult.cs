// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;

namespace MediatRAndRecordTypes.Api.ConsultAggregateModel.Commands;

public record CreateConsultRequest(Guid CustomerId, DateTime StartDate, DateTime EndDate) : IRequest<CreateConsultResponse>;

public record CreateConsultResponse(ConsultDto Consult);

public class CreateConsultHandler : IRequestHandler<CreateConsultRequest, CreateConsultResponse>
{
    private readonly IMediatRAndRecordTypesDbContext _context;

    public CreateConsultHandler(IMediatRAndRecordTypesDbContext context) => _context = context;

    public async Task<CreateConsultResponse> Handle(CreateConsultRequest request, CancellationToken cancellationToken)
    {
        var consult = new Consult(request.CustomerId, request.StartDate, request.EndDate);

        consult.EnsureAvailability(_context);

        _context.Add(consult);

        await _context.SaveChangesAsync(cancellationToken);

        return new(consult.ToDto());
    }
}

