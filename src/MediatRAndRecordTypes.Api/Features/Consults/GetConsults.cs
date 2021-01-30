using MediatR;
using MediatRAndRecordTypes.Api.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Features
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
                return new(await _context.AsNoTracking().Consults.Select(x => x.ToDto()).ToListAsync(cancellationToken));
            }
        }
    }
}
