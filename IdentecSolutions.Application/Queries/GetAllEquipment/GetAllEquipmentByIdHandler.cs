using IdentecSolutions.Application.Core.Queries;
using IdentecSolutions.Application.Services.Equipment;
using System.Net.WebSockets;

namespace IdentecSolutions.Application.Queries.GetAllEquipment
{
    public class GetAllEquipmentByIdHandler : IQueryHandler<GettAllEquipmentByIdRequest, GetAllEquipmentByIdResponse>
    {
        private readonly IEquipmentServiceRepository _equipmentServiceRepository;
        public GetAllEquipmentByIdHandler(IEquipmentServiceRepository equipmentServiceRepository)
        {
            _equipmentServiceRepository = equipmentServiceRepository;
        }
        public async Task<GetAllEquipmentByIdResponse> Handle(GettAllEquipmentByIdRequest request, CancellationToken cancellationToken)
        {
            var te = await _equipmentServiceRepository.GetAllEquipmentById(request.Id, cancellationToken);
            return new GetAllEquipmentByIdResponse();
        }
    }
}
