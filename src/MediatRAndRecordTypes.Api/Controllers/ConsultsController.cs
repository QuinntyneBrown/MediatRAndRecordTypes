// Copyright (c) Quinntyne Brown. All Rights Reserved.
// Licensed under the MIT License. See License.txt in the project root for license information.

using MediatR;
using MediatRAndRecordTypes.Api.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;


namespace MediatRAndRecordTypes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ConsultsController
{
    private readonly IMediator _mediator;

    public ConsultsController(IMediator mediator) => _mediator = mediator;

    [HttpPost(Name = "CreateConsultRoute")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(CreateConsultResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<CreateConsultResponse>> Create([FromBody] CreateConsultRequest request)
        => await _mediator.Send(request);

    [HttpPut(Name = "RescheduleConsultRoute")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(RescheduleResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<RescheduleResponse>> Reschedule([FromBody] RescheduleRequest request)
        => await _mediator.Send(request);

    [HttpDelete("{consultId}", Name = "RemoveConsultRoute")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task Remove([FromRoute] RemoveConsultRequest request)
        => await _mediator.Send(request);

    [HttpGet("{consultId}", Name = "GetConsultByIdRoute")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetConsultByIdResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
    public async Task<ActionResult<GetConsultByIdResponse>> GetById([FromRoute] GetConsultByIdRequest request)
    {
        var response = await _mediator.Send(request);

        if (response.Consult == null)
        {
            return new NotFoundObjectResult(request.ConsultId);
        }

        return response;
    }

    [HttpGet(Name = "GetConsultsRoute")]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
    [ProducesResponseType(typeof(GetConsultsResponse), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<GetConsultsResponse>> Get()
        => await _mediator.Send(new GetConsultsRequest());
}

