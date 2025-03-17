using AutoMapper;
using IdentecSolutions.Application.Models.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentecSolutions.Application.Commands.Equipment.UpdateEquipment
{
    public class UpdateEquipmentMapper : Profile
    {
        public UpdateEquipmentMapper()
        {
            CreateMap<Domain.Entities.Equipment, UpdateEquipmentModel>()
                .ForMember(d => d.Status, x => x.MapFrom(s => s.Status));
        }

    }
}
