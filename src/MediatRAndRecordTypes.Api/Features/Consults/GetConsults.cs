using MediatR;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.QueryTrackingBehavior;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public class GetConsults
    {
        public record Request : IRequest<Response>;

        public record Response(List<ConsultDto> Consults);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                _context.ChangeTracker.QueryTrackingBehavior = NoTracking;

                return new(await _context.Consults.Select(x => x.ToDto()).ToListAsync(cancellationToken));
            }
        }
    }
}
