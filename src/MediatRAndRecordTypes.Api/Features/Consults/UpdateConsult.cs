using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public class UpdateConsult
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

                consult.ChangeDateRange(request.Consult.StartDate, request.Consult.EndDate);

                await _context.SaveChangesAsync(cancellationToken);

                return new Response(consult.ToDto());
            }
        }
    }
}
