// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Data;
using MediatRAndRecordTypes.Api.IntegrationEvents;
using MediatRAndRecordTypes.Api.Models;
using System.Threading;
using System.Threading.Tasks;


namespace MediatRAndRecordTypes.Api.Features;

 public record RescheduleRequest(ConsultDto Consult) : IRequest<RescheduleResponse>;

 public record RescheduleResponse(ConsultDto Consult);

 public class RescheduleHandler : IRequestHandler<RescheduleRequest, RescheduleResponse>
 {
     private readonly IMediatRAndRecordTypesDbContext _context;
     private readonly IMediator _mediator;
     public RescheduleHandler(IMediatRAndRecordTypesDbContext context, IMediator mediator)
     {
         _context = context;
         _mediator = mediator;
     }

     public async Task<RescheduleResponse> Handle(RescheduleRequest request, CancellationToken cancellationToken)
     {
         var consult = await _context.FindAsync<Consult>(request.Consult.ConsultId);

         consult.Reschedule(request.Consult.StartDate, request.Consult.EndDate);

         consult.EnsureAvailability(_context);

         await _context.SaveChangesAsync(cancellationToken);

         await _mediator.Publish(new ConsultRescheduled(consult));

         return new(consult.ToDto());
     }
 }

