using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace mediator.Content.Activities.Salutations
{
    public class GetSalutationHandler : IRequestHandler<GetSalutationQuery, SalutationResponse>
    {
        public async Task<SalutationResponse> Handle(GetSalutationQuery getSalutationQuery, CancellationToken cancellationToken)
        {
            /// Your Logic Goes here 
            // This is only to supply an example and you should do whatever you need to achieve here
            return await Task.FromResult(new SalutationResponse
            {
                Id = Guid.NewGuid().ToString(),
                Name = nameof(SalutationResponse)
            });
        }
    }
}