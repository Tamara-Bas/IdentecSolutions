using AutoMapper;
using IdentecSolutions.Application.Models.Equipment;
using IdentecSolutions.Domain.Entities;

namespace IdentecSolutions.Application.Queries.GetEquipmentById
{
    public class GetEquipmentByIdMapper : Profile
    {
        //public GetEquipmentByIdMapper()
        //{
        //    CreateMap<EquipmentDto, Equipment>().ReverseMap();
        //}

        public GetEquipmentByIdMapper()
        {
            CreateMap<EquipmentDto, Equipment>()
                .ForMember(d => d.Id, x => x.MapFrom(s => s.Id))
                .ForMember(d => d.Name, x => x.MapFrom(s => s.Name))
                .ForMember(d => d.Description, x => x.MapFrom(s => s.Description))
                .ForMember(d => d.Price, x => x.MapFrom(s => s.Price))
                .ForMember(d => d.Status, x => x.MapFrom(s => s.Status));
        }
    }
}
