using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Boleyn.Database.Entities.Authors;
using MediatR;
using Threenine.Data;

namespace mediator.Activities.Salutations
{
    public class PostHandler : IRequestHandler<CreateSalutationCommand, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PostHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<int> Handle(CreateSalutationCommand request, CancellationToken cancellationToken)
        {
            var salutation = _mapper.Map<Salutation>(request);
            var repo = _unitOfWork.GetRepositoryAsync<Salutation>();
            await repo.InsertAsync(salutation, cancellationToken);
            await _unitOfWork.CommitAsync();
            return salutation.Id;
        }
    }
}