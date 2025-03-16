using IdentecSolutions.EF.Repository;

namespace IdentecSolutions.Application.Services.Equipment
{
    public class EquipmentService : IEquipmentServiceRepository
    {
        private readonly IBaseRepository<Domain.Entities.Equipment> _equipmentRepository;
        public EquipmentService(IBaseRepository<Domain.Entities.Equipment> equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }
        public async Task<List<Domain.Entities.Equipment>> GetAllEquipmentByStatus(bool status, CancellationToken cancellation)
        {
            try
            {
                var equipmentList = _equipmentRepository.Where(x => x.Status == status).ToList();
                return equipmentList;
            }
            
                 catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        public async Task<Domain.Entities.Equipment> GetEquipmentById(int equipmentId, CancellationToken cancellation)
        {
            try
            {
                return await  _equipmentRepository.FirstOrDefaultAsync(x => x.Id == equipmentId);
            }

            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }
    }
}
