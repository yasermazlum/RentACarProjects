using Application.Features.OperationClaims.Constants;
using Application.Features.OperationClaims.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Core.Application.Pipelines.Authorization;
using Core.Security.Entities;
using MediatR;
using static Application.Features.OperationClaims.Constants.OperationClaimsOperationClaims;

namespace Application.Features.OperationClaims.Commands.Update;

public class UpdateOperationClaimCommand : IRequest<UpdatedOperationClaimResponse>, ISecuredRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string[] Roles => new[] { Admin, Write, OperationClaimsOperationClaims.Update };

    public class UpdateOperationClaimCommandHandler : IRequestHandler<UpdateOperationClaimCommand, UpdatedOperationClaimResponse>
    {
        private readonly IOperationClaimRepository _operationClaimRepository;
        private readonly IMapper _mapper;
        private readonly OperationClaimBusinessRules _operationClaimBusinessRules;

        public UpdateOperationClaimCommandHandler(
            IOperationClaimRepository operationClaimRepository,
            IMapper mapper,
            OperationClaimBusinessRules operationClaimBusinessRules
        )
        {
            _operationClaimRepository = operationClaimRepository;
            _mapper = mapper;
            _operationClaimBusinessRules = operationClaimBusinessRules;
        }

        public async Task<UpdatedOperationClaimResponse> Handle(UpdateOperationClaimCommand request, CancellationToken cancellationToken)
        {
            OperationClaim mappedOperationClaim = _mapper.Map<OperationClaim>(request);
            OperationClaim updatedOperationClaim = await _operationClaimRepository.UpdateAsync(mappedOperationClaim);
            UpdatedOperationClaimResponse updatedOperationClaimDto = _mapper.Map<UpdatedOperationClaimResponse>(updatedOperationClaim);
            return updatedOperationClaimDto;
        }
    }
}
