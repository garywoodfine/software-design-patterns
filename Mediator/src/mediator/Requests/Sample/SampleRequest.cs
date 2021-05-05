using mediator.Content.Responses.Sample;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mediator.Content.Requests.Sample
{
    public class SampleRequest : IRequest<SampleResponse>
    {
        [FromRoute(Name = "id")] public string Id { get; set; }
    }
}