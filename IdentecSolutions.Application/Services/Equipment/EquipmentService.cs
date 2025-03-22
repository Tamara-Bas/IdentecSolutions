using AutoMapper;
using Azure.Core;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.EF.Repository;
using IdentecSolutions.EF.UnitOfWork;

namespace IdentecSolutions.Application.Services.Equipment
{
    public class EquipmentService : IEquipmentServiceRepository
    {
        private readonly IBaseRepository<Domain.Entities.Equipment> _equipmentRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public EquipmentService(IBaseRepository<Domain.Entities.Equipment> equipmentRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _equipmentRepository = equipmentRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
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


        public async Task<Domain.Entities.Equipment> CreateEquipment(EquimpmentCreateModel request)
        {
            var entity = new Domain.Entities.Equipment();
            try
            {   
                entity = _mapper.Map<Domain.Entities.Equipment>(request);
                await _equipmentRepository.AddAsync(entity);
                //return entity;
            }
            catch (Exception ex)
            {;
                throw new Exception(ex.Message);
                //implement exception
            }
            return entity;
        }
        public async Task<Domain.Entities.Equipment> UpdateEquipment(UpdateEquipmentModel request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Equipment();
            try
            {
                entity = _mapper.Map<Domain.Entities.Equipment>(request);
                await _equipmentRepository.UpdateAsync(entity);
                //return entity;
            }
            catch (Exception ex)
            {
                ;
                throw new Exception(ex.Message);
                //implement exception
            }
            return entity;
        }

        public async Task<bool> DeleteEquipment(int id, CancellationToken cancellationToken)
        {
            bool isSuccess = false;
            try
            {
                await _equipmentRepository.DeleteAsync(id);
                isSuccess = true;
                //return entity;
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
                //implement exception
            }
            return isSuccess;
        }

        public async Task<Domain.Entities.Equipment> GetEquipmentBySerialNumber(string serialNumber, CancellationToken cancellation)
        {
            try
            {
                return await _equipmentRepository.FirstOrDefaultAsync(x => x.SerialNumber == serialNumber);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
