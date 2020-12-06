using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public class RemoveConsult
    {
        public record Request(Guid ConsultId) : IRequest<Unit>;

        public class Handler : IRequestHandler<Request, Unit>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {

                var consult = await _context.FindAsync<Consult>(request.ConsultId);

                _context.Remove(consult);

                await _context.SaveChangesAsync(cancellationToken);

                return new Unit { };
            }
        }
    }
}
