using MediatR;

namespace mediator.Activities.Salutations
{
    public class CreateSalutationCommand : IRequest<int>
    {
        public string Abbreviation { get; set; }
        public string FullWord  { get; set; }
        
    }
}