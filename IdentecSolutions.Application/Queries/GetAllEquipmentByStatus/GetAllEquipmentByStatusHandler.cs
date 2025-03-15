using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Services.Equipment;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public class GetAllEquipmentByStatusHandler : IQueryHandler<GettAllEquipmentByStatusRequest, GetAllEquipmentByStatusResponse>
    {
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;
        public GetAllEquipmentByStatusHandler(IEquipmentServiceRepository equipmentServiceRepository)
        {
            _equipmentServiceRepository = equipmentServiceRepository;
        }
        public async Task<GetAllEquipmentByStatusResponse> Handle(GettAllEquipmentByStatusRequest request, CancellationToken cancellationToken)
        {
            var te = await _equipmentServiceRepository.GetAllEquipmentByStatus(request.Status, cancellationToken);
            return null;
        }
    }
}
