// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace MediatRAndRecordTypes.Api.Features;

public class GetConsultById
{
    public record Request(Guid ConsultId) : IRequest<Response>;

    public record Response(ConsultDto Consult) : IRequest<Response>;

    public class Handler : IRequestHandler<Request, Response>
    {
        private readonly IMediatRAndRecordTypesDbContext _context;

        public Handler(IMediatRAndRecordTypesDbContext context) => _context = context;

        public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
        {
            var consult = await _context.AsNoTracking().FindAsync<Consult>(request.ConsultId);

            return new(consult.ToDto());
        }
    }
}

