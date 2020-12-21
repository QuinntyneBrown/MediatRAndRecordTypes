using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.IntegrationEvents;
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
            private readonly IMediator _mediator;
            public Handler(IAppDbContext context, IMediator mediator)
            {
                _context = context;
                _mediator = mediator;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var consult = await _context.FindAsync<Consult>(request.Consult.ConsultId);

                consult.Reschedule(request.Consult.StartDate, request.Consult.EndDate);

                consult.EnsureAvailability(_context);

                await _context.SaveChangesAsync(cancellationToken);

                await _mediator.Publish(new ConsultRescheduled(consult));

                return new (consult.ToDto());
            }
        }
    }
}
