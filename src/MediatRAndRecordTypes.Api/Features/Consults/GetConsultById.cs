using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public class GetConsultById
    {
        public record Request(Guid ConsultId) : IRequest<Response>;

        public record Response(ConsultDto Consult) : IRequest<Response>;

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken) {

                var consult = await _context.FindAsync<Consult>(request.ConsultId);

                return new Response(consult.ToDto());
            }
        }
    }
}
