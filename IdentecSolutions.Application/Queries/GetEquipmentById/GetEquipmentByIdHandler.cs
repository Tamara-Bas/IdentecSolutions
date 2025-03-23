using AutoMapper;
using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Application.Services.Equipment;
using IdentecSolutions.Domain.Exceptions;

namespace IdentecSolutions.Application.Queries.GetEquipmentById
{
    public class GetEquipmentByIdHandler : IQueryHandler<GetEquipmentByIdRequest, GetEquipmentByIdResponse>
    {
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;
        private readonly IMapper _mapper;
        public GetEquipmentByIdHandler(IEquipmentServiceRepository equipmentServiceRepository, IMapper mapper)
        {
            _equipmentServiceRepository = equipmentServiceRepository;
            _mapper = mapper;
        }
        public async Task<GetEquipmentByIdResponse> Handle(GetEquipmentByIdRequest request, CancellationToken cancellationToken)
        {
            var responseEquipment = await _equipmentServiceRepository.GetEquipmentById(request.Id, cancellationToken);
            if (responseEquipment == null)
            {
                throw new NotFoundException("Equipment not found");
            }
            var mappedEquipment = _mapper.Map<EquipmentDto>(responseEquipment);
            return new GetEquipmentByIdResponse(mappedEquipment);
        }
    }
}
