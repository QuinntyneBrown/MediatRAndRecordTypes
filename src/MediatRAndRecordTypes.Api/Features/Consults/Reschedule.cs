using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public class Reschedule
    {
        public record Request(ConsultDto Consult) : IRequest<Response>;

        public record Response(ConsultDto Consult);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var consult = await _context.FindAsync<Consult>(request.Consult.ConsultId);

                consult.Reschedule(request.Consult.StartDate, request.Consult.EndDate);

                consult.EnsureValid(_context);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response(consult.ToDto());
            }
        }
    }
}
