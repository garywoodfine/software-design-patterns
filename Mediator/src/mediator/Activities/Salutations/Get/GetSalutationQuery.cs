using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace mediator.Content.Activities.Salutations
{
    public class GetSalutationQuery : IRequest<SalutationResponse>
    {
        [FromRoute(Name = "id")] public string Id { get; set; }
    }
}