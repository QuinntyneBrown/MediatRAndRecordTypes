// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;

namespace MediatRAndRecordTypes.Api.ConsultAggregateModel.Queries;

public record GetConsultByIdRequest(Guid ConsultId) : IRequest<GetConsultByIdResponse>;

public record GetConsultByIdResponse(ConsultDto Consult) : IRequest<GetConsultByIdResponse>;

public class GetConsultByIdHandler : IRequestHandler<GetConsultByIdRequest, GetConsultByIdResponse>
{
    private readonly IMediatRAndRecordTypesDbContext _context;

    public GetConsultByIdHandler(IMediatRAndRecordTypesDbContext context) => _context = context;

    public async Task<GetConsultByIdResponse> Handle(GetConsultByIdRequest request, CancellationToken cancellationToken)
    {
        var consult = await _context.AsNoTracking().FindAsync<Consult>(request.ConsultId);

        return new(consult.ToDto());
    }
}

