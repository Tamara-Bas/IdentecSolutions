using AutoMapper;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Entities;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public class GetAllEquipmentByStatusHandler : IQueryHandler<GetAllEquipmentByStatusRequest, GetAllEquipmentByStatusResponse>
    {
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;
        private readonly IMapper _mapper;
        public GetAllEquipmentByStatusHandler(IEquipmentServiceRepository equipmentServiceRepository, IMapper mapper)
        {
            _equipmentServiceRepository = equipmentServiceRepository;
            _mapper = mapper;
        }
        public async Task<GetAllEquipmentByStatusResponse> Handle(GetAllEquipmentByStatusRequest request, CancellationToken cancellationToken)
        {
            var responseEquipmentByStatus = await _equipmentServiceRepository.GetAllEquipmentByStatus(request.Status, cancellationToken);
            var mappedResult = _mapper.Map<List<EquipmentDto>>(responseEquipmentByStatus);
            return new GetAllEquipmentByStatusResponse(mappedResult, mappedResult.Count);
        }
    }
}
