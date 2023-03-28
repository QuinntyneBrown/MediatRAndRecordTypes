// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndRecordTypes.Api.ConsultAggregateModel.Commands;

public record RemoveConsultRequest(Guid ConsultId) : IRequest;

public class RemoveConsultHandler : IRequestHandler<RemoveConsultRequest>
{
    private readonly IMediatRAndRecordTypesDbContext _context;

    public RemoveConsultHandler(IMediatRAndRecordTypesDbContext context) => _context = context;

    public async Task Handle(RemoveConsultRequest request, CancellationToken cancellationToken)
    {

        var consult = await _context.Consults.SingleOrDefaultAsync(x => x.ConsultId == request.ConsultId);

        _context.Remove(consult);

        await _context.SaveChangesAsync(cancellationToken);

    }
}

