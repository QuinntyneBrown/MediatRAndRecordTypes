using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.Models;
using System.Threading;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Features.Consults
{
    public class CreateConsult
    {
        public record Request(ConsultDto Consult) : IRequest<Response>;

        public record Response(ConsultDto Consult);

        public class Handler : IRequestHandler<Request, Response>
        {
            private readonly IAppDbContext _context;

            public Handler(IAppDbContext context) => _context = context;

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {

                var consult = new Consult(request.Consult.CustomerId, request.Consult.StartDate, request.Consult.EndDate);

                consult.EnsureAvailability(_context);

                _context.Add(consult);

                await _context.SaveChangesAsync(cancellationToken);

                return new (consult.ToDto());
            }
        }
    }
}
