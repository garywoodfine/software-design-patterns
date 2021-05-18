using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace mediator.Content.Activities.Salutations
{
    [Route(RouteNames.Salutations)]
    public class Get : BaseAsyncEndpoint.WithRequest<GetSalutationQuery>.WithResponse<SalutationResponse>
    {
        private readonly IMediator _mediator;

        public Get(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Retrieve Salutation details by Id ",
            Description = "Retrieves a Salutation response ",
            OperationId = "EF0A3653-153F-4E73-8D20-621C9F9FFDC9",
            Tags = new[] {RouteNames.Salutations})
        ]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SalutationResponse))]
        [Produces("application/json")]
        public override async Task<ActionResult<SalutationResponse>> HandleAsync([FromRoute] GetSalutationQuery request,
            CancellationToken cancellationToken = default)
        {
            var response =  await _mediator.Send(request, cancellationToken);
            return new OkObjectResult(response);
        }
    }
}