using AutoMapper;
using Carpool.DAL.Entities;

namespace Carpool.BL.Models
{
    public record ParticipantListModel() : ModelBase
    {
        public class MapperProfile : Profile
        {
            public MapperProfile()
            {
                CreateMap<ParticipantEntity, ParticipantListModel>();
            }
        }
        
    }
}
