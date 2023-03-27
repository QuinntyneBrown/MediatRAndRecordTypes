// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;

namespace MediatRAndRecordTypes.Api.Features;

public record GetConsultsRequest : IRequest<GetConsultsResponse>;

public record GetConsultsResponse(List<ConsultDto> Consults);

public class GetConsultsHandler : IRequestHandler<GetConsultsRequest, GetConsultsResponse>
{
    private readonly IMediatRAndRecordTypesDbContext _context;

    public GetConsultsHandler(IMediatRAndRecordTypesDbContext context) => _context = context;

    public async Task<GetConsultsResponse> Handle(GetConsultsRequest request, CancellationToken cancellationToken)
    {
        return new(await _context.AsNoTracking().Consults.Select(x => x.ToDto()).ToListAsync(cancellationToken));
    }
}

