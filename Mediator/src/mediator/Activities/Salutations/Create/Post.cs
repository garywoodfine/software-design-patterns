using System;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.ApiEndpoints;
using mediator.Content.Activities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace mediator.Activities.Salutations
{
    [Route(RouteNames.Salutations)]
    public class Post : BaseAsyncEndpoint.WithRequest<CreateSalutationCommand>.WithoutResponse
    {
        private readonly IMediator _mediator;

        public Post(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpPost]
        [SwaggerOperation(
            Summary = "Create a new salutation",
            Description = "",
            OperationId = "AA440D51-75A5-4975-8875-C1799B58D4EB",
            Tags = new []{RouteNames.Salutations}
            )]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public override async Task<ActionResult> HandleAsync([FromBody] CreateSalutationCommand request, CancellationToken cancellationToken = new())
        {
            var result = await _mediator.Send(request, cancellationToken);
            return new CreatedResult( new Uri(RouteNames.Salutations, UriKind.Relative), new { id = result });
        }
    }
}