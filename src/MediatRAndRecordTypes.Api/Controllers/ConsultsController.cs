using MediatR;
using MediatRAndRecordTypes.Api.Features;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;

namespace MediatRAndRecordTypes.Api.Controllers
{
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
        [ProducesResponseType(typeof(CreateConsult.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<CreateConsult.Response>> Create([FromBody] CreateConsult.Request request)
            => await _mediator.Send(request);

        [HttpPut(Name = "RescheduleConsultRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Reschedule.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Reschedule.Response>> Reschedule([FromBody] Reschedule.Request request)
            => await _mediator.Send(request);

        [HttpDelete("{consultId}", Name = "RemoveConsultRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task Remove([FromRoute] RemoveConsult.Request request)
            => await _mediator.Send(request);

        [HttpGet("{consultId}", Name = "GetConsultByIdRoute")]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        [ProducesResponseType(typeof(ProblemDetails), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(GetConsultById.Response), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<GetConsultById.Response>> GetById([FromRoute] GetConsultById.Request request)
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
        [ProducesResponseType(typeof(GetConsults.Response), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<GetConsults.Response>> Get()
            => await _mediator.Send(new GetConsults.Request());
    }
}
